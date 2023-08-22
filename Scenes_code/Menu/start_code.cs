using DG.Tweening;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class start_code : MonoBehaviour
{
    private AsyncOperation operation;
    public bool state = false;
    public GameObject Mask;
    public GameObject hand;
    public GameObject handl;
    public Sprite photo1;
    public Sprite photo2;
    public Sprite photo3;
    public Sprite photo4;
    public Sprite photo5;
    public GameObject var_message;
    private Tweener event_loading;
    private Tweener event_loading2;
    public GameObject message_none;
    private bool bool_internet;
    private bool bool_varupdate;
    public Text whatnew;
    private string updatetxt = " ";
    public GameObject obj_new;
    public GameObject obj_erro;
    private string newvar;
    public GameObject loading_ogj;
    public GameObject textlistview;
    public static int countloading;

    public void Awake ()
    {
        var_message.GetComponent<Text>().text = "正在连接服务器.....";
    }

    /*
    private void Start ()
    {
        Mask.SetActive(false);
        if (IsNetworkReachability() == true)
        {
            //newvar = configXML.读配置项("ver", "ServerInformation.xml");/*
            Receive_message.SendMessageScoket("ver");
            if (newvar != Application.version)
                bool_varupdate = true;
            else
                bool_varupdate = false;
            StartCoroutine(LoadResourceCorotine("https://var.nanasekurumi.top"));
            StartCoroutine(LoadResourceUpdate("https://content.nanasekurumi.top"));
            bool_internet = true;
        }
        else
        {
            event_loading.Kill();
            event_loading2.Kill();
            var_message.GetComponent<Text>().text = "网络异常,检查更新错误";
            uichange.ui_to_middle(obj_erro);
        }
    }
    */

    public void changehandle ()
    {
        event_loading2 = DOTween.To(() => hand.GetComponent<RectTransform>().anchoredPosition, x => hand.GetComponent<RectTransform>().anchoredPosition = x, new Vector2(-315f + (countloading / 100f * 630f), 0), 1f);
        event_loading = DOTween.To(() => handl.GetComponent<Image>().fillAmount, x => handl.GetComponent<Image>().fillAmount = x, countloading / 100f, 1f);
        //event_loading.OnComplete(loadingover);
    }

    public void button_return ()
    {
        Mask.SetActive(false);
    }

    public void button_update ()
    {
        Application.OpenURL("https://www.123pan.com/s/8sWSVv-0iYJ");
        Mask.SetActive(false);
    }

    private void Update ()
    {
        // if (Receive_message.ReturnData)
        //Debug.Log(hand.transform.position.x);
        if (Mask.activeSelf == true)
        {
            changehandle();
            /*
            if (handl.GetComponent<Image>().fillAmount >= 0.2f && handl.GetComponent<Image>().fillAmount < 0.4f) hand.GetComponent<Image>().sprite = photo1;
            if (handl.GetComponent<Image>().fillAmount >= 0.4f && handl.GetComponent<Image>().fillAmount < 0.6f)
            {
                hand.GetComponent<Image>().sprite = photo2;
                // Debug.Log(newvar);
                if (bool_internet == false)
                {
                }
                else if (updatetxt.Length < 5 || updatetxt.Length > 500)
                {
                    event_loading.Kill();
                    event_loading2.Kill();
                    var_message.GetComponent<Text>().text = "服务器异常,检查更新错误";
                    uichange.ui_to_middle(obj_erro);
                    bool_varupdate = false;
                }
                else
                {
                    // Debug.Log(newvar);
                    var_message.GetComponent<Text>().text = "连接到服务器,正在检查更新....";
                }
                if (handl.GetComponent<Image>().fillAmount >= 0.6f && handl.GetComponent<Image>().fillAmount < 0.8f) hand.GetComponent<Image>().sprite = photo3;
                if (handl.GetComponent<Image>().fillAmount >= 0.8f && handl.GetComponent<Image>().fillAmount < 1f) hand.GetComponent<Image>().sprite = photo4;
                if (handl.GetComponent<Image>().fillAmount == 1f) hand.GetComponent<Image>().sprite = photo5;
            }
            */
        }
    }

    private void loadingover ()
    {
        int count = 0;
        if (bool_internet == true && bool_varupdate == true)
        {
            var_message.GetComponent<Text>().text = "检测到新版本啦！快来下载更新吧!" + "  (当前版本: " + Application.version + "  最新版本:" + newvar + ")";
            UIAnimate.ToMiddle(obj_new);
            updatetxt.Replace("<meta http-equiv=\"Content-Type\" content=\"text/html;charset=utf-8\"/>", " ").TrimStart();
            for (int i = 0; i < updatetxt.Length; i++)
            {
                if (updatetxt[i] == '\n')
                    count++;
            }
            whatnew.text = updatetxt.Replace("<meta http-equiv=\"Content-Type\" content=\"text/html;charset=utf-8\"/>", " ").TrimStart();
            textlistview.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 20 * count);
        }
        else
        {
            Mask.SetActive(false);
        }
    }

    public void openscence ()
    {
        if (File.Exists(Application.persistentDataPath + "/playerinformation.xml") == false)
        {
            UIAnimate.ToMiddle(message_none);
        }
        else
        {
            state = true;
            loadingaction();
        }
    }

    private IEnumerator start_game ()
    {
        operation = SceneManager.LoadSceneAsync(1);
        yield return operation;
    }

    private IEnumerator LoadResourceCorotine (string web)
    {
        UnityWebRequest request = UnityWebRequest.Get(@web);
        yield return request.SendWebRequest();
        string tempvar = request.downloadHandler.text;
        if (tempvar != Application.version)
        {
            if (tempvar.Length > 10)
            {
                bool_varupdate = false;
            }
            else
            {
                bool_varupdate = true;
                newvar = tempvar;
            }
        }
        else newvar = Application.version;
    }

    private IEnumerator LoadResourceUpdate (string web)
    {
        UnityWebRequest request = UnityWebRequest.Get(@web);
        yield return request.SendWebRequest();
        string newvar = request.downloadHandler.text;
        updatetxt = newvar;
    }

    private void loadingaction ()
    {
        loading_ogj.SetActive(true);
        StartCoroutine(start_game());
    }
}