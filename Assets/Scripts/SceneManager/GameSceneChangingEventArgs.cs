using System;

namespace Assets.Scripts.SceneManager
{
    /// <summary>
    /// Contains game scene changing event data.
    /// </summary>
    public class GameSceneChangingEventArgs : EventArgs
    {
        #region Constructors

        /// <summary>
        /// Hides the default constructor for the <see cref="GameSceneChangingEventArgs"/> class.
        /// </summary>
        private GameSceneChangingEventArgs() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameSceneChangingEventArgs"/> class.
        /// </summary>
        /// <param name="currentSceneIdentifier">
        /// Indicates the identifier of the current scene before the scene change.
        /// </param>
        /// <param name="prospectiveSceneIdentifier">
        /// Indicates identifier of what the new scene will be after the change.
        /// </param>
        internal GameSceneChangingEventArgs(GameSceneIdentifier currentSceneIdentifier,
            GameSceneIdentifier prospectiveSceneIdentifier)
        {
            CurrentSceneIdentifier = currentSceneIdentifier;
            ProspectiveSceneIdentifier = prospectiveSceneIdentifier;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether a request to cancel the scene change has been made.
        /// </summary>
        /// <value><c>true</c> if this instance cancel; otherwise, <c>false</c>.</value>
        public bool Cancel { get; set; }

        /// <summary>
        /// Gets or privately sets the identifier of the current game scene before the change happens.
        /// </summary>
        /// <value>
        /// The identifier of the current game scene before the change happens.
        /// </value>
        public GameSceneIdentifier CurrentSceneIdentifier { get; private set; }

        /// <summary>
        /// Gets or privately sets the identifier of the prospective game scene if the change is allowed to happen.
        /// </summary>
        /// <value>
        /// The identifier of the prospective game scene if the change is allowed to happen.
        /// </value>
        public GameSceneIdentifier ProspectiveSceneIdentifier { get; private set; }

        #endregion
    }
}