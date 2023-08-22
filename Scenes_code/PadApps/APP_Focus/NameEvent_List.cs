using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NameEvent_List : MonoBehaviour
{
    public float WaitTime = 1;
    private bool 是否需要滚动;

    private void StartChange ()
    {
        if (GetComponent<Text>().text.Length > 20)//超出显示，进行滚动
        {
            int ExceededQuantity = GetComponent<Text>().text.Length - 19;
            var ToPosition = new Vector2(-ExceededQuantity * 7f, 0);
            DOTween.To(() => this.GetComponent<RectTransform>().anchoredPosition, y => this.GetComponent<RectTransform>().anchoredPosition = y, ToPosition, 2f).OnComplete(ToTop_Text);
        }
        else
        {
            //没有超出显示
            是否需要滚动 = false;
            this.GetComponent<RectTransform>().anchoredPosition = new Vector2(-2.7f, 0f);
        }
    }

    private void ToTop_Text ()
    {
        Invoke("ToTop_Event", WaitTime);
    }

    private void Start_Text ()
    {
        Invoke("StartChange", WaitTime);
    }

    private void ToTop_Event ()
    {
        var ToPosition = new Vector2(0f, 0f);
        DOTween.To(() => this.GetComponent<RectTransform>().anchoredPosition, y => this.GetComponent<RectTransform>().anchoredPosition = y, ToPosition, 2f).OnComplete(Start_Text);
    }

    private void FixedUpdate ()
    {
        if (是否需要滚动 == true) return;
        if (GetComponent<Text>().text.Length > 20)
        {
            是否需要滚动 = true;
            StartChange();
        }
        else
        {
            是否需要滚动 = false;
        }
    }

    public void button_ON ()
    {
        int MusicIndex = 0;
        int.TryParse(this.transform.parent.name.Split('t')[1], out MusicIndex);
        StartCoroutine(LoadAudio(CoreMusicSearchWeb.musicInfoList[MusicIndex]));
    }

    private IEnumerator LoadAudio (Music.MusicMessage Musicmessage)
    {
        string url = "http://music.163.com/song/media/outer/url?id=" + Musicmessage.MusicID + ".mp3";
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG))
        {
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.ConnectionError ||
                www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                try
                {
                    GameObject AudioSyStem = GameObject.Find("Audio Source");
                    GameObject MusicText = GameObject.Find("MUSIC_Name_All/MUSIC_Name");
                    GameObject Identification = GameObject.Find("MainCanvas/music/Identification");
                    AudioSyStem.GetComponent<AudioSource>().clip = DownloadHandlerAudioClip.GetContent(www);
                    AudioSyStem.GetComponent<AudioSource>().Play();
                    MusicText.GetComponent<Text>().text = Musicmessage.MusicName + " | " + Musicmessage.MusicSinger;
                    Identification.SetActive(false);
                    musicplay.musicstate = true;
                    StartCoroutine(GameObject.Find("MainCanvas/music").GetComponent<musicplay>().AudioCallBack());

                }
                catch (Exception ex)
                {
                    // musicstate
                    Debug.LogError(ex.Message);
                    musicplay.musicstate = false;

                }
                // 解析歌词API并赋值给LrcEvent.lyricJson
                string lyricApiUrl = "http://music.163.com/api/song/media?id=" + Musicmessage.MusicID;
                UnityWebRequest lyricRequest = UnityWebRequest.Get(lyricApiUrl);
                yield return lyricRequest.SendWebRequest();

                if (lyricRequest.result == UnityWebRequest.Result.ConnectionError ||
                    lyricRequest.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogError(lyricRequest.error);
                }
                else
                {
                    try
                    {
                        string json = lyricRequest.downloadHandler.text;
                        Debug.Log(json);
                        LrcEvent.SetLrc(json);// 假设 LrcEvent 是一个包含 lyricJson 字段的类
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError(ex.Message);
                        try
                        {
                            HL.IO.HL_Log.Log(nameof(this.name) + ex.Message, "Error");
                        }
                        catch
                        {

                        }


                    }
                }
            }
        }
    }
}