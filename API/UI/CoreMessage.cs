using DG.Tweening;
using TMPro;
using UnityEngine;

public class CoreMessage : MonoBehaviour
{
    public static string _message = null;

    private void Start ()
    {
        if (_message != null) gameObject.GetComponentInChildren<TextMeshProUGUI>().text = _message;
        gameObject.GetComponentInParent<RectTransform>().sizeDelta = new Vector2(gameObject.GetComponentInChildren<TextMeshProUGUI>().text.Length * 16 + 39f, 28.3931f);
        Invoke("FadeAway", 2f);
    }

    private void FadeAway ()
    {
        DOTween.To(() => gameObject.GetComponent<RectTransform>().anchoredPosition3D, x => gameObject.GetComponent<RectTransform>().anchoredPosition = x, new Vector3(0, 500, 0), 1f).OnComplete(Des);
    }

    private void Des ()
    {
        DestroyImmediate(gameObject.transform.parent.gameObject);
    }
}