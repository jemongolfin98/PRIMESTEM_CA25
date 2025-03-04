using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class RSVPRegistration : MonoBehaviour
{
    [SerializeField] InputField feedback1;

    string URL = "https://docs.google.com/forms/u/1/d/e/1FAIpQLScT8aoM3y-KVmp1gKHgW98lGG4CSUouD0DPnK32nBKt4tq54Q/formResponse";
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Send()
    {
        StartCoroutine(Post(feedback1.text));
    }

    IEnumerator Post(string s1)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.1952130145", s1);




        UnityWebRequest www = UnityWebRequest.Post(URL, form);
        
        yield return www.SendWebRequest();

    }
}
