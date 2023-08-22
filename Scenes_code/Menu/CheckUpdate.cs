using UnityEngine;
using UnityEngine.UI;

public class CheckUpdate : MonoBehaviour
{
    private AsyncTcpClient tcpClient;
    public GameObject F_Progress;
    public GameObject F_whatnew;

    private async void Start ()
    {
        if (IsNetworkReachability())
        {
            F_Progress.SetActive(true);

            start_code.countloading = 10;
            tcpClient = new AsyncTcpClient();

            /*
            await tcpClient.ConnectAsync("103.228.170.38", 800, 3000, () =>
            {
                PopError();
            });*/
            await tcpClient.ConnectAsync(Server.IP, Server.port, 3000, () =>
            {
                PopError();
            });
            start_code.countloading = 20;
            F_Progress.GetComponentInChildren<Text>().text = "连接到服务器正在检查更新";
            if (tcpClient.IsConnected())
            {
                // 连接成功，发送消息并接收回复
                string response = await tcpClient.GetAsync("Ver");
                F_Progress.GetComponentInChildren<Text>().text = $"检测到版本更新   当前版本号: {Application.version}   服务器版本号: {response}";
                start_code.countloading = 30;
                if (Application.version != response)
                {
                    start_code.countloading = 40;
                    F_whatnew.SetActive(true);
                }
                else
                {
                    this.gameObject.SetActive(false);
                }
                start_code.countloading = 50;
                // 断开连接

                tcpClient.Disconnect();
            }
            else
            {
                PopError();
                Debug.LogError("Failed to connect to server.");
            }
        }
        else
        {
            PopError();
        }
    }

    public void PopError ()
    {
        GameObject pop = (GameObject)Resources.Load("Prefabs/UI/ScenesUI/intetneterro");
        Instantiate(pop, this.transform);
    }

    protected bool IsNetworkReachability ()
    {
        switch (Application.internetReachability)
        {
            case NetworkReachability.ReachableViaLocalAreaNetwork:
                Debug.Log("当前使用的是：WiFi！");
                return true;

            case NetworkReachability.ReachableViaCarrierDataNetwork:
                Debug.Log("当前使用的是：移动网络！");
                return true;

            default:
                Debug.Log("当前没有联网，请您先联网后再进行操作！");
                return false;
        }
    }
}