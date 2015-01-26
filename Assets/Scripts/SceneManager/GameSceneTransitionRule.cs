using Assets.Scripts.SceneManager;
using RuleEngine.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.SceneManager
{
    /// <summary>
    /// Represents a rule that must be met for game scene transition
    /// </summary>
    internal class GameSceneTransitionRule : BaseRule<GameSceneTransition>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GameSceneTransitionRule"/> class.
        /// </summary>
        /// <param name="threshold">The threshold.</param>
        /// <param name="actual">The actual.</param>
        public GameSceneTransitionRule(GameSceneTransition threshold)
            : base(threshold)
        {
            Initialize();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the instance.
        /// </summary>
        public override void Initialize()
        {
            // Clear any existing conditions
            Conditions.Clear();

            // Create our conditions
            var condition1 = new GameSceneTransitionCondition(Threshold);

            // ...and add them to our collection of conditions
            Conditions.Add(condition1);
        }

        /// <summary>
        /// Matches the conditions.
        /// </summary>
        /// <returns></returns>
        public override bool MatchConditions()
        {
            return base.MatchAllConditions();
        }

        #endregion
    }
}