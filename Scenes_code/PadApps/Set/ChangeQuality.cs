using config;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeQuality : MonoBehaviour
{
    /**
     *                    _ooOoo_
     *                   o8888888o
     *                   88" . "88
     *                   (| -_- |)
     *                    O\ = /O
     *                ____/`---'\____
     *              .   ' \\| |// `.
     *               / \\||| : |||// \
     *             / _||||| -:- |||||- \
     *               | | \\\ - /// | |
     *             | \_| ''\---/'' | |
     *              \ .-\__ `-` ___/-. /
     *           ___`. .' /--.--\ `. . __
     *        ."" '< `.___\_<|>_/___.' >'"".
     *       | | : `- \`.;`\ _ /`;.`/ - ` : | |
     *         \ \ `-. \_ __\ /__ _/ .-` / /
     * ======`-.____`-.___\_____/___.-`____.-'======
     *                    `=---='
     *
     * .............................................
     *          佛祖保佑             永无BUG
*/
    public List<GameObject> List_Toggle;
    private ConfigXML configXML = new ConfigXML();

    public void Awake ()
    {
        foreach (var item in List_Toggle)
        {
            item.GetComponent<Toggle>().onValueChanged.AddListener((bool value) => OnValueChange(item.GetComponent<Toggle>()));
        }

        if (this.gameObject.name == "GameObject")
        {
            if (configXML.读配置项("Quality", "playerinformation.xml") != "None")
            {
                Static_ChangeQuality(Convert.ToInt32(configXML.读配置项("Quality", "playerinformation.xml")));
                List_Toggle[Convert.ToInt32(configXML.读配置项("Quality", "playerinformation.xml"))].GetComponent<Toggle>().isOn = true;
            }
            else
            {
                configXML.加入配置项("Quality", QualitySettings.GetQualityLevel().ToString(), "playerinformation.xml");
            }
        }
    }

    private void OnValueChange (Toggle t)
    {
        if (t.isOn)
        {
            configXML.更新配置项("Quality", t.name.Split('e')[1], "playerinformation.xml");
            SetChangeQuality(Convert.ToInt32(t.name.Split('e')[1]));
        }
    }

    public static void Static_ChangeQuality (int level)
    {
        QualitySettings.SetQualityLevel(level);
    }

    public void SetChangeQuality (int level)
    {
        try
        {
            QualitySettings.SetQualityLevel(level);
        }
        catch (Exception ex)
        {
            new PopNewMessage(ex.Message);
        }
    }
}