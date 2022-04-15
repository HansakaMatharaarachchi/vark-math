using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFlower : MonoBehaviour
{
    // Start is called before the first frame update
   
        public Transform[] spawnPoints;
        public GameObject[] balloons;
        void Start()
        {
            StartCoroutine(StartSpawning());

        }

        IEnumerator StartSpawning()
        {
            yield return new WaitForSeconds(5);
            for (int i = 0; i <  8; i++)
            {
            Instantiate(balloons[i], spawnPoints[i].position, Quaternion.identity);
            }
        
         

        }









    }
 

