using RuleEngine.Contracts;
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
        private IRuleEngine RuleEngine { get; set; }

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
        public GameSceneManager(IRuleEngine ruleEngine)
        {
            // Guard against a null RuleEngine
            if (ruleEngine == null) { throw new ArgumentNullException("ruleEngine"); }

            // Set the rule engine reference
            RuleEngine = ruleEngine;
        }

        #endregion
    }
}
