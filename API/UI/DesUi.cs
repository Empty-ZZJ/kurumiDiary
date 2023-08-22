using UnityEngine;
using UnityEngine.UI;

public class DesUi : MonoBehaviour
{
    public int mode = 0;

    public void Start ()
    {
        switch (mode)
        {
            case 0:
                this.GetComponent<Button>().onClick.AddListener(delegate { DesThis(); });
                break;

            case 1:
                this.GetComponent<Button>().onClick.AddListener(delegate { DesParent(); });
                break;
        }
    }

    public void DesThis ()
    {
        Destroy(this.gameObject);
    }

    public void DesParent ()
    {
        Destroy(this.transform.parent.gameObject);
    }
}