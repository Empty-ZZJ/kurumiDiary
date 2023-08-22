using UnityEngine;

public class Courtyard : MonoBehaviour
{
    public void Button_Click_ToYard ()
    {
        Instantiate(Resources.Load<GameObject>("Prefabs/UI/Yard/ToYardCanvas"));
        HL.IO.HL_Log.Log(nameof(Button_Click_ToYard), "Error");
    }
}
