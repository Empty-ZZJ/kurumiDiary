using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class tokitchen : MonoBehaviour
{
    private static Vector3 from_posite;
    public GameObject maincamera;
    public GameObject mainCanvas;
    public GameObject button_start;
    private Tweener tokitchen_event;
    private Tweener closekitchen_event;

    public void openkitchen ()
    {
        if (GetComponent<Button>().interactable == true && SceneCameraMove.IsMove_Click())
        {
            GetComponent<Button>().interactable = false;
            SceneCameraMove.bool_move = false;
            from_posite = maincamera.transform.position;
            mainCanvas.SetActive(false);
            maincamera.transform.DOMove(new Vector3(0.569999993f, 1.21000004f, 7.23699999f), 1);
            tokitchen_event = maincamera.transform.DORotate(new Vector3(0, -90, 0), 1, RotateMode.FastBeyond360);
            tokitchen_event.OnComplete(openkitchenc);
        }
    }

    private void openkitchenc ()
    {
        Instantiate(Resources.Load<GameObject>("Prefabs/UI/KItchenCanvas"));
    }

    private void closekitchenc ()
    {
        SceneCameraMove.bool_move = true;
        GetComponent<Button>().interactable = true;
    }

    public void closekitchen ()
    {
        mainCanvas.SetActive(true);
        maincamera.transform.DOMove(from_posite, 1);
        closekitchen_event = maincamera.transform.DORotate(new Vector3(0, 0, 0), 1, RotateMode.Fast);
        closekitchen_event.OnComplete(closekitchenc);
    }
}