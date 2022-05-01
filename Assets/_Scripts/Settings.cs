using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts
{
    public class Settings : MonoBehaviour
    {
        [SerializeField] private Slider volumeSlider;
        [SerializeField] private TMP_Dropdown lSChangerDropdown;
        [SerializeField] private TMP_Text userName;

        private void Start()
        {
            // gets the current volume level
            volumeSlider.value = GameManager.Instance.AudioManager.GetVolume();
            // update the volume level accordingly
            volumeSlider.onValueChanged.AddListener(delegate
            {
                GameManager.Instance.AudioManager.ChangeVolume(volumeSlider.value);
            });

            if (GameManager.Instance.IsSignedIn)
            {
                userName.text = GameManager.Instance.Player.Name;
                lSChangerDropdown.value = GameManager.Instance.Player.LearningStyle switch
                {
                    LearningStyle.Visual => 0,
                    LearningStyle.Auditory => 1,
                    LearningStyle.Kinesthetic => 2,
                    _ => lSChangerDropdown.value
                };
                lSChangerDropdown.onValueChanged.AddListener(ValueChangedHandler);
            }
            else
            {
                GameObject[] itemsToHide = GameObject.FindGameObjectsWithTag("SignedInSettings");

                foreach (GameObject obj in itemsToHide)
                {
                    obj.SetActive(false);
                }
            }
        }

        // update the player learningStyle accordingly
        private void ValueChangedHandler(int value)
        {
            GameManager.Instance.Player.LearningStyle = value switch
            {
                0 => LearningStyle.Visual,
                1 => LearningStyle.Auditory,
                2 => LearningStyle.Kinesthetic,
                _ => GameManager.Instance.Player.LearningStyle
            };
        }

        private void OnDisable()
        {
            Destroy(gameObject);
        }

        public void SignOutOnClick()
        {
            GameManager.Instance.SignOut();
        }
    }
}