using Assets.Scripts.SceneManager;
using Dibware.UnityStateManager.Assets.Scripts.StateManager;
using UnityEngine;

namespace Assets.Scripts.Scenes
{
    public class Initialize : MonoBehaviour
    {
        // Use this for initialization
        void Start()
        {
            State.GameManager.SceneManager.SetGameScene(GameSceneIdentifier.Intro);
        }
    }
}