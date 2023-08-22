using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class every_photo : MonoBehaviour
{
    public GameObject maincamera;//ÉãÏñ»ú
    public Vector3 from_position;
    public Tweener event_in;
    public Tweener event_out;
    public GameObject photoCanvas;
    private Button _button;
    private GameObject MainCanvas;

    public void Start ()
    {
        _button = GetComponent<Button>();
        MainCanvas = GameObject.Find("mainCanvas");
    }

    public void camera_in ()
    {
        if (_button.interactable)
        {
            _button.interactable = false;
            MainCanvas.SetActive(false);
            from_position = maincamera.transform.position;
            SceneCameraMove.bool_move = false;
            photoCanvas.SetActive(true);

            maincamera.transform.DOMove(new Vector3(4.33199978f, 1.548f, 8.50899982f), 0.5f);
            event_in = maincamera.transform.DORotate(new Vector3(0f, 335f, 0f), 0.5f, RotateMode.Fast);
            event_in.OnComplete(F_event_in);
            void F_event_in ()
            {
                photoCanvas.SetActive(true);
            }
        }
    }

    public void camera_out ()
    {
        if (!_button.interactable)
        {
            SceneCameraMove.bool_move = false;

            maincamera.transform.DOMove(from_position, 0.5f);
            event_out = maincamera.transform.DORotate(new Vector3(0f, 0f, 0f), 0.5f, RotateMode.Fast);
            event_out.OnComplete(F_event_out);
            void F_event_out ()
            {
                //GameObject.Find("mainCanvas").SetActive(true);
                photoCanvas.SetActive(false);
                SceneCameraMove.SetMoveTrue();
                _button.interactable = true;
                MainCanvas.SetActive(true);
            }
        }
    }
}