using config;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class UserName : MonoBehaviour
{
    private ConfigXML configXML = new ConfigXML();
    public InputField txtname;
    private AsyncOperation operation;

    public void buttonon ()
    {
        txtname.text = this.transform.name;
    }

    public void custom_name ()
    {
        string wantname = txtname.text;

        if (bool_name_true(wantname))
        {
            if (File.Exists(Application.persistentDataPath + "/playerinformation.xml") == true)
            {
                if (configXML.读配置项("name", "playerinformation.xml") == "None")
                {
                    configXML.加入配置项("name", wantname, "playerinformation.xml");
                }
                else
                {
                    configXML.更新配置项("name", wantname, "playerinformation.xml");
                }
            }
            else
            {
                configXML.创建配置文件("name", wantname, "playerinformation.xml");
            }
            Destroy(this.gameObject.transform.parent.transform.parent.gameObject);
        }
        else
        {
            new PopNewMessage("您的昵称不合法");
        }
    }

    /// <summary>
    /// 检查用户名是否有效
    /// </summary>
    /// <param name="boolname"></param>
    /// <returns></returns>
    private bool bool_name_true (string boolname)
    {
        if (boolname == "None")
        {
            return false;
        }
        // 检查用户名长度是否小于等于1
        if (boolname.Length <= 1)
        {
            return false;
        }

        // 检查是否包含特殊字符以外的其他字符
        for (int i = 0; i < boolname.Length; i++)
        {
            if (!char.IsLetterOrDigit(boolname[i]) && boolname[i] != '_')
            {
                return false;
            }
        }

        // 当用户名只有一个字符时，检查它是否是纯数字
        if (boolname.Length == 1 && char.IsDigit(boolname[0]))
        {
            return false;
        }

        // 用户名有效
        return true;
    }

    public static void Update_name (string wantname)
    {
        if (File.Exists(Application.persistentDataPath + "/playerinformation.xml") == true)
        {
            if (plotbutton.ReadXML("name", "name", "playerinformation.xml") == "None")
            {
                plotbutton.AddXML("name", wantname, "name", "playerinformation.xml");
            }
            else
            {
                plotbutton.UpdateXml("name", plotbutton.ReadXML("name", "name", "playerinformation.xml"), wantname, "playerinformation.xml");
            }
        }
    }

    public static string Get_name ()
    {
        if (File.Exists(Application.persistentDataPath + "/playerinformation.xml") == true)
        {
            if (plotbutton.ReadXML("name", "name", "playerinformation.xml") == "None")
            {
                return "None";
            }
            else
            {
                return plotbutton.ReadXML("name", "name", "playerinformation.xml");
            }
        }
        else return "None";
    }
}