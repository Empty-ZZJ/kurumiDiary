using UnityEngine;

public class MenheraArrange : MonoBehaviour
{
    private readonly int today = System.DateTime.Now.Day;
    private int xml_day;
    private bool today_mode;

    private void Awake ()
    {
        if (GameConfig.GetValue("day", "arrange.xml") != "None")
        {
            int.TryParse(GameConfig.GetValue("day", "arrange.xml"), out xml_day);
            if (today == xml_day) today_mode = true; else today_mode = false;
        }
        else
        {
            GameConfig.SetValue("day", System.DateTime.Now.Day.ToString(), "arrange.xml");
            today_mode = false;
        }
        if (today_mode == false) Main_core_arrange_today_menherachan();
    }

    public void Main_core_arrange_today_menherachan ()
    {
        if (GetMode(1, 2) == 1)
        {
            UpdateMode("是否外出", "是");
            UpdateMode("外出开始时间", GetMode(13, 14).ToString());
            UpdateMode("外出结束时间", GetMode(15, 16).ToString());
        }
        else UpdateMode("是否外出", "否");

        if (GetMode(1, 3) == 1)
        {
        }
    }

    private int GetMode (int min, int max)
    {
        return NewRandom.GetRandomInAB(min, max);
    }

    private void UpdateMode (string son, string wantdate)
    {
        if (GameConfig.GetValue(son, "arrange.xml") != "None")
        {
            GameConfig.SetValue(son, wantdate, "arrange.xml");
        }
        else
        {
            GameConfig.SetValue(son, wantdate, "arrange.xml");
        }
    }

    public static string ReadMode (string want)
    {
        return GameConfig.GetValue(want, "arrange.xml");
    }

    /// <summary>
    /// 这里不用其他判断是因为当用到这个函数的时候肯定是存在"是否外出"这个节点的，所以只有是否两个答案。Main:
    /// 返回当前胡桃是否在家。
    /// </summary>
    /// <returns></returns>
    public static bool Find_bool_home_menherachan ()
    {
        int time = System.DateTime.Now.Hour;
        if (ReadMode("是否外出") == "是")
        {
            int from, to;
            int.TryParse(ReadMode("外出开始时间"), out from);
            int.TryParse(ReadMode("外出结束时间"), out to);
            if (time >= from && time <= to) return false; else return true;
        }
        else
        {
            return true;
        }
    }
}