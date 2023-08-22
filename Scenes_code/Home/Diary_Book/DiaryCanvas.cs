using System;
using UnityEngine;

public class DiaryCanvas : MonoBehaviour
{
    public void Button_Click_CloseDiary ()
    {
        GameObject.Find("kitchen/Furniture_Kitchencabinet_Forest/PhotoAlbum_new").GetComponent<Diary_button>().button_out();
    }

    public void CreatDiary (Transform _transform)
    {
        Debug.Log(MenheraArrange.Find_bool_home_menherachan().ToString());
        int _hour = DateTime.Now.Hour;
        if (_hour >= 23 || (_hour >= 0 && _hour < 6))//是不是晚上，晚上不管在不在家都可以看日记
            Instantiate(Resources.Load<GameObject>("Prefabs/UI/ScenesUI/Diary/DiaryDetail"), _transform);
        else//不是晚上
        {
            if (MenheraArrange.Find_bool_home_menherachan())//在家
            {
                Instantiate(Resources.Load<GameObject>("Prefabs/UI/ScenesUI/Diary/DiaryProhibitCanvas"));
            }
            else
            {
                Instantiate(Resources.Load<GameObject>("Prefabs/UI/ScenesUI/Diary/DiaryDetail"), _transform);
            }
        }
    }
}