using UnityEngine;

public class carema : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool todayusual;

    public GameObject maincamera;
    public GameObject close;
    public static bool nowmamera;

    private void Start ()
    {
        /*
        if (File.Exists(Application.persistentDataPath + "/NoviceTutorial.xml") == false)
        {
            return;
        }
        if (Timelight.getup == false)
        {
            return;
        }
        Debug.Log(System.DateTime.Now.Day.ToString());
        /*
        if(File.Exists(Application.persistentDataPath+ "/usual.xml")==false)
        {
            plotbutton.CreateXML("usual", "usual", "now"+System.DateTime.Now.Day.ToString(), "usual.xml");
            todayusual = false;
        }
        else
        {
            string[] tempa=new string[1];
            string tempstring= plotbutton.ReadXML("usual", "usual", "usual.xml");

            tempa = tempstring.Split('w');
            Debug.Log(tempa[1]);
            if (tempa[1] == System.DateTime.Now.Day.ToString())
            {
                todayusual = true;
                plotbutton.UpdateXml("usual", plotbutton.ReadXML("usual", "usual", "usual.xml"), "now" + System.DateTime.Now.Day.ToString(), "usual.xml");
            }
            else
            {
                todayusual = false;
                plotbutton.UpdateXml("usual", plotbutton.ReadXML("usual", "usual", "usual.xml"), "now" + System.DateTime.Now.Day.ToString(), "usual.xml");
            }
        }
        if (todayusual==false)
        {
            maincamera.transform.DOMove(new Vector3(1.41400003f, 0.925999999f, 4.67799997f), 2);
            mouse_move2.bool_move = false;
            close.SetActive(true);
            nowmamera = true;
        }
        */
    }
}