using config;
using System.IO;
using System.Xml;
using UnityEngine;

public static class GameConfig
{
    private static readonly XmlDocument xmlDoc;
    private static readonly ConfigXML config = new ConfigXML();
    private static readonly string xmlFilePath;

    static GameConfig ()
    {
        xmlFilePath = Application.persistentDataPath + "/CORECONFIG.xml";
        xmlDoc = new XmlDocument();

        if (File.Exists(xmlFilePath))
        {
            xmlDoc.Load(xmlFilePath);
        }
        else
        {
            InitializeXml();
        }
    }

    private static void InitializeXml ()
    {
        // 初始化 XML 文档
        // 这里你可以设定一些默认值或者为空的键值对

        XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
        XmlNode rootNode = xmlDoc.CreateElement("CORECONFIG");
        xmlDoc.InsertBefore(xmlDeclaration, xmlDoc.DocumentElement);
        xmlDoc.AppendChild(rootNode);

        xmlDoc.Save(xmlFilePath);
    }

    public static string GetValue (string key, string path)
    {
        if (File.Exists($"{Application.persistentDataPath}/{path}"))//存在
        {
            return config.读配置项(key, path);
        }
        else
        {
            return "None";
        }
    }

    public static void SetValue (string key, string vlaue, string path)
    {
        if (File.Exists($"{Application.persistentDataPath}/{path}"))//存在
        {
            if (config.读配置项(key, path) == "None")
            {
                config.加入配置项(key, vlaue, path);
            }
            else
            {
                config.更新配置项(key, vlaue, path);
            }
        }
        else//不存在
        {
            config.创建配置文件(key, vlaue, path);
        }
    }

    public static string GetValue (string key)
    {
        XmlNodeList nodeList = xmlDoc.GetElementsByTagName(key);
        if (nodeList.Count > 0)
        {
            return nodeList[0].InnerText;
        }

        return "None";
    }

    public static void SetValue (string key, string value)
    {
        XmlNodeList nodeList = xmlDoc.GetElementsByTagName(key);

        if (nodeList.Count > 0)
        {
            nodeList[0].InnerText = value;
        }
        else
        {
            XmlNode rootNode = xmlDoc.DocumentElement;
            XmlNode newNode = xmlDoc.CreateElement(key);
            newNode.InnerText = value;
            rootNode.AppendChild(newNode);
        }
        xmlDoc.Save(xmlFilePath);
    }
}