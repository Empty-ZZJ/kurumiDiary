using UnityEngine;

public class KitchenResourcesCode : MonoBehaviour
{
    //closekitchen();
    public void Button_Click_QuitKitchen ()
    {
        DestroyImmediate(this.gameObject);
        GameObject.Find("kitchen/Furniture_Kitchenfire_Study #40892").GetComponent<tokitchen>().closekitchen();
    }
}