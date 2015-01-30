﻿using Assets.Scripts.SceneManager;
using Dibware.UnityStateManager.Assets.Scripts.SceneManager;
using RuleEngine.Engine;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.StateManager
{
    /// <summary>
    /// Responsible for managing the game's state
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        #region MyRegion

        /// <summary>
        /// Gets or sets the game scene manager.
        /// </summary>
        internal GameSceneManager SceneManager { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GameManager"/> class.
        /// </summary>
        public GameManager()
        {
            // Create a new SceneManager and give it to the instance to use
            SceneManager = CreateSceneManager();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether this instance is game unsaved.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is game unsaved; otherwise, <c>false</c>.
        /// </value>
        public bool IsGameUnsaved { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the scene manager.
        /// </summary>
        /// <returns></returns>
        private GameSceneManager CreateSceneManager()
        {
            // Create a new rule engine
            RuleEngine<GameSceneTransition> sceneTransitionRuleEngine = new RuleEngine<GameSceneTransition>();

            // Add the rules
            AddGameSceneTransitionRules(sceneTransitionRuleEngine);

            // Create the scene manager with the rule engine
            GameSceneManager gameSceneManager = new GameSceneManager(sceneTransitionRuleEngine);

            // Wire up the event handlers
            gameSceneManager.GameSceneChanged += gameSceneManager_GameSceneChanged;
            gameSceneManager.GameSceneChanging += gameSceneManager_GameSceneChanging;

            // Wire up any callbacks
            gameSceneManager.GameSceneTransitionFailedCallback = gameSceneManager_GameSceneTransitionFailedCallback;

            // return the scene manager
            return gameSceneManager;
        }

        /// <summary>
        /// Adds the game scene transition rules to the specifed rule engine.
        /// </summary>
        /// <param name="sceneTransitionRuleEngine">The game scene rule engine to add rules to.</param>
        private void AddGameSceneTransitionRules(RuleEngine<GameSceneTransition> sceneTransitionRuleEngine)
        {
            // Create the game scene transition
            GameSceneTransition nullToIntro = new GameSceneTransition(GameSceneIdentifier.Null, GameSceneIdentifier.Intro);
            GameSceneTransition introToMainMenu = new GameSceneTransition(GameSceneIdentifier.Intro, GameSceneIdentifier.MainMenu);
            GameSceneTransition mainMenuToNewGame = new GameSceneTransition(GameSceneIdentifier.MainMenu, GameSceneIdentifier.NewGame);
            GameSceneTransition mainMenuToLoadGame = new GameSceneTransition(GameSceneIdentifier.MainMenu, GameSceneIdentifier.LoadGame);
            GameSceneTransition mainMenuToPlayGame = new GameSceneTransition(GameSceneIdentifier.MainMenu, GameSceneIdentifier.PlayGame);
            GameSceneTransition mainMenuToSaveGame = new GameSceneTransition(GameSceneIdentifier.MainMenu, GameSceneIdentifier.SaveGame);
            GameSceneTransition mainMenuToQuitGame = new GameSceneTransition(GameSceneIdentifier.MainMenu, GameSceneIdentifier.QuitGame);

            // Create the rules
            var transitionNullToIntro = new GameSceneTransitionRule(nullToIntro);
            var transitionIntroToMainMenu = new GameSceneTransitionRule(introToMainMenu);
            var transitionMainMenuToNewGame = new GameSceneTransitionRule(mainMenuToNewGame);
            var transitionmainMenuToLoadGame = new GameSceneTransitionRule(mainMenuToLoadGame);
            var transitionmainMenuToPlayGame = new GameSceneTransitionRule(mainMenuToPlayGame);
            var transitionmainMenuToSaveGame = new GameSceneTransitionRule(mainMenuToSaveGame);
            var transitionmainMenuToQuitGame = new GameSceneTransitionRule(mainMenuToQuitGame);

            // Add the rules to the engine
            sceneTransitionRuleEngine.Add(transitionNullToIntro);
            sceneTransitionRuleEngine.Add(transitionIntroToMainMenu);
            sceneTransitionRuleEngine.Add(transitionMainMenuToNewGame);
            sceneTransitionRuleEngine.Add(transitionmainMenuToLoadGame);
            sceneTransitionRuleEngine.Add(transitionmainMenuToPlayGame);
            sceneTransitionRuleEngine.Add(transitionmainMenuToSaveGame);
            sceneTransitionRuleEngine.Add(transitionmainMenuToQuitGame);
        }

        /// <summary>
        /// Cancels the scene transition if the unsaved game state is true and 
        /// the scene is about to be changed to a scene where the current game 
        /// will be ended.
        /// </summary>
        /// <param name="e">
        /// The <see cref="GameSceneChangingEventArgs"/> instance containing the event data.
        /// </param>
        /// <param name="gameIsUnsaved">
        /// Set to <c>true</c> if the game is unsaved.
        /// </param>
        private void CancelSceneTransitionIfUnsaved(GameSceneChangingEventArgs e, bool gameIsUnsaved)
        {
            // Check if the game is unsaved flag is set...
            if (gameIsUnsaved)
            {
                // ... is is so create a list of game scenes which are eligible for
                // checking if the game is unsaved before allowing the scene to change.
                List<GameSceneIdentifier> affectedGameScenes = new List<GameSceneIdentifier>
                {
                    GameSceneIdentifier.LoadGame,
                    GameSceneIdentifier.NewGame,
                    GameSceneIdentifier.QuitGame
                };

                // Set the cancel flag if this list of eligible scenes contains 
                // the prospective scene
                e.Cancel = affectedGameScenes.Contains(e.ProspectiveSceneIdentifier);

                // You may want to extend the GameSceneChangingEventArgs event 
                // data class to include a property that can hold a cancel reason.
                // For example:
                // e.CancelReason = Resources.SceneCancelReasons.GameIsUnsaved;
            }
        }

        #endregion

        #region gameSceneManager Handler and callback Methods

        /// <summary>
        /// Handles the GameSceneChanged event of the gameSceneManager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GameSceneChangedEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void gameSceneManager_GameSceneChanged(object sender, GameSceneChangedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Handles the GameSceneChanging event of the gameSceneManager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GameSceneChangingEventArgs"/> instance containing the event data.</param>
        void gameSceneManager_GameSceneChanging(object sender, GameSceneChangingEventArgs e)
        {
            CancelSceneTransitionIfUnsaved(e, IsGameUnsaved);
        }

        /// <summary>
        /// The callback method which is called when the games scene manager 
        /// acknowledges that a game scene transition has failed.
        /// </summary>
        /// <param name="reason">
        /// Identifies the reason the scene transition failed.
        /// </param>
        private void gameSceneManager_GameSceneTransitionFailedCallback(string reason)
        {
            // Handle the failure. Maybe display the message to the player?
            System.Diagnostics.Debug.WriteLine(reason);
        }

        #endregion
    }
}