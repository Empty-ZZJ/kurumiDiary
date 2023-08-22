using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public Sprite[] sprites = new Sprite[7];
    public Sprite[] sprites2 = new Sprite[19];
    public GameObject photo;
    private int index = 1;

    private void OnEnable ()
    {
        int mode = NewRandom.GetRandomInAB(1, 2);
        if (mode == 1)
        {
            index = 1;
            StartCoroutine(StartActionMode1Coroutine());
        }
        else
        {
            index = 1;
            StartCoroutine(StartActionMode2Coroutine());
        }
    }

    public IEnumerator StartActionMode1Coroutine ()
    {
        while (true)
        {
            if (index <= 6)
            {
                photo.GetComponent<Image>().sprite = sprites[index];
                index++;
            }
            else
            {
                index = 1;
                photo.GetComponent<Image>().sprite = sprites[index];
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    public IEnumerator StartActionMode2Coroutine ()
    {
        while (true)
        {
            if (index <= 18)
            {
                photo.GetComponent<Image>().sprite = sprites2[index];
                index++;
            }
            else
            {
                index = 1;
                photo.GetComponent<Image>().sprite = sprites2[index];
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}