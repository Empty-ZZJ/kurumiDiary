using TMPro;
using UnityEngine;

public class button_night : MonoBehaviour
{
    public GameObject dialog;
    public GameObject txt;
    public Animator night_move;
    private float duration = 3.45f;

    public void button_menhera ()
    {
        if (dialog.activeSelf == true) return;
        night_move.Play("night_button");
        dialog.SetActive(true);
        txt.SetActive(true);
        int a = NewRandom.GetRandomInAB(1, 4);
        switch (a)
        {
            case 1:
                txt.GetComponent<TextMeshProUGUI>().text = "时候不早了，你也早点休息吧";
                break;

            case 2:
                txt.GetComponent<TextMeshProUGUI>().text = "美梦中......";
                break;

            case 3:
                txt.GetComponent<TextMeshProUGUI>().text = "胡桃..不想吃菌菇...";
                break;

            case 4:
                txt.GetComponent<TextMeshProUGUI>().text = "呼呼呼....";
                break;

            case 5:
                txt.GetComponent<TextMeshProUGUI>().text = "还没睡吗，要早点休息呀";
                break;

            case 6:
                txt.GetComponent<TextMeshProUGUI>().text = "唔..怎么了？";
                break;
        }
        Invoke("MethodName", duration);
    }

    private void MethodName ()
    {
        dialog.SetActive(false);
    }
}