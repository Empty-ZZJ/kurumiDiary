using config;
using System;
using UnityEngine;
using UnityEngine.UI;

public class LoginUI : MonoBehaviour
{
    //登录格式：Login&email&password
    public ConfigXML configXML = new ConfigXML();

    public Toggle toggle;
    public InputField email;
    public InputField password;
    private bool LoginEn = false;//是否同意了条款，登陆游戏

    public void SetButtonState ()
    {
        Server.LoginUIState = 1;
        if (toggle.isOn) LoginEn = true;
        else LoginEn = false;
        Debug.Log("当前是否开启登录:   " + toggle.isOn.ToString());
    }

    public async void Button_click_Login ()
    {
        if (LoginEn)
        {
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
                        new PopNewMessage("连接超时");
                    });
                    if (tcpClient.IsConnected())
                    {
                        string response = await tcpClient.GetAsync(API.AccountAPI.SplicingStatements("Login", _email, md5password));
                        Debug.Log(response);
                        _showLoading.KillLoading();

                        if (response != "没有找到该用户")
                        {
                            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^[0-9]\d*$");
                            if (regex.IsMatch(response))
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
                                var _gameObject = new GameObjectEvent();
                                _gameObject.FindInactiveObjectByName("开始游戏").SetActive(true);
                                _gameObject.FindInactiveObjectByName("ExitUser").SetActive(true);
                                UsersInfo.UsersInfo.UpdateUID_Scene();
                                Destroy(this.gameObject);
                                this.transform.parent.transform.parent.transform.parent.transform.gameObject.SetActive(false);
                            }
                            else
                            {
                                new PopNewMessage("服务器返回了异常数据");
                            }
                        }
                        else { new PopNewMessage("账号或密码错误，请检查后重试"); }
                    }
                    else { new PopNewMessage("连接服务器失败"); }
                }
                else//不合法
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
            new PopNewMessage("请先同意条款");
        }
    }

    public void SetReturn (bool _a)
    {
        GameObject.Find("SignCanvas/SignUp/SignUpBack/Button/return").SetActive(_a);
    }
}