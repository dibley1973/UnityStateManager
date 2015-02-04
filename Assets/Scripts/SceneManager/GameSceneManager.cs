using Assets.Scripts.Resources;
using Assets.Scripts.SceneManager;
using RuleEngine.Engine;
using System;

namespace Dibware.UnityStateManager.Assets.Scripts.SceneManager
{
    /// <summary>
    /// Manages game scene transitions
    /// </summary>
    internal class GameSceneManager
    {
        #region Properties

        /// <summary>
        /// Gets or sets the rule engine.
        /// </summary>
        /// <value>
        /// The rule engine.
        /// </value>
        private RuleEngine<GameSceneTransition> RuleEngine { get; set; }

        /// <summary>
        /// Gets (or privately sets) the identity which indicates the current game scene.
        /// </summary>
        /// <value>The identity of the game scene.</value>
        public GameSceneIdentifier GameSceneIdentifier { get; private set; }

        /// <summary>
        /// Sets the game scene transition failed callback. The parameter contains
        /// </summary>
        /// <value>
        /// The call back to be called if the game scene transition failed.
        /// </value>
        public GameSceneTransitionFailedDelegate GameSceneTransitionFailedCallback { get; set; }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when the game scene has changed.
        /// </summary>
        public event GameSceneChangedHandler GameSceneChanged;

        /// <summary>
        /// Occurs when the game scene is changing.
        /// </summary>
        public event GameSceneChangingHandler GameSceneChanging;

        #endregion

        #region Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="GameSceneManager"/> class from being created.
        /// </summary>
        private GameSceneManager() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameSceneManager"/> class.
        /// </summary>
        /// <param name="ruleEngine">The rule engine.</param>
        public GameSceneManager(RuleEngine<GameSceneTransition> ruleEngine)
        {
            // Guard against a null RuleEngine
            if (ruleEngine == null) { throw new ArgumentNullException("ruleEngine"); }

            // Set the rule engine reference
            RuleEngine = ruleEngine;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Calls the game scenetransition failed call back if required.
        /// </summary>
        /// <param name="failedReason">The failed reason.</param>
        private void CallGameSceneTransitionFailedCallBackIfRequired(string failedReason)
        {
            if (GameSceneTransitionFailedCallback != null)
            {
                GameSceneTransitionFailedCallback(failedReason);
            }
        }

        /// <summary>
        /// Called when the game scene needs to be changed
        /// </summary>
        /// <param name="newGameSceneIdentifier">The new game scene identifier.</param>
        private void OnGameSceneChange(GameSceneIdentifier newGameSceneIdentifier)
        {
            // We will capture the 'Original' scene as we will need to know what 
            // it originally was after it has changed
            GameSceneIdentifier originalSceneIdentifier = this.GameSceneIdentifier;

            // Create the game scene transition we are about to make happen and 
            // use this in the rule engine to validate that the game scene 
            // transition is valid.
            GameSceneTransition sceneTransition =
                new GameSceneTransition(originalSceneIdentifier, newGameSceneIdentifier);
            RuleEngine.ActualValue = sceneTransition;

            // A valid transitoin is one which matches ANY of the rules
            bool isValidTransition = RuleEngine.MatchAny();

            // Check if the transition is valid...
            if (!isValidTransition)
            {
                // It's not so we will check the GameSceneTransitionFailedCallback
                // has a reference and call that with a parameter indicating the reason
                CallGameSceneTransitionFailedCallBackIfRequired(GameSceneTransitionFailedReasons.InvalidTransition);

                // Exit the method
                return;
            }

            // As we intend to raise an event before the same scene is changed 
            // in case any subscribers wish to cancel the scene change we need a 
            // flag which we can use to signify cancellation. Initially we will 
            // assume the game scene wont be cancelled...
            bool cancelled = false;

            // Raise the game scene changing event to give any subscribers chance
            // to cancel this process
            RaiseGameSceneChangingEvent(originalSceneIdentifier, newGameSceneIdentifier, ref cancelled);

            // Check to see if the event was cancelled...
            if (cancelled)
            {
                // ... it was so we will check the GameSceneTransitionFailedCallback
                // has a reference and call that with a parameter indicating the reason
                CallGameSceneTransitionFailedCallBackIfRequired(GameSceneTransitionFailedReasons.CancelledTransition);

                // Exit the method
                return;
            }

            // Getting here indicates that the event was not cancelled, so 
            // we can actually set the game scene!
            this.GameSceneIdentifier = newGameSceneIdentifier;

            // Raise the game scene changed event to notify any listeners that 
            // the game state has changed.
            RaiseGameSceneChangedEvent(originalSceneIdentifier, newGameSceneIdentifier);
        }

        /// <summary>
        /// Raises the game scene changed event.
        /// </summary>
        /// <param name="originalScene">Indicates the original scene.</param>
        /// <param name="newGameScene">Indicates the new game scene.</param>
        private void RaiseGameSceneChangedEvent(GameSceneIdentifier originalSceneIdentifier,
            GameSceneIdentifier newGameSceneIdentifier)
        {
            // See if we have any delgates attached that need to 
            // be called after we change game scene
            if (GameSceneChanged != null)
            {
                // Create new game scene changed event data
                GameSceneChangedEventArgs gameSceneChangedEventArgs =
                    new GameSceneChangedEventArgs(newGameSceneIdentifier, originalSceneIdentifier);

                // Raise the event
                GameSceneChanged(this, gameSceneChangedEventArgs);
            }
        }

        /// <summary>
        /// Raises the game scene changing event.
        /// </summary>
        /// <param name="originalScene">Indicates the original scene.</param>
        /// <param name="newGameScene">Indicates the new game scene.</param>
        /// <param name="cancelled">Set to true if the scene change was cancelled.</param>
        private void RaiseGameSceneChangingEvent(GameSceneIdentifier originalSceneIdentifier,
            GameSceneIdentifier newGameSceneIdentifier, ref bool cancelled)
        {
            // First see if we have any delgates attached that need to 
            // be called before we change game scene
            if (GameSceneChanging != null)
            {
                // Create new game scene changing event data
                GameSceneChangingEventArgs gameSceneChangingEventArgs =
                    new GameSceneChangingEventArgs(originalSceneIdentifier, newGameSceneIdentifier);

                // Raise the game scene changing event handler
                GameSceneChanging(this, gameSceneChangingEventArgs);

                // Set cancelled flag scene from the event args
                cancelled = gameSceneChangingEventArgs.Cancel;
            }
        }

        /// <summary>
        /// Sets the game scene identifier.
        /// </summary>
        /// <param name="gameSceneIdentifier">Game scene identifier.</param>
        public void SetGameScene(GameSceneIdentifier gameSceneIdentifier)
        {
            // First check if the specified game scene identifier differs from 
            // the current game scene identifier...
            if (this.GameSceneIdentifier != gameSceneIdentifier)
            {
                // ... it does so call a method to handle any actions we may
                // need to perform when the game scene identifier is changed.
                OnGameSceneChange(gameSceneIdentifier);
            }
        }

        #endregion
    }
}