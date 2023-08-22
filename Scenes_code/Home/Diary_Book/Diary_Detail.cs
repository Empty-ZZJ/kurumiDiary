using config;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Diary_Detail : MonoBehaviour
{
    public const int 当前版本游戏一共有多少日记 = 50;
    private readonly ConfigXML configXML = new ConfigXML();
    private AudioSource _Audiosystem;
    public int 当前看的日记的索引;
    public int 用户解锁的日记篇数;
    public TextMeshProUGUI left;
    public TextMeshProUGUI right;
    public TextMeshProUGUI time;
    private int Mode;
    private readonly bool book_state;
    public Sprite[] 胡桃的图片 = new Sprite[4];
    public GameObject prop_background;
    public Image Image_胡桃的图片;
    public Animator animator_system;
    public GameObject Diary;

    public void Awake ()
    {
        _Audiosystem = GameObject.Find("audiosystem/scence_audio").GetComponent<AudioSource>();
        Diary = GameObject.Find("kitchen/Furniture_Kitchencabinet_Forest/PhotoAlbum_new");
        if (File.Exists(Application.persistentDataPath + "/diary.xml") == true)
        {
            if (configXML.读配置项("用户解锁的日记篇数", "diary.xml") == "None")
            {
                configXML.加入配置项("用户解锁的日记篇数", "1", "diary.xml");
                用户解锁的日记篇数 = 1;
            }
            else
            {
                int.TryParse(configXML.读配置项("用户解锁的日记篇数", "diary.xml"), out 用户解锁的日记篇数);
                当前看的日记的索引 = 用户解锁的日记篇数;
            }
        }
        else { configXML.创建配置文件("用户解锁的日记篇数", 1.ToString(), "diary.xml"); 用户解锁的日记篇数 = 1; 当前看的日记的索引 = 1; }
        DiaryUpdate(1);
        StartCoroutine(Loading_Diary(当前看的日记的索引));
    }

    public static void AddDiaty ()
    {
        ConfigXML _ = new ConfigXML();
        int temp = Convert.ToInt32(_.读配置项("用户解锁的日记篇数", "diary.xml"));
        string time = System.DateTime.Now.Year.ToString() + "." + System.DateTime.Now.Month.ToString() + "." + System.DateTime.Now.Day.ToString();
        if (temp + 1 <= 50)
        {
            _.更新配置项("用户解锁的日记篇数", (temp + 1).ToString(), "diary.xml");
            _.加入配置项("diary" + (temp + 1).ToString(), time, "diary.xml");
        }
    }

    public void ButtonLeft ()
    {
        Debug.Log(当前看的日记的索引 - 1);
        if (当前看的日记的索引 - 1 >= 1)
        {
            DiaryUpdate(2);
            Mode = 2;
            StartCoroutine(ChangeDiary());
        }
    }

    public void ButtonRight ()
    {
        Debug.Log(当前看的日记的索引 + 1);
        if (当前看的日记的索引 + 1 <= 用户解锁的日记篇数)
        {
            DiaryUpdate(1);
            Mode = 1;
            StartCoroutine(ChangeDiary());
        }
    }

    public IEnumerator ChangeDiary ()
    {
        if (Mode == 1)
        {
            yield return StartCoroutine(Loading_Diary(++当前看的日记的索引));
        }
        else
        {
            yield return StartCoroutine(Loading_Diary(--当前看的日记的索引));
        }
    }

    public void DiaryUpdate (int mode)
    {
        Instantiate(Resources.Load<GameObject>("Prefabs/UI/ScenesUI/Diary/UI_Fan"), this.gameObject.transform);
        FanAnimate.mode = mode;
    }

    private string ReadDiatyTime (int diary_index)
    {
        if (File.Exists(Application.persistentDataPath + "/diary.xml") == true)
        {
            if (configXML.读配置项("diary" + diary_index.ToString(), "diary.xml") == "None")
            {
                string time = System.DateTime.Now.Year.ToString() + "." + System.DateTime.Now.Month.ToString() + "." + System.DateTime.Now.Day.ToString();
                configXML.加入配置项("diary" + diary_index.ToString(), time, "diary.xml");
            }
        }
        return configXML.读配置项("diary" + diary_index.ToString(), "diary.xml");
    }

    public IEnumerator Loading_Diary (int temp_index)
    {

        yield return null;
        try
        {
            TextAsset textAsset = Resources.Load<TextAsset>("TextAsset/Diary/diary" + temp_index.ToString());
            string originalText = textAsset.text;

            // 将所有的 "*" 替换成特定内容
            string replacedText = originalText.Replace("*", GameConfig.GetValue("name", "playerinformation.xml").TrimStart());

            // 将文本按行分割成数组
            string[] lines = replacedText.Split('\n');

            // 计算分割位置（行数的一半）
            int splitIndex = lines.Length / 2;

            // 构建前半部分和后半部分的文本
            string[] firstHalfLines = new string[splitIndex];
            string[] secondHalfLines = new string[lines.Length - splitIndex];

            // 将行数组分割为前半部分和后半部分
            for (int i = 0; i < splitIndex; i++)
            {
                firstHalfLines[i] = lines[i];
            }

            for (int i = splitIndex; i < lines.Length; i++)
            {
                secondHalfLines[i - splitIndex] = lines[i];
            }

            // 将数组转换回字符串
            string firstHalfText = string.Join("\n", firstHalfLines);
            string secondHalfText = string.Join("\n", secondHalfLines);

            // 更新 UI 文本
            left.text = firstHalfText;
            right.text = secondHalfText;
            time.text = ReadDiatyTime(temp_index);
        }
        catch (Exception ex)
        {
            new PopNewMessage(ex.Message);
            HL.IO.HL_Log.Log(nameof(this.name) + ex.Message, "Error");
        }

    }

    public List<string> SplitByLine (string text)
    {
        List<string> lines = new List<string>();
        byte[] array = Encoding.UTF8.GetBytes(text);
        using (MemoryStream stream = new MemoryStream(array))
        {
            using (var sr = new StreamReader(stream))
            {
                string line = sr.ReadLine();
                while (line != null)
                {
                    lines.Add(line);
                    line = sr.ReadLine();
                };
            }
        }
        return lines;
    }

    public int Getlist_lenght (string[] temp_list)
    {
        for (int i = 0; i <= temp_list.Length; i++)
        {
            if (temp_list[i] == "-")
            {
                return i - 1;
            }
        }
        return temp_list.Length;
    }

    public string Getlist_content (string[] list, int index_start, int index_finally)
    {
        string fi = "";
        for (int i = index_start; i <= index_finally; i++)
        {
            if (list[i].Contains("*") == true)
            {
                fi += list[i].Replace("*", plotbutton.ReadXML("name", "name", "playerinformation.xml").TrimStart()) + "\n";
            }
            else fi += list[i] + "\n";
        }
        return fi;
    }
}