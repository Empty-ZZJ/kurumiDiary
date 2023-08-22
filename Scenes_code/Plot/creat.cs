using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class creat : MonoBehaviour
{
    public GameObject plotchoice;
    public GameObject parentsobj;
    private GameObject clone;
    public int index = 1;
    public Sprite photo;

    private void Awake ()
    {
        if (File.Exists(Application.streamingAssetsPath + "/allplot.xml") == true)
        {
        }
        else
        {
            plotbutton.CreateXML("allplot", "allplot", "12", "allplot.xml");
            plotbutton.AddXML("藏宝图上", "plot/1-2", "allplot", "allplot.xml");
            plotbutton.AddXML("藏宝图下", "plot/1-3", "allplot", "allplot.xml");
            plotbutton.AddXML("藏宝图下", "plot/1-3", "allplot", "allplot.xml");
            plotbutton.AddXML("宠物篇", "plot/1-4", "allplot", "allplot.xml");
            plotbutton.AddXML("外婆篇", "plot/1-5", "allplot", "allplot.xml");
            plotbutton.AddXML("外婆篇下", "plot/1-6", "allplot", "allplot.xml");
            plotbutton.AddXML("打工篇", "plot/1-7", "allplot", "allplot.xml");
            plotbutton.AddXML("大扫除篇", "plot/1-8", "allplot", "allplot.xml");
            plotbutton.AddXML("奶茶篇", "plot/1-9", "allplot", "allplot.xml");
            plotbutton.AddXML("祭奠篇上", "plot/1-10", "allplot", "allplot.xml");
            plotbutton.AddXML("祭奠篇下", "plot/1-11", "allplot", "allplot.xml");
            plotbutton.AddXML("羁绊篇", "plot/1-12", "allplot", "allplot.xml");
        }
        string localPath = UnityEngine.Application.streamingAssetsPath + "/" + "allplot.xml";
        if (File.Exists(localPath))
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(localPath);
            XmlNodeList nodeList = xml.SelectSingleNode("root").ChildNodes;
            foreach (XmlElement xe in nodeList)
            {
            }
        }
    }

    private void Start ()
    {
        Create_newstory("藏宝图 中", "plot/1-2");
        Create_newstory("藏宝图 下", "plot/1-3");
        Create_newstory("宠物篇", "plot/1-4");
        Create_newstory("外婆篇", "plot/1-5");
        Create_newstory("外婆篇•下", "plot/1-6");
        Create_newstory("打工篇", "plot/1-7");
        Create_newstory("大扫除篇", "plot/1-8");
        Create_newstory("奶茶篇", "plot/1-9");
        Create_newstory("祭典篇•上", "plot/1-10");
        Create_newstory("祭典篇•下", "plot/1-11");
        Create_newstory("羁绊篇", "plot/1-12");
        {
            add_sotry("除夕特别篇 上", "plot/small_2267", "true");
            add_sotry("除夕特别篇 中", "plot/2278", "false");
            add_sotry("除夕特别篇 下", "plot/small_2238", "false");
        }
    }

    public void Create_newstory (string title, string file)
    {
        if (index == 0)
        {
            index = 1;
        }
        index++;
        var photo = Resources.Load<Sprite>(file);
        GetComponent<RectTransform>().sizeDelta = new Vector2(index * 250, 12.9f);
        clone = Instantiate(plotchoice, plotchoice.transform.parent);
        clone.gameObject.GetComponentInChildren<Text>().text = title;
        clone.transform.name = "choicewhat" + index.ToString();
        GameObject photobj = GameObject.Find("Canvas/Horizontal Scroll View/Viewport/Content/choicewhat" + index.ToString() + "/plotphoto");
        GameObject obj_lock = GameObject.Find("Canvas/Horizontal Scroll View/Viewport/Content/choicewhat" + index.ToString() + "/lock");
        photobj.gameObject.GetComponent<Image>().sprite = photo;

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
        else
        {
            plotbutton.CreateXML("plot" + index.ToString(), "plot" + index.ToString(), "false", "plotlock.xml");
            obj_lock.SetActive(true);
            //Debug.Log(clone.transform.name);
            clone.GetComponent<Button>().interactable = false;
        }
    }

    public void add_sotry (string title, string file, string bool_lock)
    {
        if (index == 0)
        {
            index = 1;
        }
        index++;
        var photo = Resources.Load<Sprite>(file);
        GetComponent<RectTransform>().sizeDelta = new Vector2(index * 250, 12.9f);
        clone = Instantiate(plotchoice, plotchoice.transform.parent);
        clone.gameObject.GetComponentInChildren<Text>().text = title;
        clone.transform.name = "choicewhat" + index.ToString();
        GameObject photobj = GameObject.Find("Canvas/Horizontal Scroll View/Viewport/Content/choicewhat" + index.ToString() + "/plotphoto");
        GameObject obj_lock = GameObject.Find("Canvas/Horizontal Scroll View/Viewport/Content/choicewhat" + index.ToString() + "/lock");
        photobj.gameObject.GetComponent<Image>().sprite = photo;
        if (File.Exists(Application.persistentDataPath + "/plotlock.xml") == true)
        {
            string temp = plotbutton.ReadXML("plot" + index.ToString(), "plot" + index.ToString(), "plotlock.xml");
            Debug.Log(temp);
            if (temp == "None")
            {
                plotbutton.AddXML("plot" + index.ToString(), bool_lock, "plot" + index.ToString(), "plotlock.xml");
                if (bool_lock == "false")
                {
                    obj_lock.SetActive(true);
                    clone.GetComponent<Button>().interactable = false;
                }
                return;
            }
            else if (temp == "false")
            {
                obj_lock.SetActive(true);
                //Debug.Log(clone.transform.name);
                clone.GetComponent<Button>().interactable = false;
            }
            else if (temp == "true")
            {
            }
        }
    }
}