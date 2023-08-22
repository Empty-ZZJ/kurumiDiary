using UnityEngine;

public class buutonok : MonoBehaviour
{
    public GameObject gamepverCanvas;

    //1200  -300
    public void Mainok ()
    {
        int a = Random.Range(0, 2);
        if (a == 0)
        {
            UIAnimate.ToUp(gamepverCanvas);
            //gamepverCanvas.transform.DOMoveY(1200, 1);
        }
        if (a == 1)
        {
            UIAnimate.ToButtom(gamepverCanvas);
            //gamepverCanvas.transform.DOMoveY(-300, 1);
        }
    }
}