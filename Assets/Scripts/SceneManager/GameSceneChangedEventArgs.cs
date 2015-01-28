using System;

namespace Assets.Scripts.SceneManager
{
    /// <summary>
    /// Contains game scene changed event data.
    /// </summary>
    public class GameSceneChangedEventArgs : EventArgs
    {
        #region Constructors

        /// <summary>
        /// Hides the default constructor for the <see cref="GameSceneChangedEventArgs"/> class.
        /// </summary>
        private GameSceneChangedEventArgs() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameSceneChangedEventArgs"/> class.
        /// </summary>
        /// <param name="currentSceneIdentifier">
        /// Indicates the identifier of the current game scene after the change happened.
        /// </param>
        /// <param name="originalSceneIdentifier">
        /// Indicates the identifier of the original game scene before the change happened.
        /// </param>
        public GameSceneChangedEventArgs(GameSceneIdentifier currentSceneIdentifier,
            GameSceneIdentifier originalSceneIdentifier)
        {
            CurrentSceneIdentifier = currentSceneIdentifier;
            OriginalSceneIdentifier = originalSceneIdentifier;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or privately sets the identifier of the game scene after the change happened.
        /// </summary>
        /// <value>The identifier of the currnt scene.</value>
        public GameSceneIdentifier CurrentSceneIdentifier { get; private set; }

        /// <summary>
        /// Gets or privately sets the identifier of the game scene before the change happened.
        /// </summary>
        /// <value>The identifier of the original scene.</value>
        public GameSceneIdentifier OriginalSceneIdentifier { get; private set; }

        #endregion
    }
}