using UnityEngine;
using UnityEngine.UI;

public class timechoice : MonoBehaviour
{
    public GameObject timetxtlist;
    public GameObject view;
    public GameObject choicetime;
    public GameObject imagetime;
    public GameObject txtobj;
    public GameObject timeerror;

    public void ViewChange ()
    {
        float fltemp = view.GetComponent<Slider>().value;
        float seconde = fltemp * 3600;
        int M = (int)(seconde / 60);
        float S = seconde % 60;
        timetxtlist.GetComponent<Text>().text = M + ":" + string.Format("{00:00}", S);
    }

    public void returnview ()
    {
        UIAnimate.ToLeft(choicetime);
    }

    public void oktime ()
    {
        if (view.GetComponent<Slider>().value * 3600 != 0)
        {
            UIAnimate.ToLeft(choicetime);
            TimeFocus_Core.GameTime = view.GetComponent<Slider>().value * 3600;
            TimeFocus_Core.bool_start = true;
            txtobj.GetComponent<Text>().text = timetxtlist.GetComponent<Text>().text;
            imagetime.GetComponent<Image>().fillAmount = TimeFocus_Core.GameTime / 3600f;
        }
        else
        {
            UIAnimate.ToMiddle(timeerror);
        }

        //choicetime.transform.DOMoveX(4000f, 1);
    }

    public void button_error ()
    {
        UIAnimate.ToButtom(timeerror);
    }
}