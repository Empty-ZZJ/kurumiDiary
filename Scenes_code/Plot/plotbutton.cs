using config;
using System.Collections;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class plotbutton : MonoBehaviour
{
    private ConfigXML configXML = new ConfigXML();

    // Start is called before the first frame update
    private AsyncOperation operation;

    public GameObject message;

    public GameObject mask;
    public GameObject list;
    public GameObject right_txt;
    public GameObject right_detail;
    public GameObject right_photo;
    public GameObject lock1;
    public GameObject lock2;
    public GameObject lock1_txt;
    public GameObject lock2_txt;

    public void closelist ()
    {
        UIAnimate.ToButtom(list);
        mask.SetActive(false);
        lock1.SetActive(false);
        lock2.SetActive(false);
    }

    public void onda ()
    {
        int i;
        string[] qwe = new string[5];
        string temptxt = this.transform.name;
        qwe = temptxt.Split('t');
        int.TryParse(qwe[1], out i);
        if (i > 1 && i < 13)
        {
            UIAnimate.ToMiddle(message);
            return;
        }
        mask.SetActive(true);
        UIAnimate.ToMiddle(list);
        right_txt.GetComponent<Text>().text = GameObject.Find("Canvas/Horizontal Scroll View/Viewport/Content/choicewhat" + i.ToString() + "/title").GetComponent<Text>().text;
        right_detail.GetComponent<Text>().text = configXML.读配置项_StreamingAssets("plot" + i.ToString(), "config/plot/plotdetail.xml");
        right_photo.GetComponent<Image>().sprite = GameObject.Find("Canvas/Horizontal Scroll View/Viewport/Content/choicewhat" + i.ToString() + "/plotphoto").GetComponent<Image>().sprite;
        if (File.Exists(Application.persistentDataPath + "/plotdone.xml") == true)
        {
            string temp = configXML.读配置项("plot" + i.ToString(), "plotdone.xml");
            if (temp == "None")
            {
                configXML.加入配置项("plot" + i.ToString(), "false", "plotdone.xml");
            }
            else if (temp == "true")
            {
                lock1.SetActive(true);
                lock2.SetActive(true);
            }
        }
        else
        {
            configXML.创建配置文件("plot" + i.ToString(), "false", "plotdone.xml");
        }

        if (File.Exists(Application.persistentDataPath + "/plotset.xml") == false)
        {
            configXML.创建配置文件("nowplot", i.ToString(), "plotset.xml");
        }
        else
            configXML.更新配置项("nowplot", i.ToString(), "plotset.xml");
        string[] tempprize = new string[5];
        string temp2;
        temp2 = configXML.读配置项_StreamingAssets("plot" + i.ToString(), "config/plot/plotprize.xml");
        tempprize = temp2.Split("|");
        {
            lock1_txt.GetComponent<Text>().text = "钻石×" + tempprize[0];
            lock2_txt.GetComponent<Text>().text = "好感度×" + tempprize[1];
        }
    }

    public void plot_run ()
    {
        StartCoroutine(start_game());
    }

    public static void CreateXML (string chilenode, string a1, string a2, string alie)
    {
        string localPath = UnityEngine.Application.persistentDataPath + "/" + alie;
        if (!File.Exists(localPath))
        {
            XmlDocument xml = new XmlDocument();
            XmlDeclaration xmldecl = xml.CreateXmlDeclaration("1.0", "UTF-8", "");//设置xml文件编码格式为UTF-8
            XmlElement root = xml.CreateElement("root");//创建根节点
            XmlElement info = xml.CreateElement(chilenode);//创建子节点
            info.SetAttribute(a1, a2);//创建子节点属性名和属性值
            root.AppendChild(info);//将子节点按照创建顺序，添加到xml
            xml.AppendChild(root);
            xml.Save(localPath);//保存xml到路径位置
        }
    }

    public static void AddXML (string a1, string a2, string newson, string alie)
    {
        string localPath = UnityEngine.Application.persistentDataPath + "/" + alie;
        if (File.Exists(localPath))
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(localPath);//加载xml文件
            XmlNode root = xml.SelectSingleNode("root");//获取根节点
            XmlElement info = xml.CreateElement(newson);//创建新的子节点
            info.SetAttribute(a1, a2);//创建新子节点属性名和属性值
            root.AppendChild(info);//将子节点按照创建顺序，添加到xml
            xml.AppendChild(root);
            xml.Save(localPath);//保存xml到路径位置
        }
    }

    public static string ReadXML (string chilenode, string want, string alie)
    {
        string localPath = UnityEngine.Application.persistentDataPath + "/" + alie;
        if (File.Exists(localPath))
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(localPath);//加载xml
            XmlNodeList nodeList = xml.SelectSingleNode("root").ChildNodes;
            foreach (XmlElement xe in nodeList)
            {//遍历子节点
                if (xe.Name == chilenode)
                {
                    return (xe.GetAttribute(want));
                }
            }
        }
        return ("None");
    }

    public static string ReadXML_streaming (string chilenode, string want, string alie)
    {
        string localPath = UnityEngine.Application.streamingAssetsPath + "/" + alie;
        if (File.Exists(localPath))
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(localPath);//加载xml
            XmlNodeList nodeList = xml.SelectSingleNode("root").ChildNodes;
            foreach (XmlElement xe in nodeList)
            {//遍历子节点
                if (xe.Name == chilenode)
                {
                    return (xe.GetAttribute(want));
                }
            }
        }
        return ("None");
    }

    public static void UpdateXml (string a, string b, string c, string alie)
    {
        string localPath = UnityEngine.Application.persistentDataPath + "/" + alie;
        if (File.Exists(localPath))
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(localPath);
            XmlNodeList nodeList = xmlDoc.SelectSingleNode("root").ChildNodes;
            foreach (XmlElement xe in nodeList)
            {
                //拿到节点中属性Name =小王的节点
                if (xe.GetAttribute(a) == b)
                {
                    //更新节点属性
                    xe.SetAttribute(a, c);
                    break;
                }
            }
            xmlDoc.Save(localPath);
        }
    }

    private IEnumerator start_game ()
    {
        operation = SceneManager.LoadSceneAsync(3);
        yield return operation;
    }
}