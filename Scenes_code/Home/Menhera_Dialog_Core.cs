using UnityEngine;

public class Menhera_Dialog_Core : MonoBehaviour
{
    private void Start ()
    {
    }

    /*
    public static Vector3 from_position;
    public string[] nowtext = new string[500];
    public int[] nowcharacter = new int[500];
    public string[] animator_Menherachan = new string[500];
    public string[] audio_dialog = new string[500];
    public bool[] bool_reply = new bool[500];
    public string[] string_reply = new string[500];
    public GameObject maincamera;//摄像机
    public GameObject sleep;//睡觉的模型
    public GameObject kitchen;//在餐厅的模型
    public GameObject liedown;//在客厅想要对话或者无事可做的模型
    public GameObject menherachanCanvas;///对话界面的画布
    public GameObject mainCanvas;//主画布
    public static bool stateCanvas;//对话界面的画布状态
    public static int mode;//是随机到了哪个日常对话
    public static bool mode_state;//是否在对话界面
    public int tot_dialog_txt;
    public int now_txt_index = 0;
    public GameObject dialog_system;
    public TextMeshProUGUI dialog_txt;
    public Tweener dialog_event;
    public bool bool_next = true;//是否可以下一句
    public static bool bool_是否有新对话;
    public DateTime last_time;
    public DateTime next_time;
    public Animator Animator_system;
    public GameObject close;
    public AudioSource audio_source;
    public GameObject Ipad;
    public GameObject Diary;
    /// <summary>
    /// 1：nothing 2:等待对话  3：正在对话
    /// </summary>
    public int menherachan_mode;
    private void Awake ()
    {
        return;
        Ipad = GameObject.Find("kitchen/Furniture_Kitchencabinet_Forest/MPad");
        Diary = GameObject.Find("kitchen/Furniture_Kitchencabinet_Forest/PhotoAlbum_new");
        if (File.Exists(Application.persistentDataPath + "/talk.xml") == true)
        {
            if (plotbutton.ReadXML("nextktime", "nextktime", "talk.xml") == "None")
            {
                plotbutton.AddXML("nextktime", DateTime.Now.AddMinutes(NewRandom.GetRandomInAB(10, 30)).ToString(), "nextktime", "talk.xml");
            }
        }
        else plotbutton.CreateXML("nextktime", "nextktime", DateTime.Now.ToString(), "talk.xml");
        bool_next = true;

        if (System.DateTime.Now.Hour >= 23 || System.DateTime.Now.Hour < 8)//夜间，胡桃睡觉
        {
            set_menherachan_all_false();
            sleep.SetActive(true);
            return;
        }
        else
        {
        }

        update_menherachan_event();
    }
    private void set_menherachan_all_false ()
    {
        sleep.SetActive(false);
        kitchen.SetActive(false);
        liedown.SetActive(false);
        Invoke("set_menherachan_all_false", 600f);
    }

    void update_menherachan_event ()
    {
        if (是否有新对话() == false)
        {
            if (menherachan_mode != 1)
            {
                menherachan_mode = 1;
                Animator_system.Play("nothing");
                dialog_system.SetActive(false);
            }
        }
        else
        {
            if (menherachan_mode != 2)
            {
                menherachan_mode = 2;
                Animator_system.Play("newtalk");
            }
        }
        Invoke("update_menherachan_event", 5);
    }
    public void Main_core_loadplot (string txtfile)
    {
        int index_temp = 0;
        string[] temp_text;
        string[] temp_reply;
        TextAsset textAsset = (TextAsset)Resources.Load(txtfile, typeof(TextAsset));
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        XmlNodeList node = xmlDoc.SelectSingleNode("root").ChildNodes;
        foreach (XmlElement x1 in node)
        {
            foreach (XmlElement data1 in x1.ChildNodes)
            {
                //data1.Name序号，暂时无用。
                // data1.InnerText 要的文本。
                //Q|听说多吃会变胖呢。|1-胡桃已经变胖了|null|
                temp_text = data1.InnerText.Split('|');
                if (data1.InnerText.Contains("*") == true)
                    nowtext[index_temp] = temp_text[1].Replace("*", " " + plotbutton.ReadXML("name", "name", "playerinformation.xml").TrimStart() + " ");
                else
                    nowtext[index_temp] = temp_text[1];
                switch (temp_text[0])
                {
                    case "Q":
                        nowcharacter[index_temp] = 0;
                        break;

                    case "M":
                        nowcharacter[index_temp] = 1;
                        break;
                }
                animator_Menherachan[index_temp] = temp_text[3];
                audio_dialog[index_temp] = temp_text[4];
                //需要回复一句话
                {
                    temp_reply = temp_text[2].Split('-');
                    if (temp_reply.Length == 1)
                        bool_reply[index_temp] = false;
                    else
                    {
                        bool_reply[index_temp] = true;
                        string_reply[index_temp] = temp_reply[1];
                    }
                }
                //------------------
                index_temp++;
                tot_dialog_txt = index_temp;
                //------------------
            }
        }
    }
    public void camera_to_table_left ()
    {
        if (SceneCameraMove.IsMove_Click())
        {
            mainCanvas.SetActive(false);
            stateCanvas = true;
            from_position = maincamera.transform.position;
            SceneCameraMove.bool_move = false;
            maincamera.transform.DOMove(new Vector3(1.648f, 0.802999973f, 3.74099994f), 0.5f);
            Tweener temp = maincamera.transform.DORotate(new Vector3(0, -25f, 0), 0.5f, RotateMode.Fast);
            temp.OnComplete(setmenherachanCanvas);
        }
    }
    public bool 是否有新对话 ()
    {
        DateTime.TryParse(plotbutton.ReadXML("nextktime", "nextktime", "talk.xml"), out next_time);
        if (DateTime.Now >= next_time)
        {
            bool_是否有新对话 = true;
            return bool_是否有新对话;
        }
        else
        {
            bool_是否有新对话 = false;
            return bool_是否有新对话;
        }
    }
    public void camera_to_living_talk ()
    {
        if (是否有新对话() == false)
        {
            return;
        }
        if (SceneCameraMove.IsMove_Click())
        {
            bool_next = true;
            Animator_system.Play("starttalk");
            mainCanvas.SetActive(false);
            stateCanvas = true;
            from_position = maincamera.transform.position;
            SceneCameraMove.bool_move = false;
            Tweener temp = maincamera.transform.DOMove(new Vector3(5.42000008f, 0.602999985f, 5.6960001f), 1f);
            temp.OnComplete(setmenherachanCanvas);
            if (mode_state == false)
            {
                mode = NewRandom.GetRandomInAB(1, 2);
                Main_core_loadplot("plottxt/usualplot/plot" + mode.ToString());
                now_txt_index = 0;
                mode_state = true;
                dialog_system.SetActive(true);
            }
            else return;
        }
    }
    protected void setmenherachanCanvas ()
    {
        dialog_system.SetActive(true);
        menherachanCanvas.SetActive(true);
        button_next_dialog();
    }
    public void button_next_dialog ()
    {
        //animator_Menherachan
        if (bool_next == true)
        {
            bool_next = false;
            Debug.Log(nowtext[now_txt_index]);
            if (nowcharacter[now_txt_index] == 0)
            {
                //Animator_system.Play(animator_Menherachan[now_txt_index]);
                dialog_event = DOTween.To(() => string.Empty, value => dialog_txt.text = value, nowtext[now_txt_index], nowtext[now_txt_index].Length * 0.07f).SetEase(Ease.Linear);
                play_audio_core_menherachan(audio_dialog[now_txt_index]);
                dialog_event.OnComplete(event_nextdialog);
            }
            else
            {
            }
            now_txt_index++;
        }
        else return;
    }
    private void event_nextdialog ()
    {
        if (now_txt_index >= tot_dialog_txt)
        {
            Debug.Log("对话结束");
            close.SetActive(true);
            bool_是否有新对话 = false;
            plotbutton.UpdateXml("nextktime", plotbutton.ReadXML("nextktime", "nextktime", "talk.xml"), DateTime.Now.AddMinutes(NewRandom.GetRandomInAB(10, 30)).ToString(), "talk.xml");
            menherachan_mode = 0;
            Invoke("update_menherachan_event", 2f);
            return;
        }
        bool_next = true;
    }
    public void play_audio_core_menherachan (string file)
    {
        if (file == "null")
        {
            audio_source.Stop();
            return;
        }
        string path = "music/menherachan/usual/" + file;
        audio_source.clip = Resources.Load<AudioClip>(path);
        audio_source.Play();
    }
    */
}