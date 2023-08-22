using config;
using UnityEngine;
using UnityEngine.UI;

public class button_choice : MonoBehaviour
{
    private ConfigXML configXML = new ConfigXML();
    public GameObject obj_title;
    public GameObject obj_txt;
    public GameObject obj_photo;
    public GameObject obj_from;
    public static int nowindex;

    public void mouseon ()
    {
        //抹茶水羊羹| 一口清凉嫩绿质地，入口即化，透着淡淡的茶香，是清凉与解暑的佳品。| 水羊羹 | zizhi_food5 | 抹茶 | 琼脂 | 豆沙 | 0.5
        string tempname = this.gameObject.transform.name;
        string[] tempname1 = new string[2];
        tempname1 = tempname.Split('e');
        int.TryParse(tempname1[1], out nowindex);
        string finally_txt = configXML.读配置项_StreamingAssets("food" + tempname1[1], "config/food_detail/zizhi_food.xml");
        string[] tempname2 = new string[10];
        tempname2 = finally_txt.Split('|');
        obj_title.GetComponent<Text>().text = tempname2[0];
        obj_txt.GetComponent<Text>().text = tempname2[1];
        obj_from.GetComponent<Text>().text = tempname2[2];
        obj_photo.GetComponent<Image>().sprite = Resources.Load<Sprite>(tempname2[3]);
    }
}