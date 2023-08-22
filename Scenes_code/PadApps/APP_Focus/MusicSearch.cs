using UnityEngine;

public class MusicSearch : MonoBehaviour
{
    public GameObject MusicSearchPanle;
    public GameObject MusicGameObject;

    public void Button_On ()
    {
        MusicSearchPanle.SetActive(true);
        UIAnimate.ToMiddle(MusicGameObject);
    }
}