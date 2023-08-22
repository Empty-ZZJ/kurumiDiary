using config;
using System;
using System.IO;
using TMPro;
using UnityEngine;

namespace UsersInfo
{
    public class UsersInfo : MonoBehaviour
    {
        private static ConfigXML configXML = new ConfigXML();
        private static GameObject StaticGameObj;

        public static void UpdateUserID_Scene ()
        {
            var _ = new GameObjectEvent();
            if (_.FindInactiveObjectByName("StaticGameObj") == null)
            {
                {
                    StaticGameObj = Instantiate(Resources.Load<GameObject>("Prefabs/StaticGameObj"));
                    DontDestroyOnLoad(StaticGameObj);
                }
                Debug.Log("创建了：" + "StaticGameObj");
            }
            if (!File.Exists(Application.persistentDataPath + "/playerinformation.xml"))
            {
                configXML.创建配置文件("UID", "local", "playerinformation.xml");
                configXML.加入配置项("Login", "False", "playerinformation.xml");
            }
        }

        public static bool UpdateUID_Scene ()
        {
            try
            {
                GameObject _UID = StaticGameObj;
                if (configXML.读配置项("Login", "playerinformation.xml") == "True")
                {
                    _UID.GetComponentInChildren<TextMeshProUGUI>().text = "UID:" + configXML.读配置项("UID", "playerinformation.xml");
                }
                else
                {
                    _UID.GetComponentInChildren<TextMeshProUGUI>().text = "本地用户";
                }
                return true;
            }
            catch (Exception ex)
            {
                new PopNewMessage(ex.Message);
                return false;
            }
        }
    }
}