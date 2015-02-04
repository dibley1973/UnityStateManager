using Assets.Scripts.SceneManager;
using Dibware.UnityStateManager.Assets.Scripts.StateManager;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Scenes
{
    public class Intro : MonoBehaviour
    {
        // Use this for initialization
        void Start()
        {
            StartCoroutine(DelayMethod(5.0f));

            //3 seconds later
            // Change to main menu
            State.GameManager.SceneManager.SetGameScene(GameSceneIdentifier.MainMenu);
        }


        IEnumerator DelayMethod(float delay)
        {
            yield return new WaitForSeconds(delay);
        }
    }
}
