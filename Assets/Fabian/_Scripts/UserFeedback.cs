using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class UserFeedback : MonoBehaviour
{

    [SerializeField] InputField feedback1;

    string URL = "https://docs.google.com/forms/u/2/d/e/1FAIpQLSeG-zHYc-imlacHgnY9_Tez2a90IjGuYtwCxTEXSYbEUK7zew/formResponse";


    public void Send()
    {
        StartCoroutine(Post(feedback1.text));
    }

    IEnumerator Post(string s1)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.1869543954", s1);
        form.AddField("entry.1107065024", s1);
        form.AddField("entry.833734402", s1);




        UnityWebRequest www = UnityWebRequest.Post(URL, form);

        yield return www.SendWebRequest();

    }


}
