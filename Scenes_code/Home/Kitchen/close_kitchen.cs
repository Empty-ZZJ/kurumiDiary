using UnityEngine;

public class close_kitchen : MonoBehaviour
{
    public GameObject button_close;
    public GameObject KItchenCanvas;
    public GameObject mainCanvas;

    public void button_close_kitchen ()
    {
        mainCanvas.SetActive(true);
        KItchenCanvas.SetActive(false);
    }
}