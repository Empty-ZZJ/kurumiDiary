using UnityEngine;

public class none_zuan : MonoBehaviour
{
    public GameObject none_obj;
    public bool distinguish;

    public void over_message ()
    {
        Debug.Log(distinguish);
        if (distinguish == false)
        {
            distinguish = true;
            UIAnimate.ToRight(none_obj);
            return;
        }
        distinguish = false;
        UIAnimate.ToLeft(none_obj);
    }
}