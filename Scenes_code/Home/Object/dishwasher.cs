using UnityEngine;

public class dishwasher : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip open;

    public AudioClip close;
    private bool dishwasher_on = false;
    public GameObject now_obj;

    private void Start ()
    {
        Debug.Log(this.gameObject.name);
    }

    // Update is called once per frame
    private void Update ()
    {
    }

    public void OnMousep ()
    {
        Debug.Log("dishwasher");
        if (dishwasher_on == false)
        {
            dishwasher_on = true;
            /*
                float i = this.gameObject.transform.localPosition.x;
                while (i<= 1.798f)
                {
                    i = i + 0.01f;
                    this.gameObject.transform.localPosition = new Vector3(i, 0.233f, 0.1360002f);
                }
            */
            GetComponent<Animation>().Play("open");
            AudioSource now_audio = now_obj.GetComponent<AudioSource>();
            now_audio.clip = open;
            now_audio.Play();
        }
        else
        {
            dishwasher_on = false;
            /*
                float i = this.gameObject.transform.localPosition.x;
                while (i >= 1.594f)
                {
                    i = i - 0.01f;
                    this.gameObject.transform.localPosition = new Vector3(i, 0.233f, 0.1360002f);
                }
            */
            GetComponent<Animation>().Play("close");
            AudioSource now_audio = now_obj.GetComponent<AudioSource>();
            now_audio.clip = close;
            now_audio.Play();
        }
    }
}