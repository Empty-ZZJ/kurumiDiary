using DG.Tweening;
using UnityEngine;

public class fridgefoor : MonoBehaviour
{
    public GameObject door;
    public GameObject maincamera;// to:P Vector3(0.569999993,1.30799997,7.65799999)    R Vector3(10.5826902,0,0)
    public GameObject FridgeCanvas;
    public GameObject mainCanvas;
    public GameObject list;
    private Vector3 from_posite;
    private Tweener overfridge;
    private Tweener openfridge;
    private Tweener nonefirdge;
    public AudioSource scence_audio;
    public AudioClip open;
    public AudioClip close;
    public AudioClip open_none;
    public AudioClip close_none;
    public static int mode = 1;

    public void OnMouse ()
    {
        if (mode == 1 && SceneCameraMove.IsMove_Click())
        {
            mainCanvas.SetActive(false);
            mode = 0;
            list.SetActive(false);
            SceneCameraMove.bool_move = false;
            from_posite = maincamera.transform.position;
            scence_audio.clip = open_none;
            scence_audio.Play();
            maincamera.transform.DOMove(new Vector3(0.569999933f, 1.35599995f, 7.42999983f), 0.75f);
            openfridge = maincamera.transform.DORotate(new Vector3(10.5826902f, 0f, 0f), 0.75f, RotateMode.FastBeyond360);
            openfridge.OnComplete(openfirdge);
        }
        else if (mode == 2)
        {
            mode = 0;
            FridgeCanvas.SetActive(true);
            scence_audio.clip = open;
            scence_audio.Play();
            nonefirdge = door.transform.DOLocalRotate(new Vector3(270f, 120.000008f, 0f), 0.75f, RotateMode.FastBeyond360);
            nonefirdge.OnComplete(event_none);
        }
    }

    public void closefridge ()
    {
        if (mode == 2)
        {
            mainCanvas.SetActive(true);
            mode = 0;
            scence_audio.clip = close_none;
            scence_audio.Play();
            maincamera.transform.DOMove(from_posite, 0.75f);
            overfridge = maincamera.transform.DORotate(new Vector3(0f, 0f, 0f), 0.75f, RotateMode.FastBeyond360);
            overfridge.OnComplete(event_closefirdge);
        }
        else if (mode == 3)
        {
            mainCanvas.SetActive(true);
            mode = 0;
            scence_audio.clip = close;
            scence_audio.Play();
            maincamera.transform.DOMove(from_posite, 0.75f);
            overfridge = maincamera.transform.DORotate(new Vector3(0f, 0f, 0f), 0.75f, RotateMode.FastBeyond360);
            overfridge.OnComplete(event_closefirdge);
            nonefirdge = door.transform.DOLocalRotate(new Vector3(270f, 0f, 0f), 0.75f, RotateMode.FastBeyond360);
        }
    }

    public void event_closefirdge ()
    {
        SceneCameraMove.bool_move = true;
        FridgeCanvas.SetActive(false);
        list.SetActive(false);
        mode = 1;
    }

    public void openfirdge ()
    {
        mode = 2;
        FridgeCanvas.SetActive(true);
    }

    public void event_none ()
    {
        list.SetActive(true);
        mode = 3;
    }
}