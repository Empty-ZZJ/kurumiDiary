using UnityEngine;

public class clothes : MonoBehaviour
{
    public AudioClip open;
    public AudioClip close;
    public GameObject now_obj;
    private bool clothes_state = false;
    public GameObject rightdoor;
    public GameObject leftdoor;

    public void OnMouse ()
    {
        if (clothes_state == false)
        {
            clothes_state = true;
            leftdoor.GetComponent<Animation>().Play("open2");
            rightdoor.GetComponent<Animation>().Play("open");
            AudioSource now_audio = now_obj.GetComponent<AudioSource>();
            now_audio.clip = open;
            now_audio.Play();
        }
        else
        {
            clothes_state = false;
            leftdoor.GetComponent<Animation>().Play("close2");
            rightdoor.GetComponent<Animation>().Play("close");
            AudioSource now_audio = now_obj.GetComponent<AudioSource>();
            now_audio.clip = close;
            now_audio.Play();
        }
    }
}