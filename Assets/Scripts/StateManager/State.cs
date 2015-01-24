using Assets.Scripts.SceneManager;
using Dibware.UnityStateManager.Assets.Scripts.Resources;
using Dibware.UnityStateManager.Assets.Scripts.SceneManager;
using RuleEngine.Contracts;
using RuleEngine.Engine;
using UnityEngine;

namespace Dibware.UnityStateManager.Assets.Scripts.StateManager
{
    /// <summary>
    /// The State object follows a singleton pattern to ensure there is only ever 
    /// one instant of any of the StateManagers
    /// </summary>
    public class State : MonoBehaviour
    {
        #region Fields

        /// <summary>
        /// Holds the static instance.
        /// </summary>
        private static State _instance = null;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="State"/> class.
        /// </summary>
        protected State() { }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether this <see cref="State"/> is active.
        /// </summary>
        /// <value><c>true</c> if is active; otherwise, <c>false</c>.</value>
        static public bool IsActive
        {
            get
            {
                return _instance != null;
            }
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static State Instance
        {
            get
            {
                // First check if we have a reference to the instance has...
                if (State._instance == null)
                {
                    // ... we dont, so see if we can get one if one exists... 
                    _instance = Object.FindObjectOfType(typeof(State)) as State;

                    // ... check again if we now have an instance...
                    if (_instance == null)
                    {
                        _instance = GetNewInstance();
                    }
                }

                // Finally return the instance
                return State._instance;
            }
        }

        /// <summary>
        /// Gets or sets the game scene manager.
        /// </summary>
        private GameSceneManager SceneManager { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a new instance.
        /// </summary>
        /// <returns>
        /// The new instance.
        /// </returns>
        static State GetNewInstance()
        {
            // ... we still dont so we need to create one
            GameObject gameObject = new GameObject(StateManagerKeys.GameManger);

            // We need to ensure that the instance is not destroyed when 
            // loading or changing to a new Scene
            DontDestroyOnLoad(gameObject);

            // Tie the instance of the GameManager to the game object
            _instance = gameObject.AddComponent<State>();

            // Create a new SceneManager and give it to the instance to use
            _instance.SceneManager = CreateSceneManager();

            // Finally return the instance
            return _instance;
        }

        /// <summary>
        /// Creates the scene manager.
        /// </summary>
        /// <returns></returns>
        private static GameSceneManager CreateSceneManager()
        {
            // Create a new rule engine
            RuleEngine<GameSceneTransition> ruleEngine = new RuleEngine<GameSceneTransition>();

            // Add the rules
            // TODO: AddGameSceneTransitionRules(ruleEngine);

            // Create the scene manager with the rule engine
            return new GameSceneManager(ruleEngine as IRuleEngine);
        }

        ///// <summary>
        ///// Adds the game scene transition rules to the specifed rule engine.
        ///// </summary>
        ///// <param name="ruleEngine">The rule engine to add rules to.</param>
        //private static void AddGameSceneTransitionRules(RuleEngine<GameSceneTransition> ruleEngine)
        //{
        //    // Create the game scene transition
        //    GameSceneTransition nullToIntro = new GameSceneTransition(GameScene.NullScene, GameScene.Intro);
        //    GameSceneTransition introToMainMenu = new GameSceneTransition(GameScene.Intro, GameScene.MainMenu);
        //    GameSceneTransition mainMenuToNewGame = new GameSceneTransition(GameScene.MainMenu, GameScene.NewGame);

        //    // Create the rules
        //    var transitionNullToIntro = new SceneTransitionRule(nullToIntro);
        //    var transitionIntroToMainMenu = new SceneTransitionRule(introToMainMenu);
        //    var transitionMainMenuToNewGame = new SceneTransitionRule(mainMenuToNewGame);

        //    sceneTransitionRuleEngine.Add(transitionNullToIntro);
        //    sceneTransitionRuleEngine.Add(transitionIntroToMainMenu);
        //    sceneTransitionRuleEngine.Add(transitionMainMenuToNewGame);
        //}

        #endregion
    }
}
