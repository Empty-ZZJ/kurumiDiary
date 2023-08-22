using System;
using System.Collections;
using UnityEngine;

public class Audiosystem : MonoBehaviour
{
    public GameObject obj_mainaudio;
    public GameObject obj_backgroundaudio;
    private string NowAudio;

    public void change_audio (AudioClip wantclip)
    {
        obj_mainaudio.GetComponent<AudioSource>().clip = wantclip;
        obj_mainaudio.GetComponent<AudioSource>().Play();
    }

    public void change_backaudio (AudioClip wantclip)
    {
        obj_backgroundaudio.GetComponent<AudioSource>().clip = wantclip;
        obj_backgroundaudio.GetComponent<AudioSource>().Play();
    }

    private void Awake ()
    {
        change_audio(Resources.Load<AudioClip>($"Audio/Back_Main"));
        int.TryParse(GameConfig.GetValue("TimeDifference", "setting.xml"), out UsualValue.TimeDifference);
        Debug.Log($"系统时差：{UsualValue.TimeDifference}");
        UpdateAudioWithTimezoneOffset(UsualValue.TimeDifference);
        StartCoroutine(HandleTime());
    }

    public void UpdateAudioWithTimezoneOffset (int offset)
    {
        DateTime now = DateTime.Now.AddHours(offset);

        int nowHour = now.Hour;

        if (nowHour >= 7 && nowHour < 8)
        {
            // 起床时间
            PlayBackgroundAudio("Back_Day");
        }
        else if (nowHour >= 8 && nowHour < 12)
        {
            // 吃饭时间（早餐）
            PlayBackgroundAudio("Back_Day");
        }
        else if (nowHour >= 12 && nowHour < 13)
        {
            // 吃饭时间（午餐）
            PlayBackgroundAudio("Back_Day");
        }
        else if (nowHour >= 18 && nowHour < 19)
        {
            // 吃饭时间（晚餐）
            PlayBackgroundAudio("Back_Day");
        }
        else if (nowHour >= 23 || (nowHour >= 0 && nowHour < 8))
        {
            // 睡觉时间
            PlayBackgroundAudio("Back_Night");
        }
        else
        {
            // 活动时间
            PlayBackgroundAudio("Back_Day");
        }
    }

    private void PlayBackgroundAudio (string clipName)
    {
        if (NowAudio != clipName)
        {
            AudioSource audioSource = obj_backgroundaudio.GetComponent<AudioSource>();
            AudioClip audioClip = Resources.Load<AudioClip>($"Audio/{clipName}");

            if (audioClip != null)
            {
                audioSource.clip = audioClip;
                audioSource.Play();
            }
            else
            {
                Debug.Log("Failed to load audio clip.");
            }
            NowAudio = clipName;
        }
    }

    private IEnumerator HandleTime ()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f); // 等待 3 秒
            UpdateAudioWithTimezoneOffset(UsualValue.TimeDifference);
        }
    }
}