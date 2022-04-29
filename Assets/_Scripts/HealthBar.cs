using UnityEngine;
using UnityEngine.UI;

namespace _Scripts
{
    public class HealthBar : MonoBehaviour
    {
        private int health = 3;
        [SerializeField] private Image[] filledHearts;

        private void Start()
        {
            for (int i = 0; i < GameManager.Instance.CurrentLevelProgress.NoOfWrongAnswers; i++)
            {
                DecreaseHealth();
            }
        }

        private void DecreaseHealth()
        {
            if (health == 0) return;
            --health;
            filledHearts[health].enabled = false;
        }
    }
}