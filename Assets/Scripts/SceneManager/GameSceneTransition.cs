using System;

namespace Assets.Scripts.SceneManager
{
    /// <summary>
    /// Represents the transition from one game scene to another.
    /// </summary>
    public class GameSceneTransition : IEquatable<GameSceneTransition>
    {
        #region Properties

        /// <summary>
        /// Gets (or privately sets) the GameState that can be transitioned from.
        /// </summary>
        /// <value>From.</value>
        public GameSceneIdentifier TransitionFrom { get; private set; }

        /// <summary>
        /// Gets (or privately sets) the GameState that can be transitioned to.
        /// </summary>
        /// <value>To.</value>
        public GameSceneIdentifier TransitionTo { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="GameSceneTransition"/> class from being created.
        /// </summary>
        private GameSceneTransition() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameSceneTransition"/> class.
        /// </summary>
        /// <param name="transitionFrom">The scene to transition from.</param>
        /// <param name="transitionTo">The scene to transition to.</param>
        public GameSceneTransition(GameSceneIdentifier transitionFrom, GameSceneIdentifier transitionTo)
        {
            TransitionFrom = transitionFrom;
            TransitionTo = transitionTo;
        }

        #endregion

        #region IEquatable<GameSceneTransition> Members

        /// <summary>
        /// Determines whether the specified <see cref="GameSceneTransition"/> is equal to the current <see cref="GameSceneTransition"/>.
        /// </summary>
        /// <param name="rule">The <see cref="GameSceneTransition"/> to compare with the current <see cref="GameSceneTransition"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="GameSceneTransition"/> is equal to the current
        /// <see cref="GameSceneTransition"/>; otherwise, <c>false</c>.</returns>
        public bool Equals(GameSceneTransition gameSceneTransition)
        {
            return (TransitionFrom == gameSceneTransition.TransitionFrom) &&
                    (TransitionTo == gameSceneTransition.TransitionTo);
        }

        #endregion
    }
}