using System;
using UnityEngine;
using UnityEngine.UI;

public class LrcEvent : MonoBehaviour
{
    public Text lyricText; // 显示歌词的Text
    public AudioSource audioSource; // 音频播放器组件

    private static LyricData lyricData; // 解析后的歌词数据
    private static string[] lyricLines; // 分割后的歌词行数组
    private static int currentLyricIndex = 0; // 当前歌词行的索引
    public static bool IsLRc;

    public static void SetLrc (string _json)
    {
        currentLyricIndex = 0;
        IsLRc = true;
        // 解析歌词JSON数据
        lyricData = JsonUtility.FromJson<LyricData>(_json);
        // 替换所有的 \r
        string cleanedLyricText = lyricData.lyric.Replace("\r", "");
        // 将歌词文本分割成行
        lyricLines = cleanedLyricText.Split('\n');
        Debug.Log(_json);
    }


    public void Update ()
    {
        if (IsLRc == false)
        {
            lyricText.text = string.Empty;
            return;
        }

        try
        {

            float currentTime = audioSource.time;

            // 逐行检查歌词，判断是否需要显示新的歌词行
            while (currentLyricIndex < lyricLines.Length)
            {

                string lyricLine = lyricLines[currentLyricIndex];
                if (string.IsNullOrEmpty(lyricLine))
                {

                    break;
                }
                else if (!string.IsNullOrEmpty(lyricLine) && lyricLine[lyricLine.Length - 1] == ']')
                {
                    //这种一般是歌曲信息，不是时间+歌词文本的格式
                    DisplayLyric(lyricLine);
                    currentLyricIndex++;
                }
                else if (IsLyricTime(lyricLine, currentTime))
                {
                    DisplayLyric(lyricLine);
                    currentLyricIndex++;
                }
                else
                {
                    break;
                }
            }


        }
        catch (Exception)
        {
            //new PopNewMessage(ex.Message);
            lyricText.text = "无法识别的歌词格式";
        }

    }

    // 判断当前歌词行是否需要显示
    private bool IsLyricTime (string lyricLine, float currentTime)
    {
        string[] parts = lyricLine.Split('[', ']');
        if (parts.Length < 3)
        {
            return false;
        }

        string[] timeParts = parts[1].Split(':');
        if (timeParts.Length < 2)
        {
            return false;
        }

        float lyricTime = float.Parse(timeParts[0]) * 60 + float.Parse(timeParts[1]);
        return currentTime >= lyricTime;
    }

    // 显示歌词行到UI元素
    private void DisplayLyric (string lyricLine)
    {
        string[] parts = lyricLine.Split('[', ']');
        if (parts.Length < 3)
        {
            return;
        }

        lyricText.text = parts[2];
    }
}
// 用于解析歌词JSON数据的类
[System.Serializable]
public class LyricData
{
    public string lyric;
}