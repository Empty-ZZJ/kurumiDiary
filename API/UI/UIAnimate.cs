using DG.Tweening;
using UnityEngine;

public static class UIAnimate
{
    public static Tweener ToMiddle (GameObject obj)
    {
        return (DOTween.To(() => obj.GetComponent<RectTransform>().anchoredPosition, x => obj.GetComponent<RectTransform>().anchoredPosition = x, new Vector2(0, 0), 0.5f));
    }

    public static Tweener ToLeft (GameObject obj)
    {
        return (DOTween.To(() => obj.GetComponent<RectTransform>().anchoredPosition, x => obj.GetComponent<RectTransform>().anchoredPosition = x, new Vector2(-1250, 0), 0.5f));
    }

    public static Tweener ToRight (GameObject obj)
    {
        return (DOTween.To(() => obj.GetComponent<RectTransform>().anchoredPosition, x => obj.GetComponent<RectTransform>().anchoredPosition = x, new Vector2(1250, 0), 0.5f));
    }

    public static Tweener ToUp (GameObject obj)
    {
        return (DOTween.To(() => obj.GetComponent<RectTransform>().anchoredPosition, x => obj.GetComponent<RectTransform>().anchoredPosition = x, new Vector2(0, 1250), 0.5f));
    }

    public static Tweener ToButtom (GameObject obj)
    {
        return (DOTween.To(() => obj.GetComponent<RectTransform>().anchoredPosition, x => obj.GetComponent<RectTransform>().anchoredPosition = x, new Vector2(0, 1250), 0.5f));
    }
}