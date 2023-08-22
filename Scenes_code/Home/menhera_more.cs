using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class menhera_more : MonoBehaviour
{
    public GameObject obj_more_background;
    public GameObject obj_more;
    private bool state;
    private Tweener open_event;

    private Tweener open_list ()
    {
        return DOTween.To(() => obj_more_background.GetComponent<Image>().fillAmount, x => obj_more_background.GetComponent<Image>().fillAmount = x, 1f, 0.25f);
    }

    private Tweener close_list ()
    {
        return DOTween.To(() => obj_more_background.GetComponent<Image>().fillAmount, x => obj_more_background.GetComponent<Image>().fillAmount = x, 0f, 0.25f);
    }

    public void button_more ()
    {
        if (state == false)
        {
            open_event = open_list();
            open_event.OnComplete(openevent_fina);
        }
        else
        {
            DOTween.Kill(open_event);
            close_list();
            state = false;
            obj_more.SetActive(false);
        }
    }

    private void openevent_fina ()
    {
        obj_more.SetActive(true);
        state = true;
    }

    public void close ()
    {

        GameObject.Find("EventSystem").GetComponent<TouchMenhera>().Button_Click_Living_out();




    }

}