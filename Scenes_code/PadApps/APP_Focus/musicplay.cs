using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class musicplay : MonoBehaviour
{
    public static bool musicstate = false;
    public GameObject sign;
    public AudioSource maindudio;
    public Text MusicNameLabel;

    public void button_music ()
    {
        if (musicstate == false)
        {
            musicstate = true; int index = NewRandom.GetRandomInAB(1, 15);
            var sudiofile = Resources.Load<AudioClip>("Music/sound" + index.ToString());
            maindudio.clip = sudiofile;
            maindudio.Play();
            StartCoroutine("AudioCallBack");
            sign.SetActive(false);
            MusicNameLabel.text = "ƒ⁄÷√“Ù¿÷:" + index.ToString();
        }
        else
        {
            LrcEvent.IsLRc = false;
            musicstate = false;
            sign.SetActive(true);
            maindudio.Pause();
            StopCoroutine("AudioCallBack");
            MusicNameLabel.text = "‘›Œﬁ“Ù¿÷≤•∑≈";
        }
    }

    public IEnumerator AudioCallBack ()
    {
        Debug.Log(maindudio.isPlaying);
        while (maindudio.isPlaying)
        {
            yield return new WaitForSecondsRealtime(0.1f);//—”≥Ÿ¡„µ„“ª√Î÷¥––
        }
        nextmusci();
    }

    public void nextmusci ()
    {
        int index = NewRandom.GetRandomInAB(1, 15);
        var sudiofile = Resources.Load<AudioClip>("music/sound" + index.ToString());
        maindudio.clip = sudiofile;
        maindudio.Play();
        StartCoroutine("AudioCallBack");
        MusicNameLabel.text = "ƒ⁄÷√“Ù¿÷:" + index.ToString();
    }
}