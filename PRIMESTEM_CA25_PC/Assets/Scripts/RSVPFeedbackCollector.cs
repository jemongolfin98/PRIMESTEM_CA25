using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RSVPFeedbackCollector : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI txtData;
    [SerializeField] private UnityEngine.UI.Button btnSubmit;
    [SerializeField] private CollectionOption option;

    private enum CollectionOption { openEmailClient, openGFormLink, sendGFormData };

    private const string kRecieverEmailAddress = "je425824@ucf.edu";

    private const string kGFormBaseURL = "https://docs.google.com/forms/d/e/1FAIpQLScT8aoM3y-KVmp1gKHgW98lGG4CSUouD0DPnK32nBKt4tq54Q/";
    private const string kGFormEntryID01 = "1952130145";
    private const string kGFormEntryID02 = "1874571834";
    private const string kGFormEntryID03 = "1061231665";
    
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Assertions.Assert.IsNotNull( txtData );
        UnityEngine.Assertions.Assert.IsNotNull( btnSubmit );
        btnSubmit.onClick.AddListener( delegate {
            switch (option) {
                case CollectionOption.openEmailClient:
                    OpemEmailClient( txtData.text );
                    break;
                case CollectionOption.openGFormLink:
                    OpenGFormLink();
                    break;
                case CollectionOption.sendGFormData:
                    StartCoroutine( SendFormData (txtData.txt ));
                    break;
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private static void OpenEmailClient( string RSVP )
    {
        string email = kRecieverEmailAddress;
        string subject = "RSVP";
        string body = "<" + rsvp + ">";
        OpenLink( "mailto:" + email + "?subject=" + subject + "&body=" + body );
    }

    private static void OpenGFormLink()
    {
        string urlGFormView = kGFormBaseURL + "viewform";
        OpenLink( urlGFormView);
    }

    private static IEnumerator SendGFormData<T>( T dataContainer )
    {
        bool isString = dataContainer is string;
        string jsonData = isString ? dataContainer.ToString() : JsonUtility.ToJson(dataContainer);

        WWWForm form = new WWWWForm();
        form.AddField( kGFormEntryID01, jsonData );
        form.AddField( kGFormEntryID02, jsonData );
        form.AddField( kGFormEntryID03, jsonData );
        string urlGFormResponse = kGFormBaseURL + "formResponse";
        using ( UnityWebRequest www = UnityWebRequest.Post( urlGFormResponse, form ))
        {
            yield return www.SendWebRequest();
        }
    }

    // We cannot have spaces in links for iOS
    public static void OpenLink( string link )
    {
        bool googleSearch = link.Contains( "google.com/search" );
        string linkNoSpaces = link.Replace( " ", googleSearch ? "+" : "%20");
        Application.OpenURL( linkNoSpaces );
    }
}
