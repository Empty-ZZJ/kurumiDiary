using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.Networking;

namespace config
{
    //ConfigXML configXML = new ConfigXML();
    public class ConfigXML
    {
        public void 创建配置文件 (string 节点名, string 写入值, string 文件名)
        {
            string localPath = UnityEngine.Application.persistentDataPath + "/" + 文件名;
            if (!File.Exists(localPath))
            {
                XmlDocument xml = new XmlDocument();
                XmlDeclaration xmldecl = xml.CreateXmlDeclaration("1.0", "UTF-8", "");
                XmlElement root = xml.CreateElement("root");
                XmlElement info = xml.CreateElement(节点名);
                info.SetAttribute(节点名, 写入值);
                root.AppendChild(info);
                xml.AppendChild(root);
                xml.Save(localPath);
            }
        }

        public void 加入配置项 (string 节点名, string 写入值, string 文件名)
        {
            string localPath = UnityEngine.Application.persistentDataPath + "/" + 文件名;
            if (File.Exists(localPath))
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(localPath);
                XmlNode root = xml.SelectSingleNode("root");
                XmlElement info = xml.CreateElement(节点名);
                info.SetAttribute(节点名, 写入值);
                root.AppendChild(info);
                xml.AppendChild(root);
                xml.Save(localPath);
            }
        }

        public string 读配置项 (string 欲读取节点, string 文件名)
        {
            string localPath = UnityEngine.Application.persistentDataPath + "/" + 文件名;
            if (File.Exists(localPath))
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(localPath);
                XmlNodeList nodeList = xml.SelectSingleNode("root").ChildNodes;
                foreach (XmlElement xe in nodeList)
                {
                    if (xe.Name == 欲读取节点)
                    {
                        return (xe.GetAttribute(欲读取节点));
                    }
                }
            }
            return ("None");
        }

        /// <summary>
        /// 文件名不要后缀
        /// </summary>
        /// <param name="欲读取节点"></param>
        /// <param name="文件名"></param>
        /// <returns></returns>
        public string 读配置项_Resources (string 欲读取节点, string 文件名)
        {
            TextAsset textAsset = (TextAsset)Resources.Load(文件名, typeof(TextAsset));
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(textAsset.text);
            XmlNodeList nodeList = xmlDoc.SelectSingleNode("root").ChildNodes;
            foreach (XmlElement xe in nodeList)
            {
                if (xe.Name == 欲读取节点)
                {
                    return (xe.GetAttribute(欲读取节点));
                }
            }
            return ("None");
        }

        public void 更新配置项 (string 欲更新节点, string 欲更新值, string 文件名)
        {
            string localPath = UnityEngine.Application.persistentDataPath + "/" + 文件名;
            if (File.Exists(localPath))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(localPath);
                XmlNodeList nodeList = xmlDoc.SelectSingleNode("root").ChildNodes;
                foreach (XmlElement xe in nodeList)
                {
                    if (xe.Name == 欲更新节点)
                    {
                        xe.SetAttribute(欲更新节点, 欲更新值);
                        break;
                    }
                }
                xmlDoc.Save(localPath);
            }
        }

        public string 读配置项_StreamingAssets (string 欲读取节点, string 文件名)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(ReadXmlFromStreamingAssets(文件名));
            XmlNodeList nodeList = xmlDoc.SelectSingleNode("root").ChildNodes;
            foreach (XmlElement xe in nodeList)
            {
                if (xe.Name == 欲读取节点)
                {
                    return xe.GetAttribute(欲读取节点);
                }
            }
            return ("None");
        }

        public static string ReadXmlFromStreamingAssets (string xmlFileName)
        {
            string filePath = Path.Combine(Application.streamingAssetsPath, xmlFileName);

            if (Application.platform == RuntimePlatform.Android)
            {
                UnityWebRequest www = UnityWebRequest.Get(filePath);
                www.SendWebRequest();

                while (!www.isDone) { }

                return www.downloadHandler.text;
            }
            else
            {
                return File.ReadAllText(filePath);
            }
        }
    }
}