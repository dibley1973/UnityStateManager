
using Assets.Scripts.SceneManager;
namespace Assets.Scripts.EventManager
{
    /// <summary>
    /// Handles raising of game events
    /// </summary>
    internal class GameEventManager
    {
        #region GameSceneManger.GameSceneChanged

        /// <summary>
        /// Occurs when the game scene has changed.
        /// </summary>
        public event GameSceneChangedHandler GameSceneChanged;

        /// <summary>
        /// Raises the game scene changed event.
        /// </summary>
        /// <param name="originalScene">Indicates the original scene.</param>
        /// <param name="newGameScene">Indicates the new game scene.</param>
        public void RaiseGameSceneChangedEvent(GameSceneIdentifier originalSceneIdentifier,
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

        #endregion

        #region GameSceneManger.GameSceneChanging

        /// <summary>
        /// Occurs when the game scene is changing.
        /// </summary>
        public event GameSceneChangingHandler GameSceneChanging;

        /// <summary>
        /// Raises the game scene changing event.
        /// </summary>
        /// <param name="originalScene">Indicates the original scene.</param>
        /// <param name="newGameScene">Indicates the new game scene.</param>
        /// <param name="cancelled">Set to true if the scene change was cancelled.</param>
        public void RaiseGameSceneChangingEvent(GameSceneIdentifier originalSceneIdentifier,
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

        #endregion
    }
}
