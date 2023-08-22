using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FanAnimate : MonoBehaviour
{
    public Sprite[] sprites = new Sprite[13];
    public int index = 1;
    public static bool fan_state;
    public static int mode;
    public AudioClip audio__fan;

    public void Update ()
    {
        if (mode == 1 || mode == 2)
        {
            fan_state = false;
            if (mode == 1)
            {
                index = 1;
                GetComponent<AudioSource>().clip = audio__fan;
                GetComponent<AudioSource>().Play();
                StartCoroutine(Start_fan_mode1(ok));
            }
            else
            {
                index = 12;
                GetComponent<AudioSource>().clip = audio__fan;
                GetComponent<AudioSource>().Play();

                StartCoroutine(Start_fan_mode2(ok));
            }
            mode = 0;
        }
    }

    public IEnumerator Start_fan_mode1 (Action callback)
    {
        // 当索引小于等于12时，执行以下操作
        while (index <= 12)
        {
            // 将image组件的精灵设置为sprites数组中对应索引的图像
            this.GetComponent<Image>().sprite = sprites[index];
            index++;
            // 在每个迭代之间等待0.05秒
            yield return new WaitForSeconds(0.05f);
        }
        // 将fan_state变量设置为true
        fan_state = true;
        // 如果回调函数不为空，则执行回调函数
        if (callback != null)
            callback();
    }

    public IEnumerator Start_fan_mode2 (Action callback)
    {
        while (index >= 1)
        {
            this.GetComponent<Image>().sprite = sprites[index];
            index--;
            yield return new WaitForSeconds(0.05f);
        }

        fan_state = true;

        if (callback != null)
            callback();
    }

    private void ok ()
    {
        Debug.Log("结束翻书");
        Destroy(this.gameObject);
    }
}