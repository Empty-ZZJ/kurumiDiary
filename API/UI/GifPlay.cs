using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GifPlay : MonoBehaviour
{
    // Start is called before the first frame update
    private int index = 1;

    private void OnEnable ()
    {
        Debug.Log("Gif");
        StartCoroutine(StartAction());
    }

    private void OnDisable ()
    {
        StopCoroutine(StartAction());
    }

    public IEnumerator StartAction ()
    {
        while (true)
        {
            if (index <= 60)
            {
                this.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Loading/" + index.ToString());
                index++;
            }
            else
            {
                index = 1;
                this.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Loading/" + index.ToString());
            }
            yield return new WaitForSeconds(0.008f);
        }
    }
}