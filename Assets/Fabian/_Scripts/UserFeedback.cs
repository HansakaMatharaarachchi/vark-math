using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class UserFeedback : MonoBehaviour
{

    [SerializeField] InputField feedback1;

    string URL = "https://docs.google.com/forms/d/e/1FAIpQLScp7675PQbWtU58yemTK-Vji9juDG4VGZ4v8Wlb7onmpJOt3g/viewform?usp=sf_link";


    public void Send()
    {
        StartCoroutine(Post(feedback1.text));
    }

    IEnumerator Post(string s1)
    {
        WWWForm form = new WWWForm();
        // form.AddField("entry.1869543954_sentinel", s1);
        // form.AddField("entry.1107065024_sentinel", s1);
         form.AddField("entry.516175595", s1);




        UnityWebRequest www = UnityWebRequest.Post(URL, form);

        yield return www.SendWebRequest();

    }


}
