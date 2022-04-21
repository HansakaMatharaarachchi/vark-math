using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using _Scripts;
using UnityEngine;
using UnityEngine.UI;

public class FuelBar : MonoBehaviour
{
    private float fuelAmount;
    private readonly float maxFuelValue = 1.0f;
    private float lerpSpeed;

    [SerializeField] private Image fuelBar;

    void Start()
    {
        for (int i = 0; i < GameManager.Instance.currentLevelProgress.noOfCorrectAnswers; i++)
        {
            Fill();
        }
    }
    void Update()
    {
        lerpSpeed = 3f * Time.deltaTime;
        FuelBarFiller();
        ColorChanger();
    }

    void FuelBarFiller()
    {
        fuelBar.fillAmount = Mathf.Lerp(fuelBar.fillAmount, (fuelAmount / maxFuelValue), lerpSpeed);
    }

    void ColorChanger()
    {
        Color healthColor = Color.Lerp(Color.red, Color.green, (fuelAmount / maxFuelValue));
        fuelBar.color = healthColor;
    }

    // public void Reduce()
    // {
    //     if (fuelAmount > 0)
    //         fuelAmount -= maxFuelValue / 3;
    // }

    public void Fill()
    {
        if (fuelAmount < maxFuelValue)
            fuelAmount += maxFuelValue / 3;
    }
}