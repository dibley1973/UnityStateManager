using Assets.Scripts.StateManager;
using Dibware.UnityStateManager.Assets.Scripts.Resources;
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
        /// Holds the static game manager instance.
        /// </summary>
        private static GameManager _gameManagerInstance = null;

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
                return _gameManagerInstance != null;
            }
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static GameManager GameManager
        {
            get
            {
                // First check if we have a reference to the instance has...
                if (State._gameManagerInstance == null)
                {
                    // ... we dont, so see if we can get one if one exists... 
                    _gameManagerInstance = Object.FindObjectOfType(typeof(GameManager)) as GameManager;

                    // ... check again if we now have an instance...
                    if (_gameManagerInstance == null)
                    {
                        _gameManagerInstance = GetNewGameManagerInstance();
                    }
                }

                // Finally return the instance
                return State._gameManagerInstance;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a new instance of the game amanger.
        /// </summary>
        /// <returns>
        /// The new instance.
        /// </returns>
        static GameManager GetNewGameManagerInstance()
        {
            // ... we still dont so we need to create one
            GameObject gameObject = new GameObject(StateManagerKeys.GameManger);

            // We need to ensure that the instance is not destroyed when 
            // loading or changing to a new Scene
            DontDestroyOnLoad(gameObject);

            // Tie the instance of the GameManager to the game object
            _gameManagerInstance = gameObject.AddComponent<GameManager>();



            // Finally return the instance
            return _gameManagerInstance;
        }

        #endregion
    }
}
