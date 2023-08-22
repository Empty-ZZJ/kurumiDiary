using UnityEngine;
using UnityEngine.UI;

public class windows : MonoBehaviour
{
    public Text text_var;

    private void Awake ()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Application.targetFrameRate = 60;
        text_var.text = "Ver:" + Application.version.ToString();
    }
}