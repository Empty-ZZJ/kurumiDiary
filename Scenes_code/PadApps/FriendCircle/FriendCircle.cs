using UnityEngine;

public class FriendCircle : MonoBehaviour
{
    public void OpenFriendCircle ()
    {
        this.GetComponent<ipad>().OpenApp(0);
    }

    public void Start ()
    {
    }
}