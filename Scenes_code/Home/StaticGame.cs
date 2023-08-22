using config;
using TMPro;
using UnityEngine;

public class StaticGame : MonoBehaviour
{
    private ConfigXML configXML = new ConfigXML();
    public TextMeshProUGUI txt;

    private void Awake ()
    {
        //DontDestroyOnLoad(this.gameObject);

        if (configXML.读配置项("Login", "playerinformation.xml") == "True")
        {
            txt.text = "UID:" + configXML.读配置项("UID", "playerinformation.xml");
        }
        else
        {
            txt.text = "本地用户";
        }
    }
}