using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckLoopAudio : MonoBehaviour
{
    // Start is called before the first frame update
 
        public AudioSource audioSource;
        public float delay = 4;
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            Playt();
        }
    }
        void Playt()
        {
            // Plays an Audio Clip after 4 seconds
             
                audioSource.PlayDelayed(delay);
        
        }
    
    

}

