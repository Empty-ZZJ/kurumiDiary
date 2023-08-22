using config;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UsersLogin : MonoBehaviour
{
    public GameObject Login;
    public GameObject StartGame;
    public GameObject exitUser;
    private ConfigXML configXML = new ConfigXML();

    private void Awake ()
    {
        UsersInfo.UsersInfo.UpdateUserID_Scene();
        if (configXML.读配置项("Login", "playerinformation.xml") == "None")
        {
            configXML.加入配置项("Login", "False", "playerinformation.xml");
        }
        else if (configXML.读配置项("Login", "playerinformation.xml") == "True")
        {
            Login.SetActive(false);
            StartGame.SetActive(true);
            exitUser.SetActive(true);
            if (configXML.读配置项("UID", "playerinformation.xml") != "local")
                new PopNewMessage($"欢迎：{configXML.读配置项("UID", "playerinformation.xml")}");
            else { new PopNewMessage("欢迎您，本地用户"); }
        }
    }

    public void 本地登录 ()
    {
        configXML.更新配置项("UID", "local", "playerinformation.xml");
        UsersInfo.UsersInfo.UpdateUID_Scene();
        configXML.更新配置项("Login", "True", "playerinformation.xml");
        var _gameObject = new GameObjectEvent();
        _gameObject.FindInactiveObjectByName("ExitUser").SetActive(true);
    }

    public void ExitUser ()
    {
        exitUser.SetActive(false);
        Login.SetActive(true);
        StartGame.SetActive(false);
        configXML.更新配置项("UID", "local", "playerinformation.xml");
        configXML.更新配置项("Login", "False", "playerinformation.xml");
        UsersInfo.UsersInfo.UpdateUID_Scene();
    }

    public void CoreStartGame ()
    {
        if (!File.Exists(Application.persistentDataPath + "/playerinformation.xml"))
        {
            Instantiate(Resources.Load<GameObject>("Prefabs/UI/ScenesUI/Account/NameSet"));
            return;
        }
        else if (configXML.读配置项("name", "playerinformation.xml") == "None")
        {
            Instantiate(Resources.Load<GameObject>("Prefabs/UI/ScenesUI/Account/NameSet"));
            return;
        }
        else
        {
            GameObject load = Resources.Load<GameObject>("Prefabs/UI/LoadingEvent");
            Instantiate(load);
            StartCoroutine(start_game());
            AsyncOperation operation = new AsyncOperation();
            IEnumerator start_game ()
            {
                operation = SceneManager.LoadSceneAsync(1);
                yield return operation;
            }
        }
    }
}