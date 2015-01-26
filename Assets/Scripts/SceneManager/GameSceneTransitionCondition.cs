using RuleEngine.Base;

namespace Assets.Scripts.SceneManager
{
    /// <summary>
    /// Represents a condition that must be met for game scene transition
    /// </summary>
    internal class GameSceneTransitionCondition : BaseCondition<GameSceneTransition>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GameSceneTransitionCondition"/> class.
        /// </summary>
        /// <param name="threshold">The threshold.</param>
        public GameSceneTransitionCondition(GameSceneTransition threshold)
            : base(threshold) { }

        #endregion

        #region ICondition<int> Members

        /// <summary>
        /// Determines whether this instance is satisfied.
        /// </summary>
        /// <returns></returns>
        public override bool IsSatisfied
        {
            get { return Value.Equals(Threshold); }
        }

        #endregion
    }
}
