using config;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class lucksystem : MonoBehaviour
{
    private ConfigXML configXML = new ConfigXML();
    public GameObject txt_coin;
    public static long nowcoin;
    public static long frequency;

    public void Start ()
    {
        if (File.Exists(Application.persistentDataPath + "/coinsystem.xml") == false)
        {
            configXML.创建配置文件("nowcoin", "0", "coinsystem.xml");
            configXML.加入配置项("frequency", "0", "coinsystem.xml");
        }
        else
        {
            Debug.Log(configXML.读配置项("nowcoin", "coinsystem.xml"));
            Debug.Log(configXML.读配置项("frequency", "coinsystem.xml"));
        }
    }

    public void FixedUpdate ()
    {
        long.TryParse(configXML.读配置项("nowcoin", "coinsystem.xml"), out nowcoin);
        long.TryParse(configXML.读配置项("frequency", "coinsystem.xml"), out frequency);
        txt_coin.GetComponent<Text>().text = nowcoin.ToString();
    }

    public void add_coin (long wantadd)
    {
        long temp;
        long.TryParse(configXML.读配置项("nowcoin", "coinsystem.xml"), out temp);
        long finallycoin = temp + wantadd;
        configXML.更新配置项("nowcoin", finallycoin.ToString(), "coinsystem.xml");
    }

    public string find_coin ()
    {
        return configXML.读配置项("nowcoin", "coinsystem.xml");
    }

    /// <summary>
    /// 增加math_add_frequency个抽奖次数，小于100时返回False，满100时自动归零，并且返回一个True。
    /// </summary>
    public bool add_frequency (int math_add_frequenc)
    {
        long temp;
        long.TryParse(configXML.读配置项("frequency", "coinsystem.xml"), out temp);
        if ((temp + math_add_frequenc) >= 100)
        {
            configXML.更新配置项("frequency", (temp + math_add_frequenc - 100).ToString(), "coinsystem.xml");
            return true;
        }
        else
        {
            configXML.更新配置项("frequency", (temp + math_add_frequenc).ToString(), "coinsystem.xml");
            return false;
        }
    }

    /// <summary>
    /// 该函数已停止使用。
    /// </summary>
    /// <param name="wantreduce"></param>
    /// <returns></returns>
    public static bool reduce_coin (long wantreduce)
    {
        long temp;
        long finallycoin;
        long.TryParse(plotbutton.ReadXML("nowcoin", "nowcoin", "coinsystem.xml"), out temp);
        if (wantreduce <= temp)
        {
            finallycoin = temp - wantreduce;
            plotbutton.UpdateXml("nowcoin", plotbutton.ReadXML("nowcoin", "nowcoin", "coinsystem.xml"), finallycoin.ToString(), "coinsystem.xml");
            return true;
        }
        return false;
    }

    /// <summary>
    /// 该函数已停止使用。
    /// </summary>
    /// <param name="wantadd"></param>
    /// <returns></returns>
    public static bool rest_frequency (int wantadd)
    {
        int temp;
        int.TryParse(plotbutton.ReadXML("frequency", "frequency", "coinsystem.xml"), out temp);
        if (temp + wantadd < 100)
        {
            int finallycoin = temp + wantadd;
            plotbutton.UpdateXml("frequency", plotbutton.ReadXML("frequency", "frequency", "coinsystem.xml"), finallycoin.ToString(), "coinsystem.xml");
            return false;
        }
        else
        {
            plotbutton.UpdateXml("frequency", plotbutton.ReadXML("frequency", "frequency", "coinsystem.xml"), "0", "coinsystem.xml");
            return true;
        }
    }
}