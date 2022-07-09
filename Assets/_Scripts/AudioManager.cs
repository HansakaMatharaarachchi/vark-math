using UnityEngine;

namespace _Scripts
{
    public class AudioManager
    {
        public AudioManager()
        {
            if (PlayerPrefs.HasKey("musicVolume"))
            {
                Load();
            }
            else
            {
                PlayerPrefs.SetFloat("musicVolume", 0.80f);
                Load();
            }
        }

        public void ChangeVolume(float value)
        {
            AudioListener.volume = value;
            Save(value);
        }

        public float GetVolume()
        {
            return PlayerPrefs.GetFloat("musicVolume");
        }

        private void Load()
        {
            AudioListener.volume = PlayerPrefs.GetFloat("musicVolume");
        }

        private void Save(float value)
        {
            PlayerPrefs.SetFloat("musicVolume", value);
        }

        public void PauseAudio(bool state)
        {
            AudioListener.pause = state;
        }
    }
}