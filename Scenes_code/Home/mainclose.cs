using DG.Tweening;
using System.Threading;
using UnityEngine;

public class mainclose : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject maincamera;

    public GameObject close;
    private Tweener twe;

    // Update is called once per frame
    public void colseon ()
    {
        if (carema.nowmamera == true)
        {
            twe = maincamera.transform.DOMove(new Vector3(3.06900001f, 1.09000003f, 2.42000008f), 2);
            twe.OnComplete(lastevent);
        }
    }

    public void lastevent ()
    {
        Thread.Sleep(10);
        close.SetActive(false);
        SceneCameraMove.bool_move = true;
    }
}