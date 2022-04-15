using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_V_Manager : MonoBehaviour
{
    public GameObject[] panelArray;
    
    
    
    


    // Start is called before the first frame update
    void Start()
    {
        panelArray[0].SetActive(true);
    }

    public void Clickbutton_1()
    {
        panelArray[1].SetActive(false);
        panelArray[0].SetActive(false);

    }

    public void Clickbutton_2()
    {
        panelArray[4].SetActive(false);
        panelArray[1].SetActive(true);
        panelArray[2].SetActive(true);
    }

    public void next_btn()
    {
        panelArray[2].SetActive(false);
        panelArray[3].SetActive(true);
        
    }

    public void clickbutton_3()
    {
        SceneManager.LoadScene(2);
    }
}
