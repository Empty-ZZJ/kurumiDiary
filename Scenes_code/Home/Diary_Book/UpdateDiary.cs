using API;
using config;
using System;
using System.IO;
using UnityEngine;

public class UpdateDiary : MonoBehaviour
{
    private ConfigXML configXML = new ConfigXML();

    private void Start ()
    {
        if (File.Exists(Application.persistentDataPath + "/diary.xml") == true)
        {
            if (configXML.读配置项("日记下一次更新时间", "diary.xml") == "None")
            {
                configXML.加入配置项("日记下一次更新时间", TimestampConverter.DateTimeToTimestamp(DateTime.Now).ToString(), "diary.xml");
            }
            else check_diary();
        }
    }

    public void Set ()
    {
        if (true)
        {
            Debug.Log("I love you");
        }
    }

    public void check_diary ()
    {
        DateTime _now = DateTime.Now;
        if (_now > TimestampConverter.TimestampToDateTime(Convert.ToInt64(configXML.读配置项("日记下一次更新时间", "diary.xml"))))
        {
            Debug.Log("更新日记");

            configXML.更新配置项("日记下一次更新时间", TimestampConverter.DateTimeToTimestamp(_now.AddDays(NewRandom.GetRandomInAB(2, 7))).ToString(), "diary.xml");
            Diary_Detail.AddDiaty();
        }
    }
}