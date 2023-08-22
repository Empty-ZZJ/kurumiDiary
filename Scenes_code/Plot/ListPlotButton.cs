using UnityEngine;

public class ListPlotButton : MonoBehaviour
{
    public static int Plotindex;

    public void Button_On_core ()
    {

        int.TryParse(this.name.Split('t')[1], out Plotindex);
        if (Plotindex > 2)
        {
            new PopNewPopup("错误", "未能在服务器上检索到该剧情信息，这可能是由于当前剧情文本并未更新所导致的。可等待后续更新。");
        }
        else
        {
            Instantiate(Resources.Load<GameObject>("Prefabs/UI/Plot/DetailPlotCanvas"));
        }
        Debug.Log(Plotindex);

    }
}