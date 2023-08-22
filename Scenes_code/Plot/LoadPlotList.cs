/*using config;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoadPlotList : MonoBehaviour
{
    public GameObject content;
    public GameObject PlotSelect;
    ConfigXML configXML = new ConfigXML();
    private void Awake ()
    {
        StartCoroutine(Thread_CreatList("藏宝图•上", "plot/1-1", 1));
        StartCoroutine(Thread_CreatList("藏宝图•中", "plot/1-2", 2));
        StartCoroutine(Thread_CreatList("藏宝图•下", "plot/1-3", 3));
        StartCoroutine(Thread_CreatList("宠物篇", "plot/1-4", 4));
        StartCoroutine(Thread_CreatList("外婆篇", "plot/1-5", 5));
        StartCoroutine(Thread_CreatList("外婆篇•下", "plot/1-6", 6));
    }
    private IEnumerator Thread_CreatList (string title, string file, int index)
    {
        content.GetComponent<RectTransform>().sizeDelta = new Vector2(index * 250, 12.9f);
        var photo = Resources.Load<Sprite>(file);
        GameObject _temp = Instantiate(PlotSelect, content.transform);
        _temp.name = "choicewhat" + index.ToString();
        _temp.gameObject.GetComponentInChildren<Text>().text = title;
        GameObject photobj = GameObject.Find("Canvas/Horizontal Scroll View/Viewport/Content/choicewhat" + index.ToString() + "/plotphoto");
        GameObject obj_lock = GameObject.Find("Canvas/Horizontal Scroll View/Viewport/Content/choicewhat" + index.ToString() + "/lock");
        photobj.gameObject.GetComponent<Image>().sprite = photo;
        if (File.Exists(Application.persistentDataPath + "/plotlock.xml") == true)
        {
            string temp = configXML.读配置项("plot" + index.ToString(), "plotlock.xml");
            Debug.Log(temp);
            if (temp == "True")
            {
                obj_lock.SetActive(false);
                _temp.GetComponent<Button>().interactable = true;
            }
        }
        else
        {
            if (index == 1)
            {
                obj_lock.SetActive(false);
                _temp.GetComponent<Button>().interactable = true;
                configXML.创建配置文件("plot" + index.ToString(), "True", "plotlock.xml");
            }
            //理论上这个else永远不会执行，因为index！=1的时候，plotlock.xml一定已经存在了。
            //但是保险起见，还是加着吧 ૮꒰ ˶• ༝ •˶꒱ა
            else
            {
                configXML.创建配置文件("plot" + index.ToString(), "False", "plotlock.xml");
                _temp.GetComponent<Button>().interactable = false;
                obj_lock.SetActive(true);
            }
        }
        yield return null;
    }
}*/

// 在Unity中，这个类用于加载藏宝图列表
using config; // 引入配置文件处理类
using System.Collections; // 引入集合命名空间
using System.IO; // 引入文件操作命名空间
using UnityEngine; // 引入Unity引擎命名空间
using UnityEngine.UI; // 引入Unity UI命名空间

public class LoadPlotList : MonoBehaviour // 定义一个名为LoadPlotList的公共类，继承自MonoBehaviour
{
    public GameObject content; // 定义一个公共GameObject类型的变量content,用于存放内容面板
    public GameObject PlotSelect; // 定义一个公共GameObject类型的变量PlotSelect,用于存放选择按钮
    private readonly ConfigXML configXML = new ConfigXML(); // 创建一个ConfigXML类型的对象configXML,用于处理配置文件

    private void Awake () // Awake方法在对象初始化时自动调用
    {
        StartCoroutine(Thread_CreatList("藏宝图•上", "plot/1-1", 1)); // 调用Thread_CreatList协程函数，传入参数"藏宝图•上"、"plot/1-1"和1
        StartCoroutine(Thread_CreatList("藏宝图•中", "plot/1-2", 2)); // 调用Thread_CreatList协程函数，传入参数"藏宝图•中"、"plot/1-2"和2
        StartCoroutine(Thread_CreatList("藏宝图•下", "plot/1-3", 3)); // 调用Thread_CreatList协程函数，传入参数"藏宝图•下"、"plot/1-3"和3
        StartCoroutine(Thread_CreatList("宠物篇", "plot/1-4", 4)); // 调用Thread_CreatList协程函数，传入参数"宠物篇"、"plot/1-4"和4
        StartCoroutine(Thread_CreatList("外婆篇", "plot/1-5", 5)); // 调用Thread_CreatList协程函数，传入参数"外婆篇"、"plot/1-5"和5
        StartCoroutine(Thread_CreatList("外婆篇•下", "plot/1-6", 6)); // 调用Thread_CreatList协程函数，传入参数"外婆篇•下"、"plot/1-6"和6
    }

    private IEnumerator Thread_CreatList (string title, string file, int index) // Thread_CreatList方法，接收三个参数，返回一个IEnumerator类型的对象
    {

        var photo = Resources.Load<Sprite>(file); // 从Resources中加载名为file的图片资源到photo变量中
        GameObject _temp = Instantiate(PlotSelect, content.transform); // 在content面板的位置实例化PlotSelect对象到_temp变量中
        _temp.name = "choicewhat" + index.ToString(); // 为_temp对象设置名字为"choicewhat"加上index的字符串形式
        _temp.gameObject.GetComponentInChildren<Text>().text = title; // 为_temp对象的子对象中的Text组件设置文本为title
        GameObject photobj = GameObject.Find("Canvas/Horizontal Scroll View/Viewport/Content/choicewhat" + index.ToString() + "/plotphoto"); // 在场景中查找名为"Canvas/Horizontal Scroll View/Viewport/Content/choicewhat"加上index的字符串形式和"plotphoto"的GameObject对象到photobj变量中
        GameObject obj_lock = GameObject.Find("Canvas/Horizontal Scroll View/Viewport/Content/choicewhat" + index.ToString() + "/lock"); // 在场景中查找名为"Canvas/Horizontal Scroll View/Viewport/Content/choicewhat"加上index的字符串形式和"lock"的GameObject对象到obj_lock变量中
        photobj.gameObject.GetComponent<Image>().sprite = photo; // 将photo变量中的图片资源赋值给photobj对象的Image组件的sprite属性
        if (File.Exists(Application.persistentDataPath + "/plotlock.xml")) // 如果plotlock.xml文件存在
        {
            string temp = configXML.读配置项("plot" + index.ToString(), "plotlock.xml"); // 从configXML对象中读取名为"plot"加上index的字符串形式和"plotlock.xml"的配置项值到temp变量中
            Debug.Log(temp); // 在控制台输出temp变量的值
            if (temp == "True") // 如果temp等于"True"
            {
                obj_lock.SetActive(false); // 将obj_lock对象设置为不活动状态
                _temp.GetComponent<Button>().interactable = true; // 将_temp对象的Button组件的interactable属性设置为true
            }
        }
        else // 如果plotlock.xml文件不存在
        {
            if (index == 1) // 如果index等于1
            {
                obj_lock.SetActive(false); // 将obj_lock对象设置为不活动状态
                _temp.GetComponent<Button>().interactable = true; // 将_temp对象的Button组件的interactable属性设置为true
                configXML.创建配置文件("plot" + index.ToString(), "True", "plotlock.xml"); // 在configXML对象中创建名为"plot"加上index的字符串形式和"True","plotlock.xml"的配置项，并将其值设为True
            }
            else // 如果index不等于1
            {
                configXML.创建配置文件("plot" + index.ToString(), "False", "plotlock.xml"); // 在configXML对象中创建名为"plot"加上index的字符串形式和"False","plotlock.xml"的配置项，并将其值设为False
                _temp.GetComponent<Button>().interactable = false; // 将_temp对象的Button组件的interactable属性设置为false
                obj_lock.SetActive(true); // 将obj_lock对象设置为活动状态
            }

        }
        yield return null; // 等待一段时间后返回null,使协程继续执行下一个步骤
    }
}