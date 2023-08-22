using UnityEngine;
using UnityEngine.UI;

public class sensitivity : MonoBehaviour
{
    public GameObject _Slider;

    private void Start ()
    {
        if (GameConfig.GetValue("sensitivity", "setting.xml") != "None")
        {
            float temp;//0.002
            float.TryParse(GameConfig.GetValue("sensitivity", "setting.xml"), out temp);
            _Slider.GetComponent<Slider>().value = temp;
        }
        else
        {
            _Slider.GetComponent<Slider>().value = 0.002f;
            GameConfig.SetValue("sensitivity", _Slider.GetComponent<Slider>().value.ToString(), "setting.xml");
        }

    }

    public void update_sensitivity ()
    {
        float temp;
        float.TryParse(GameConfig.GetValue("sensitivity", "setting.xml"), out temp);
        GameConfig.SetValue("sensitivity", _Slider.GetComponent<Slider>().value.ToString(), "setting.xml");

    }
}