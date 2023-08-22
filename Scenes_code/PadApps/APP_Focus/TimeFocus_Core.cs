using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimeFocus_Core : MonoBehaviour
{
    private AsyncOperation operation;
    public GameObject txtobj;
    public static float GameTime = 1800f;
    public float timer = 0;
    public GameObject txt_game;
    public GameObject imagetime;
    public GameObject windowsp;
    public GameObject choicetime;
    public static bool bool_start;
    public GameObject gamepverCanvas;

    /// <summary>
    /// 表示当前设定的定时时间。
    /// </summary>
    public float resaultlove;

    public static int State;

    public void Start ()
    {
        State = 0;
    }

    public void Starttime ()
    {
        if (txt_game.GetComponent<Text>().text == "结束")
        {
            UIAnimate.ToMiddle(windowsp);
            //windowsp.transform.DOMoveX(722f,1);
            return;
        }
        if (bool_start == true)
        {
            txt_game.GetComponent<Text>().text = "结束";

            resaultlove = GameTime;
            Debug.Log(resaultlove);
            State = 1;
            StartCoroutine("ChangeTime");
        }
        else if (bool_start == false)
        {
            UIAnimate.ToMiddle(choicetime);
            //choicetime.transform.DOMoveX(722f, 1);
        }
    }

    private IEnumerator ChangeTime ()
    {
        if (GameTime <= 3600f)
        {
            while (GameTime > 0)
            {
                imagetime.GetComponent<Image>().fillAmount = GameTime / 3600f;
                int M = (int)(GameTime / 60);
                float S = GameTime % 60;
                timer += Time.deltaTime;
                if (timer >= 1f)
                {
                    timer = 0;
                    GameTime--;
                    txtobj.GetComponent<Text>().text = M + ":" + string.Format("{00:00}", S);
                }
                yield return null;
            }
            GameOver();
        }
    }

    private void GameOver ()
    {
        //判定倒计时结束时该做什么的方法
        // lovedegree.add_degree(resaultlove / 36000f);
        LoveDegree.AddLoveValue(Convert.ToInt32(resaultlove / 100));
        txt_game.GetComponent<Text>().text = "开始";
        GameTime = 1800f;
        imagetime.GetComponent<Image>().fillAmount = GameTime / 3600f;
        txtobj.GetComponent<Text>().text = "30:00";
        gamepverCanvas.transform.DOMoveY(choicetime.transform.position.y, 1);
        Debug.Log(resaultlove / 36000f);
        bool_start = false;
        State = 2;
    }

    public void button_insist ()
    {
        //windowsp.transform.DOMoveX(-500f, 1);
        UIAnimate.ToLeft(windowsp);
    }

    public void button_giveup ()
    {
        //windowsp.transform.DOMoveX(-500f, 1);
        UIAnimate.ToLeft(windowsp);
        StopCoroutine("ChangeTime");
        txt_game.GetComponent<Text>().text = "开始";
        GameTime = 1800f;
        imagetime.GetComponent<Image>().fillAmount = GameTime / 3600f;
        txtobj.GetComponent<Text>().text = "30:00";
        bool_start = false;
        State = 3;
    }
}