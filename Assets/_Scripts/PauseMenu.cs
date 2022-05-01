using System;
using UnityEngine;

namespace _Scripts
{
    public class PauseMenu : MonoBehaviour
    {
        // triggers on opening pause menu
        private void OnEnable()
        {
            Time.timeScale = 0f;
            GameManager.Instance.AudioManager.PauseAudio(true);
            // pause animations
            Animator[] allAnims = FindObjectsOfType<Animator>(true);
            if (allAnims.Length != 0)
            {
                foreach( var anim in allAnims )
                {
                    anim.speed = 0;
                }
            }
        }

        //triggers on closing pause menu
        private void OnDisable()
        {
            Time.timeScale = 1f;
            GameManager.Instance.AudioManager.PauseAudio(false);
            // resume animations
            Animator[] allAnims = FindObjectsOfType<Animator>(true);
            if (allAnims.Length != 0)
            {
                foreach( var anim in allAnims )
                {
                    anim.speed = 1;
                }
            }
        }

        public void OpenSettings()
        {
            GameManager.Instance.OpenSettings();
        }

        public void ExitLevel()
        {
            GameManager.Instance.LoadScene(1);
        }
    }
}