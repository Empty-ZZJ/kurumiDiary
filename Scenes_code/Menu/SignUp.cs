using config;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SignUp : MonoBehaviour
{
    //注册格式：SignUp&email&password&name
    private ConfigXML configXML = new ConfigXML();

    public InputField email;
    public InputField password;
    public Toggle toggle;
    public Text Image_VerificationCode;
    private bool SignUpEn;//是否同意了条款，注册账号
    private bool bool_VerificationCode;//是否通过验证码
    private string VerificationCode = "";//验证码
    public GameObject Panel_Code;
    public InputField input_VerificationCode;

    public void SetButtonState ()
    {
        Server.LoginUIState = 2;
        if (toggle.isOn) SignUpEn = true;
        else SignUpEn = false;
        Debug.Log("当前是否开启注册:   " + toggle.isOn.ToString());
    }

    public void Button_click_VerificationCode ()
    {
        if (input_VerificationCode.text.ToLower() == VerificationCode.ToLower())
        {
            // 验证码正确
            GameObject.Find("SignCanvas/SignUp/SignUpBack/Button/return").SetActive(true);
            bool_VerificationCode = true;
            Panel_Code.SetActive(false);
        }
        else
        {
            new PopNewMessage("验证码错误");
        }
    }

    public void SetReturnOK ()
    {
        GameObject.Find("SignCanvas/SignUp/SignUpBack/Button/return").SetActive(true);
    }

    public async void Button_click_SignUp ()
    {
        if (SignUpEn)//同意了条款
        {
            if (bool_VerificationCode == false)
            {
                GameObject.Find("SignCanvas/SignUp/SignUpBack/Button/return").SetActive(false);
                VerificationCode = API.AccountAPI.CreatNew_VerificationCode();
                Image_VerificationCode.GetComponent<Text>().text = VerificationCode;
                Panel_Code.SetActive(true);
                return;
            }
            var waita = new WaitLoadGIf();
            AsyncTcpClient tcpClient = new AsyncTcpClient();
            string _email = email.text;
            try
            {
                if (API.AccountAPI.CheckEmail(ref _email) == "邮箱地址合法")
                {
                    ShowLoading _showLoading = new ShowLoading();
                    string md5password = API.EncodingAction.GetShA256(password.text);
                    Debug.Log(md5password);
                    await tcpClient.ConnectAsync(Server.IP, Server.port, 3000, () =>
                    {
                        //超时
                        _showLoading.KillLoading();
                        new PopNewMessage("连接超时,请重试");
                    });
                    if (tcpClient.IsConnected())
                    {
                        string response = await tcpClient.GetAsync(API.AccountAPI.SplicingStatements("SignUp", _email, md5password));
                        Debug.Log(response);
                        //-1：注册过了
                        //-2:注册失败
                        //>=10000：注册成功
                        _showLoading.KillLoading();
                        if (Convert.ToInt32(response) > 10000)
                        {
                            new PopNewMessage("欢迎: " + response);//登录成功，返回的response就是登录的UID
                            if (configXML.读配置项("UID", "playerinformation.xml") != "None")
                            {
                                configXML.更新配置项("UID", response, "playerinformation.xml");
                                configXML.更新配置项("Login", "True", "playerinformation.xml");
                            }
                            else
                            {
                                configXML.加入配置项("UID", response, "playerinformation.xml");
                                configXML.加入配置项("Login", "True", "playerinformation.xml");
                            }
                            GameObject.Find("MainCanvas/Button_Game_In/GameObject").SetActive(false);
                            GameObjectEvent _gameObject = new GameObjectEvent();
                            _gameObject.FindInactiveObjectByName("开始游戏").SetActive(true);
                            _gameObject.FindInactiveObjectByName("ExitUser").SetActive(true);
                            UsersInfo.UsersInfo.UpdateUID_Scene();
                            this.transform.parent.transform.parent.transform.gameObject.transform.parent.transform.gameObject.SetActive(false);
                            Destroy(this.gameObject);
                        }
                        else
                        {
                            //new NewMessage();
                            switch (response)
                            {
                                case "-1":
                                    new PopNewMessage("邮箱已经注册");
                                    break;

                                case "-2":
                                    new PopNewMessage("注册失败");
                                    break;

                                default:
                                    new PopNewMessage("未知错误");
                                    break;
                            }
                        }
                    }
                    else { new PopNewMessage("连接服务器失败"); }
                }
                else
                {
                    new PopNewMessage("邮箱地址不合法");
                }
            }
            catch (Exception ex) { new PopNewMessage(ex.Message); }
            finally
            {
                waita.Exit();
                tcpClient.Disconnect();
                Destroy(tcpClient);
            }
        }
        else
        {
            new PopNewMessage("请先同意条款再注册");
        }
    }

    public void UpdateCode ()
    {
        VerificationCode = API.AccountAPI.CreatNew_VerificationCode();
        Image_VerificationCode.GetComponent<Text>().text = VerificationCode;
    }
}