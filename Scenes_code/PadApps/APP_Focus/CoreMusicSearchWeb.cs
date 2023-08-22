using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoreMusicSearchWeb : MonoBehaviour
{
    public Text Input_MusicName;
    public GameObject CloneList;
    public GameObject Content;
    public GameObject MusicView;
    public GameObject Viewport;
    public Scrollbar ScrollbarList;
    public static List<Music.MusicMessage> musicInfoList;

    public async void Button_Search_Start ()
    {
        musicInfoList = await new Music().MusicSearch(Input_MusicName.text);
        ListStartAnimate();
        KillObj();
        for (int i = 0; i < musicInfoList.Count; i++)
        {
            GameObject Clone = Instantiate(CloneList, Content.transform);
            Clone.gameObject.name = "List" + (i).ToString();
            Clone.GetComponentInChildren<Text>().text = musicInfoList[i].MusicName + "      " + musicInfoList[i].MusicSinger;

        }
        ScrollbarList.value = 1;
    }

    public void KillObj ()
    {
        if (Content.transform.childCount > 0)
        {
            foreach (Transform child in Content.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }

    public void ListStartAnimate ()
    {
        MusicView.SetActive(true);
        Viewport.SetActive(true);
        DOTween.To(() => MusicView.GetComponent<Image>().fillAmount, y => MusicView.GetComponent<Image>().fillAmount = y, 1, 0.5f);
    }

    public bool GameObjBool (string ObjectName)
    {
        if (GameObject.Find(ObjectName) != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// ¹Ø±ÕËÑË÷¿ò
    /// </summary>
    public void ReturnMusicSearch ()
    {
        KillObj();
        DOTween.To(() => MusicView.GetComponent<Image>().fillAmount, y => MusicView.GetComponent<Image>().fillAmount = y, 0, 0.5f).OnComplete(ReturnMusicSearch_Event);
        UIAnimate.ToUp(this.gameObject);
        void ReturnMusicSearch_Event ()
        {
            MusicView.SetActive(false);
            Viewport.SetActive(false);
            this.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }
}