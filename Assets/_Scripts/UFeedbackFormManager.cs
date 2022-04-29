using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using _Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UFeedbackFormManager : MonoBehaviour
{
    [SerializeField] private ToggleGroup likeTheAppToggleGroup;
    [SerializeField] private ToggleGroup ratingOfTheAppToggleGroup;
    [SerializeField] private TMP_InputField suggestionsInputField;
    
    [SerializeField] private Button submitBtn;

    [SerializeField] private GameObject thankYouPanel;

    //Google Form URL
    private const string FormURL = "https://docs.google.com/forms/d/e/1FAIpQLSf3EOhocJHrPaTXXvSVxmOpmJQmOqQbhrJYmpCxpv2p4ZZasw/formResponse";

    private void Update()
    {
        submitBtn.interactable = (likeTheAppToggleGroup.GetFirstActiveToggle() != null) &&
                                 (ratingOfTheAppToggleGroup.GetFirstActiveToggle() != null) &&
                                 (!string.IsNullOrWhiteSpace(suggestionsInputField.text));
    }

    // changes the value of given form entry ID 
    IEnumerator PostResponses()
    {
        WWWForm form = new WWWForm();
        
        //Assigns selected values to relevant fields of the form
        form.AddField("entry.1601340890",likeTheAppToggleGroup.GetFirstActiveToggle().name);
        form.AddField("entry.1791367065", ratingOfTheAppToggleGroup.GetFirstActiveToggle().name);
        form.AddField("entry.624070535",suggestionsInputField.text);
        
        UnityWebRequest www = UnityWebRequest.Post(FormURL, form);
        yield return www.SendWebRequest();
    }

    public async void SendResponses()
    {
        StartCoroutine(PostResponses());
        thankYouPanel.SetActive(true);
        await Task.Delay(2000);
        thankYouPanel.SetActive(false);
    }

    public void BackToLobby()
    {
        GameManager.Instance.LoadScene(1);
    }
}