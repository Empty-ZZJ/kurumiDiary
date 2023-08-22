using UnityEngine;
using UnityEngine.UI;

public class loadlist : MonoBehaviour
{
    public Dropdown province;
    public Text text_province;
    private string[] list_province = new string[] { "北京", "天津", "上海", "重庆", "河北", "山西", "吉林", "黑龙江", "江苏", "浙江", "安徽", "福建", "江西", "山东", "河南", "湖北", "湖南", "广东", "海南", "四川", "贵州", "云南", "陕西", "甘肃", "青海", "内蒙古", "广西", "西藏", "宁夏", "新疆", "香港", "澳门" };

    private void Start ()
    {
        for (int i = 0; i < list_province.Length; i++) loadlist_province(list_province[i]);
        text_province.text = list_province[province.value];
    }

    public void loadlist_province (string add_province)
    {
        Dropdown.OptionData OptionData = new Dropdown.OptionData();
        OptionData.text = add_province;
        province.options.Add(OptionData);
    }

    public void load_city ()
    {
        Debug.Log(text_province.text);
    }
}