using UnityEngine;

public class closemessage : MonoBehaviour
{
    public GameObject message;

    public void messageclose ()
    {
        //message.transform.DOMove(new Vector3(766.80f, 1040.40f, 0.00f), 0.5f);
        UIAnimate.ToUp(message);
    }
}