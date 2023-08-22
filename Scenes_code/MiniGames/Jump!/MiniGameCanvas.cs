using UnityEngine;

public class MiniGameCanvas : MonoBehaviour
{
    public void Button_Click_Quit ()
    {
        Destroy(this.gameObject);
        GameObject.Find("living_room/LivingRoom_TV/TV").GetComponent<TV>().Button_Click_Quit();
    }
}