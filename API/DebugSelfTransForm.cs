using UnityEngine;

public class DebugSelfTransForm : MonoBehaviour
{
    public void Update ()
    {
        Debug.Log(this.gameObject.transform.position);
    }

}
