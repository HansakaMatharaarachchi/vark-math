using UnityEngine;
using UnityEngine.UI;

namespace _Scripts
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private Button settingsBtn;
        [SerializeField] private Button exitBtn;

        // triggers on opening pause menu
        public void OnEnable()
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
            //todo assign
        }

        public void ExitLevel()
        {
            GameManager.Instance.LoadScene(1);
        }
    }
}