using config;
using UnityEngine;

public class plotlock : MonoBehaviour
{
    private ConfigXML configXML = new ConfigXML();

    private void Awake ()
    {
    }

    /*
    public static bool findlock(string plotname)
    {
        if (File.Exists(Application.persistentDataPath + "/plotlock.xml") == true)
        {
            string temp = plotbutton.ReadXML("plot" + index.ToString(), "plot" + index.ToString(), "plotlock.xml");
            Debug.Log(temp);
            if (temp == "None")
            {
                plotbutton.AddXML("plot" + index.ToString(), "false", "plot" + index.ToString(), "plotlock.xml");
                obj_lock.SetActive(true);
                clone.GetComponent<Button>().interactable = false;
            }
            else if (temp == "false")
            {
                obj_lock.SetActive(true);
                //Debug.Log(clone.transform.name);
                clone.GetComponent<Button>().interactable = false;
            }
        }
    */

    public void updatelock (string plotname, bool state)
    {
        if (state == true)
        {
            configXML.更新配置项(plotname, "true", "plotlock.xml");
            return;
        }
        configXML.更新配置项(plotname, "false", "plotlock.xml");
    }
}