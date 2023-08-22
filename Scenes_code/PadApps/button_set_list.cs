using UnityEngine;
using UnityEngine.UI;

public class button_set_list : MonoBehaviour
{
    public GameObject[] buttonlist = new GameObject[10];
    public GameObject[] view = new GameObject[10];
    public Sprite normal;
    public Sprite now;
    public int listindex;

    public void rest (int index)
    {
        nowbutton(index);
    }

    private void nowbutton (int index)
    {
        for (int i = 1; i < 4; i++)
        {
            buttonlist[i].GetComponent<Image>().sprite = normal;
            view[i].SetActive(false);
        }
        buttonlist[index].GetComponent<Image>().sprite = now;
        view[index].SetActive(true);
    }
}