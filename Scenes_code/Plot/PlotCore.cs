using config;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlotCore : MonoBehaviour
{
    private readonly ConfigXML configxml = new ConfigXML();
    public AudioClip ClickAudio;
    public AudioSource audioSource;

    public struct Struct_PlotMessage
    {
        public string txt;
        public string character;
        public string skin_back;
        public string skin_character;
        public bool event_white;
        public bool next;//是否有下一句
        public List<string> choice;
    }

    /// <summary>
    /// 专门用来处理第一分支的数据
    /// </summary>
    public struct TemporaryBranchInfo
    {
        public bool IsTemporaryBranch;
        public int Branch_2_From;
        public int Branch_2_End;
    }
    public struct PlotHistory
    {
        public string _Character;
        public string _Text;
        public void SetNewPlot (string _Character, string _Text)
        {
            this._Character = _Character;
            this._Text = _Text;
        }
    }
    public static List<PlotHistory> _PlotHistory = new List<PlotHistory>();

    public TemporaryBranchInfo _TemporaryBranchInfo = new TemporaryBranchInfo();
    public List<Struct_PlotMessage> PlotMessage = new List<Struct_PlotMessage>();
    public int CoreIndex = 0;

    public Image F_Skin_Back;
    public Image F_Skin_Character;
    public Text F_text;
    public bool Core_statenext;
    public GameObject F_Choice1;
    public GameObject F_Choice2;
    public GameObject F_dialog_white;

    public GameObject F_Left_dialog_name;
    public GameObject F_right_dialog_name;
    public AudioSource F_AudioSource;

    /// <summary>
    /// 每多一列多15元素点。
    /// 130的时候显示11个字
    /// </summary>
    private const int Value_Default_width_choie = 130;

    public void Awake ()
    {
        InitializePlot();
    }

    // 定义协程函数
    public void InitializePlot ()
    {
        try
        {
            // 初始化变量
            if (File.Exists(Application.persistentDataPath + "/config_PlotProgress.xml"))
            {
                if (configxml.读配置项("plot" + ListPlotButton.Plotindex.ToString(), "config_PlotProgress.xml") != "None")
                {
                    int.TryParse(configxml.读配置项("plot" + ListPlotButton.Plotindex.ToString(), "config_PlotProgress.xml"), out CoreIndex);
                }
            }

            Core_statenext = true; // 设置状态标志
            int count = 0; // 计数器
            XmlDocument xmlDoc = new XmlDocument(); // 创建 XML 文档对象
            var _ = Resources.Load<TextAsset>($"TextAsset/Plot/MainPlotTXT/plotall{ListPlotButton.Plotindex}");
            string _temp = _.text; // 从 StreamingAssets 文件夹中读取 XML 文件
            Debug.Log(_temp);
            xmlDoc.LoadXml(_temp); // 加载 XML 数据
            XmlNodeList node = xmlDoc.SelectSingleNode("root").ChildNodes; // 获取 root 节点的所有子节点
            foreach (XmlElement x1 in node) // 遍历子节点
            {
                foreach (XmlElement data1 in x1.ChildNodes) // 遍历子节点的子节点
                {
                    //<t1>Q|锵锵，你看。我在箱子底下翻到了一张以前画的“藏宝图”哦。|background/kitchen_10am|illustration/014 #157711|0|12</t1>
                    string[] messageArray = data1.InnerText.Split('|'); // 分割数据
                    List<string> _temp_message = new List<string>(messageArray);

                    // 创建结构体对象并赋值
                    var _Struct_PlotMessage = new Struct_PlotMessage();
                    _Struct_PlotMessage.character = _temp_message[0];
                    _Struct_PlotMessage.txt = _temp_message[1];
                    _Struct_PlotMessage.skin_back = _temp_message[2];
                    _Struct_PlotMessage.skin_character = _temp_message[3];

#pragma warning disable CS0164
                    choiceskin:
#pragma warning restore CS0164
                    {
                        // 处理选择分支数据
                        _Struct_PlotMessage.choice = new List<string>();//初始化

                        List<string> __temp_message = new List<string>(_temp_message[4].Split('-'));

                        foreach (string s in __temp_message)
                        {
                            _Struct_PlotMessage.choice.Add(s);
                        }
                    }

                    if (_temp_message[4] == "3") { _Struct_PlotMessage.event_white = true; }


                    _Struct_PlotMessage.next = true;
                    _Struct_PlotMessage.next = true;

                    PlotMessage.Add(_Struct_PlotMessage); // 将结构体对象添加到列表中
                    count++;
                }
            }

        }
        catch (Exception ex)
        {
            new PopNewMessage(ex.Message);
        }
        var lastPlotMessage = PlotMessage[PlotMessage.Count - 1];
        lastPlotMessage.event_white = true;
        lastPlotMessage.next = false;
        PlotMessage[PlotMessage.Count - 1] = lastPlotMessage;
        Debug.Log($"总数:{PlotMessage.Count}");
        NextPlotScene(); // 调用下一个方法
    }
    /*
    public void Update ()
    {
        Debug.Log(CoreIndex);
    }
    */
    public void NextPlotScene ()
    {
        audioSource.PlayOneShot(ClickAudio);

        Debug.Log($"当前序号：{CoreIndex}");
        if (!Core_statenext) return;
        if (PlotMessage[CoreIndex].next)
        {

            if (_TemporaryBranchInfo.IsTemporaryBranch)//处于第一分支，要跳过第二分支的全部文本
            {
                Debug.Log(CoreIndex + " " + _TemporaryBranchInfo.Branch_2_From);
                if (CoreIndex + 1 >= _TemporaryBranchInfo.Branch_2_From)
                {
                    Debug.Log("第一选项的文本结束");
                    _TemporaryBranchInfo.IsTemporaryBranch = false;
                    GotoPlotIndex(_TemporaryBranchInfo.Branch_2_End + 1, true);
                    return;
                }
            }

            if (PlotMessage[CoreIndex].event_white)
            {
                ShowWhiteWait();
                Debug.Log("白屏等待");
                return;
            }
            if (Core_statenext)
            {
                F_Choice1.SetActive(false); F_Choice2.SetActive(false);
                Core_statenext = false;
                ChangeImage_back(PlotMessage[CoreIndex].skin_back);
                ChangeImage_character(PlotMessage[CoreIndex].skin_character);
                ChangeTxt(PlotMessage[CoreIndex].txt);
                ChangeImage_character_Txt(PlotMessage[CoreIndex].character);
                ChangeCharacterVoice(CoreIndex + 1);
            }
            CoreIndex++;
        }
        else
        {
            Debug.Log("结束");

            configxml.更新配置项("plot" + ListPlotButton.Plotindex.ToString(), "0", "config_PlotProgress.xml");
            //写一个要解锁的篇章
            string _nextplot_index = configxml.读配置项_StreamingAssets("plot" + ListPlotButton.Plotindex.ToString(), "config/plot/plotdetail.xml").Split('|')[1];
            if (configxml.读配置项($"plot{_nextplot_index}", "plotlock.xml") == "None")
            {
                configxml.加入配置项($"plot{_nextplot_index}", "True", "plotlock.xml");
            }
            else if (configxml.读配置项($"plot{_nextplot_index}", "plotlock.xml") == "False" || configxml.读配置项($"plot{_nextplot_index}", "plotlock.xml") == "True")
            {
                configxml.更新配置项($"plot{_nextplot_index}", "True", "plotlock.xml");
            }
            else
            {
                new PopNewMessage("Error:The plot is not allowed");
            }

            ShowWhiteWait();
            AsyncOperation operation;
            IEnumerator start_map ()
            {
                yield return new WaitForSeconds(1);
                operation = SceneManager.LoadSceneAsync(2);
                yield return operation;
            }
            StartCoroutine(start_map());
        }
    }

    public void ChangeCharacterVoice (int _index)
    {
        var _audio = Resources.Load<AudioClip>($"Audio/Character/Plot{ListPlotButton.Plotindex}/t{_index}");

        if (_audio == null)
        {
            Debug.LogWarning("没有找到音频资源");
            return;
        }
        else
        {
            F_AudioSource.clip = _audio;
            F_AudioSource.Play();
            return;
        }
    }

    public void ChangeImage_character_Txt (string name)
    {
        Debug.Log(name);
        void setnone ()
        {
            F_Left_dialog_name.SetActive(false);
            F_right_dialog_name.SetActive(false);
        }
        switch (name)
        {
            case "Q":
                setnone();
                F_Left_dialog_name.SetActive(true);
                F_Left_dialog_name.GetComponentInChildren<Text>().text = "七濑胡桃";
                break;

            case "M":
                setnone();
                F_right_dialog_name.SetActive(true);
                F_right_dialog_name.GetComponentInChildren<Text>().text = "我";
                break;
        }
    }

    public void ChangeImage_back (string path)
    {
        var photo = Resources.Load<Sprite>(path);
        if (photo != null)
        {
            F_Skin_Back.gameObject.GetComponent<Image>().sprite = photo;
        }
        else
        {
            photo = Resources.Load<Sprite>("background/" + path);
            if (photo != null)
            {
                F_Skin_Back.gameObject.GetComponent<Image>().sprite = photo;
            }
            else
            {
                HL.IO.HL_Log.Log("错误的剧情资源", "严重错误");
                new PopNewMessage("错误的剧情资源");
            }
        }

    }

    public void ChangeImage_character (string path)
    {
        var photo = Resources.Load<Sprite>(path);
        if (photo != null)
        {
            F_Skin_Character.gameObject.GetComponent<Image>().sprite = photo;
        }
        else
        {
            photo = Resources.Load<Sprite>("illustration/" + path);
            if (photo != null)
            {
                F_Skin_Character.gameObject.GetComponent<Image>().sprite = photo;
            }
            else
            {
                HL.IO.HL_Log.Log("错误的剧情资源", "严重错误");
                new PopNewMessage("错误的剧情资源");
            }
        }

    }

    public void ChangeTxt (string _txt)
    {
        F_text.text = "";
        F_text.DOText(_txt, _txt.Length * 0.07f).OnComplete(_overtxt);
    }

    private IEnumerator UpdateProgress ()
    {
        yield return null; // 等待一帧，确保协程在下一帧开始执行

        if (File.Exists(Application.persistentDataPath + "/config_PlotProgress.xml"))
        {
            if (configxml.读配置项("plot" + ListPlotButton.Plotindex.ToString(), "config_PlotProgress.xml") != "None")
            {
                configxml.更新配置项("plot" + ListPlotButton.Plotindex.ToString(), CoreIndex.ToString(), "config_PlotProgress.xml");
            }
            else
            {
                configxml.加入配置项("plot" + ListPlotButton.Plotindex.ToString(), CoreIndex.ToString(), "config_PlotProgress.xml");
            }
        }
        else
        {
            configxml.创建配置文件("plot" + ListPlotButton.Plotindex.ToString(), CoreIndex.ToString(), "config_PlotProgress.xml");
        }
    }

    /// <summary>
    /// 当一句话结束后执行的，这里有解除禁止状态和判断是否有选项。
    /// </summary>
    public void _overtxt ()
    {
        if (PlotMessage[CoreIndex].choice.Count == 1)
        {
            Core_statenext = true;

            StartCoroutine(UpdateProgress());
        }
        else
        {
            /*
            for (int i = 0; i < PlotMessage[index].choice.Count; i++)
            {
                Debug.Log(PlotMessage[index].choice[i]);
            }
            */
            //对于PlotMessage[index]的格式
            //序号1-第一个选项文本-第二个选项文本-序号2
            F_Choice1.SetActive(true);

            F_Choice1.GetComponent<RectTransform>().sizeDelta = new Vector2(Value_Default_width_choie + (PlotMessage[CoreIndex].choice[1].Length - 11) / 2 * 15f, 48.5244f);

            F_Choice1.GetComponentInChildren<Text>().text = PlotMessage[CoreIndex].choice[1];
            if (PlotMessage[CoreIndex].choice.Count >= 3)
            {
                F_Choice2.SetActive(true);
                F_Choice2.GetComponent<RectTransform>().sizeDelta = new Vector2(Value_Default_width_choie + (PlotMessage[CoreIndex].choice[2].Length - 11) / 2 * 15f, 48.5244f);
                F_Choice2.GetComponentInChildren<Text>().text = PlotMessage[CoreIndex].choice[2];
            }
        }
    }

    /// <summary>
    /// 跳转到指定的就剧情索引
    /// </summary>
    private void GotoPlotIndex (int _Goto_index, bool _Immediately = false)
    {
        CoreIndex = _Goto_index - 1;
        if (_Immediately)
        {
            NextPlotScene();
        }
    }

    public void ButtonCoreChoice (int choiceindex)
    {
        audioSource.PlayOneShot(ClickAudio);
        switch (choiceindex)
        {
            case 1:
                if (PlotMessage[CoreIndex].choice.Count > 3)
                {
                    _TemporaryBranchInfo.IsTemporaryBranch = true;
                    _TemporaryBranchInfo.Branch_2_From = Convert.ToInt32(PlotMessage[CoreIndex].choice[0]);
                    _TemporaryBranchInfo.Branch_2_End = Convert.ToInt32(PlotMessage[CoreIndex].choice[3]);
                    Debug.Log(_TemporaryBranchInfo.Branch_2_From + "   " + _TemporaryBranchInfo.Branch_2_End);
                }
                else
                {
                    _TemporaryBranchInfo.IsTemporaryBranch = false;
                }

                Core_statenext = true;
                CoreIndex++;
                break;

            case 2:
                if ((PlotMessage[CoreIndex].choice.Count > 3))
                {
                    _TemporaryBranchInfo.IsTemporaryBranch = false;
                    Core_statenext = true;
                    GotoPlotIndex(Convert.ToInt32(PlotMessage[CoreIndex].choice[3]) + 1);
                    return;
                }
                else
                {
                    CoreIndex++;
                }
                Core_statenext = true;
                break;
            default:

                Debug.LogError("Error!");
                return;
        }
        NextPlotScene();
    }

    public void ShowWhiteWait ()
    {
        if (PlotMessage[CoreIndex].event_white)
        {
            F_dialog_white.SetActive(true);
            Core_statenext = false;
            F_dialog_white.GetComponent<Image>().DOColor(new Color32(255, 255, 255, 255), 1).OnComplete(() =>
            {
                F_dialog_white.GetComponent<Image>().DOColor(new Color32(255, 255, 255, 0), 1).OnComplete(() =>
                {
                    F_dialog_white.SetActive(false);
                });

                Core_statenext = true;
                CoreIndex++;
                NextPlotScene();
            });
        }
    }
}