using UnityEngine;
using UnityEngine.UI;

namespace _Scripts
{
    public class PauseMenu : MonoBehaviour
    {
        // triggers on opening pause menu
        private void OnEnable()
        {
            Time.timeScale = 0f;
        }

        //triggers on closing pause menu
        private void OnDisable()
        {
            Time.timeScale = 1f;
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