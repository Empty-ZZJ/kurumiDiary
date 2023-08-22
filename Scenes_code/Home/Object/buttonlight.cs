using UnityEngine;

public class buttonlight : MonoBehaviour
{
    public GameObject wantlight;
    public AudioClip wantlightaudio;
    public AudioSource audioplayer;

    // Start is called before the first frame update
    private void Start ()
    {
    }

    // Update is called once per frame
    private void Update ()
    {
    }

    public void lighton ()
    {
        Debug.Log("lighton");
        audioplayer.GetComponent<AudioSource>().clip = wantlightaudio;
        audioplayer.GetComponent<AudioSource>().Play();
        wantlight.GetComponent<Animation>().Play("light");
    }
}