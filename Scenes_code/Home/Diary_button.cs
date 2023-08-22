using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Diary_button : MonoBehaviour
{
    public Transform cmaeratran;
    public Animator animator_system;
    public AudioSource audiosystem;
    public AudioClip open;
    public AudioClip close;
    private Vector3 _FromPosition;
    private Tweener event_open;

    private void Start ()
    {
        _FromPosition = gameObject.transform.position;
        //Debug.Log(//fromposition.position);
    }

    public void button_in ()
    {
        if (GetComponent<Button>().interactable == true && SceneCameraMove.IsMove_Click())
        {
            GetComponent<Button>().interactable = false;
            Vector3 to_position = cmaeratran.position;
            audiosystem.clip = open;
            audiosystem.Play();
            to_position.z += 0.407f;
            to_position.x += 0.15f;
            SceneCameraMove.SetMoveFalse();
            animator_system.Play("PhotoAlbum_skinopen");
            event_open = this.gameObject.transform.DOMove(to_position, 0.5f);
            event_open.OnComplete(Event_set_state_diary_true);
        }
    }

    public void button_out ()
    {
        Debug.Log("πÿ±’»’º«");
        animator_system.Play("photoAlbum_opened_Close");
        audiosystem.clip = close;
        audiosystem.Play();
        this.gameObject.transform.DOMove(_FromPosition, 0.5f).OnComplete(overevent);
        SceneCameraMove.bool_move = true;/*
        (3.12, 0.44, 4.27)
UnityEngine.Debug:Log(object)
diary_button: Start()(at Assets / Code / Scenes_code / MainGame / diary_button.cs:20)*/
    }

    private void overevent ()
    {
        GetComponent<Button>().interactable = true;
    }

    public void Event_set_state_diary_true ()
    {
        Instantiate(Resources.Load<GameObject>("Prefabs/UI/ScenesUI/Diary/DiaryCanvas"));
        animator_system.Play("openstate");
        /*
        Temp_skybox.Temp_Core_skybox_on();
        animator_system.Play("openstate");
        Maincanvas.SetActive(false);
        DiaryCanvas.SetActive(true);
        //Instantiate(Resources.Load<GameObject>("Prefabs/UI/DiaryCanvas"), this.transform);
        Vector3 to_position = cmaeratran.position;
        to_position.z += 0.407f;
        to_position.x += 0.15f;
        this.transform.DOMove(to_position, 0.25f);
        */
    }
}