using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class NameEvent : MonoBehaviour
{
    public float WaitTime = 1;
    private Tweener EvemtName;
    private Tweener EvemtName2;
    private bool 是否需要滚动;

    private void Start ()
    {
        Debug.Log(this.GetComponent<RectTransform>().anchoredPosition);
        //StartChange();
    }

    private void StartChange ()
    {
        if (GetComponent<Text>().text.Length > 9)//超出显示，进行滚动
        {
            int ExceededQuantity = GetComponent<Text>().text.Length - 8;
            var ToPosition = new Vector2(-2.7f, ExceededQuantity * 20.1f);
            EvemtName = DOTween.To(() => this.GetComponent<RectTransform>().anchoredPosition, y => this.GetComponent<RectTransform>().anchoredPosition = y, ToPosition, 2f).OnComplete(ToTop_Text);
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
        var ToPosition = new Vector2(-2.7f, 20.1f);
        EvemtName2 = DOTween.To(() => this.GetComponent<RectTransform>().anchoredPosition, y => this.GetComponent<RectTransform>().anchoredPosition = y, ToPosition, 2f).OnComplete(Start_Text);
    }

    private void FixedUpdate ()
    {
        if (是否需要滚动 == true) return;
        if (GetComponent<Text>().text.Length > 9)
        {
            是否需要滚动 = true;
            StartChange();
        }
        else
        {
            是否需要滚动 = false;
        }
    }
}