using DG.Tweening;
using TMPro;
using UnityEngine;

public class NoviceTutorial : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject txt;

    public GameObject txt2;
    public GameObject maincamera;
    public GameObject mainCanvas;
    public GameObject mainpaint;
    public GameObject mask;
    public bool bool_NoviceTutorial;
    public int nowindexl;
    private Tweener twe;

    private void Awake ()
    {
        //GraduallyShow();
        //GraduallynoShow();
        mainCanvas.SetActive(true);
        bool_NoviceTutorial = true;
        mainpaint.SetActive(false);
        /*
        if (File.Exists(Application.persistentDataPath + "/NoviceTutorial.xml") == false)
        {
            plotbutton.CreateXML("NoviceTutorial", "NoviceTutorial", "true", "NoviceTutorial.xml");
            bool_NoviceTutorial = false;
            mask.SetActive(true);
            start_NoviceTutorial();
        }
        else
        {
            mainCanvas.SetActive(true);
            bool_NoviceTutorial = true;
            mainpaint.SetActive(false);
        }
        */
    }

    public void start_NoviceTutorial ()
    {
        twe = maincamera.transform.DOMoveX(1.449f, 30);
        twe.OnComplete(last);
        GraduallyShow(txt, "在繁华而喧嚣的城市中");
        GraduallyShow(txt2, "每一个人都为了生活而奔波忙碌着");
    }

    // Update is called once per frame
    protected void GraduallyShow (GameObject txta, string nowtxt)
    {
        Transform[] transforms = txta.GetComponentsInChildren<Transform>();
        foreach (var transformChild in transforms)
        {
            TextMeshProUGUI text = txta.GetComponent<TextMeshProUGUI>();
            if (text)
            {
                text.text = nowtxt;
                text.DOColor(new Color(text.color.r, text.color.g, text.color.b, 255), 2);
            }
        }
    }

    protected void GraduallynoShow (GameObject txta)
    {
        Transform[] transforms = txta.GetComponentsInChildren<Transform>();
        foreach (var transformChild in transforms)
        {
            TextMeshProUGUI text = txta.GetComponent<TextMeshProUGUI>();
            if (text)
            {
                text.text = "";
                text.DOColor(new Color(text.color.r, text.color.g, text.color.b, 0), 2);
            }
        }
    }

    public void FixedUpdate ()
    {
        Application.targetFrameRate = 60;
        if (Time.frameCount % 240 == 0)
        {
            if (bool_NoviceTutorial == false)
            {
                nowindexl++;

                if (nowindexl == 1)
                {
                    GraduallynoShow(txt);
                    GraduallynoShow(txt2);
                    GraduallyShow(txt, "日子一天又一天重复着 循环着");
                    GraduallyShow(txt2, "却始终缺少一些色彩");
                }
                if (nowindexl == 2)
                {
                    GraduallynoShow(txt);
                    GraduallynoShow(txt2);
                    GraduallyShow(txt, "然而就在不久前");
                    GraduallyShow(txt2, "一个女孩出现在了我的生活中");
                }
                if (nowindexl == 3)
                {
                    GraduallynoShow(txt);
                    GraduallynoShow(txt2);
                    GraduallyShow(txt, "从此我的生活");
                    GraduallyShow(txt2, "不再如此平淡");
                }
            }
        }
    }

    public void last ()
    {
        bool_NoviceTutorial = true;
        mainCanvas.SetActive(true);
        mainpaint.SetActive(false);
    }
}