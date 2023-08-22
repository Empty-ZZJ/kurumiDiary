using UnityEngine;

public class IpadCanvas : MonoBehaviour
{
    public void Click_Button_Close ()
    {
        GameObject.Find("kitchen/Furniture_Kitchencabinet_Forest/MPad").GetComponent<ipadclose>().onipadclose();
    }
}