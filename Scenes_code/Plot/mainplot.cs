using config;
using DG.Tweening;
using System.Collections;
using System.Threading;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainplot : MonoBehaviour
{
    public GameObject background;
    public GameObject txtdialog;
    public GameObject character;
    public GameObject dialog;
    public GameObject dialog_menherachan;
    public GameObject dialog_me;
    public GameObject obj_choice1;
    public GameObject obj_choice2;
    public GameObject whitemask;
    private AsyncOperation operation;
    //-----------------------------------分隔符--------------------------------------------

    public string[] nowtext = new string[500];
    public string[] nowcharacter = new string[500];
    public string[] nowcharacterbackground = new string[500];
    public string[] nowbackground = new string[500];
    public string[] choice1 = new string[500];
    public string[] choice2 = new string[500];
    public bool[] bool_seechoice = new bool[500];
    public int[] int_seechoice = new int[500];
    public bool[] bool_white = new bool[500];

    public static bool move;
    public int plotindex;
    public int nowindex = 0;//当前剧情索引
    private Tweener twe;
    private Tweener dialoh_white;
    private Tweener event_txt;

    //-----------------------------------分隔符--------------------------------------------
    private int fla = 0;

    private ConfigXML configXML = new ConfigXML();

    private void Awake ()
    {
        /*
        int.TryParse(configXML.读配置项("nowplot", "plotset.xml"), out plotindex);
        Debug.Log(plotindex);
        if (File.Exists(Application.persistentDataPath + "/plotprocessing.xml") == true)
        {
            if (configXML.读配置项("plotall" + plotindex.ToString(), "plotprocessing.xml") == "None")
            {
                plotbutton.AddXML("plotall" + plotindex.ToString(), "0", "plotall" + plotindex.ToString(), "plotprocessing.xml");
            }
            int.TryParse(plotbutton.ReadXML("plotall" + plotindex.ToString(), "plotall" + plotindex.ToString(), "plotprocessing.xml"), out nowindex);
        }
        else
        {
            plotbutton.CreateXML("plotall" + plotindex.ToString(), "plotall" + plotindex.ToString(), plotindex.ToString(), "plotprocessing.xml");
        }

        loadTemplate("plottxt/mainplot/plotall" + plotindex.ToString());
        */
    }

    private void Start ()
    {
        /*
        changetxt(true, nowtext[nowindex], new Color(142 / 255f, 114 / 255f, 106 / 255f, 255 / 255f));
        changebackground(nowbackground[nowindex]);
        changecharacter(nowcharacterbackground[nowindex]);
        nowindex++;
        */
    }

    public void choiceon ()
    {
        move = true;
        seechoice(false, 0, "", "");
        changetxt(true, nowtext[nowindex], new Color(142 / 255f, 114 / 255f, 106 / 255f, 255 / 255f));
        changebackground(nowbackground[nowindex]);
        changecharacter(nowcharacterbackground[nowindex]);
        nowindex++;
    }

    public void touchplot ()
    {
        if (move == true)
        {
            Debug.Log(fla + " " + nowindex);
            move = false;
            whitemask.SetActive(false);
            if (bool_white[nowindex - 1] == true)
            {
                move = false;
                whitemask.SetActive(true);
                dialoh_white = whitemask.GetComponent<Image>().DOColor(new Color32(255, 255, 255, 255), 2);
                dialoh_white.OnComplete(dialogwhie);
                return;
            }
            if (nowindex >= fla)
            {
                if (configXML.读配置项("plot" + plotindex.ToString(), "plotdone.xml") == "false" || configXML.读配置项("plot" + plotindex.ToString(), "plotdone.xml") == "None")
                {
                    configXML.更新配置项("plot" + plotindex.ToString(), "true", "plotdone.xml");
                    //lucksystem.add_coin(300);
                    string[] tempprize = new string[5];
                    string temp2;
                    temp2 = configXML.读配置项_StreamingAssets("plot" + plotindex.ToString(), "config/plot/plotprize.xml");
                    tempprize = temp2.Split("|");
                    {
                        Debug.Log(tempprize[0] + " " + tempprize[1]);
                        int zuanshi; float haogandu;
                        int.TryParse(tempprize[0], out zuanshi);
                        float.TryParse(tempprize[1], out haogandu);
                        //lucksystem.add_coin(zuanshi);
                        //lovedegree.add_degree(haogandu);
                    }
                }
                Debug.Log("对话到这里就结束了");
                int temp1 = plotindex + 1;
                Debug.Log("plot" + temp1.ToString());
                //  plotlock.updatelock("plot" + temp1.ToString(), true);
                configXML.更新配置项("plotall" + plotindex.ToString(), "0", "plotprocessing.xml");
                whitemask.SetActive(true);
                twe = whitemask.GetComponent<Image>().DOColor(new Color32(255, 255, 255, 255), 2);
                twe.OnComplete(startmap);
                return;//正常了
            }
            if (nowcharacter[nowindex] == "")
            {
                Debug.Log("P");
                changetxt(true, nowtext[nowindex], Color.blue);
                changebackground(nowbackground[nowindex]);
                changecharacter(nowcharacterbackground[nowindex]);
                nowindex++;
                configXML.更新配置项("plotall" + plotindex.ToString(), nowindex.ToString(), "plotprocessing.xml");
                return;
            }
            else if (nowcharacter[nowindex] == "七濑胡桃:" || nowcharacter[nowindex] == "我:")
            {
                changetxt(true, nowtext[nowindex], new Color(142 / 255f, 114 / 255f, 106 / 255f, 255 / 255f));
                changebackground(nowbackground[nowindex]);
                changecharacter(nowcharacterbackground[nowindex]);
                nowindex++;
                configXML.更新配置项("plotall" + plotindex.ToString(), nowindex.ToString(), "plotprocessing.xml");
                return;
            }
        }
    }

    public void changebackground (string file)//场景
    {
        var photo = Resources.Load<Sprite>(file);
        background.gameObject.GetComponent<Image>().sprite = photo;
    }

    public void changecharacter (string file)//人物立绘
    {
        var photo = Resources.Load<Sprite>(file);
        character.gameObject.GetComponent<Image>().sprite = photo;
    }

    public void changetxt (bool Rightsee, string txt, Color txtcolor)
    {
        move = false;
        Debug.Log(event_txt.IsActive());
        if (Rightsee == true)
        {
            if (nowcharacter[nowindex] == "七濑胡桃:")
            {
                seedailog(-1);
            }
            else if (nowcharacter[nowindex] == "我:")
            {
                seedailog(1);
            }
            else if (nowcharacter[nowindex] == "")
            {
                seedailog(0);
            }
            txtdialog.SetActive(true);
            dotxt(txt);
            txtdialog.GetComponentInChildren<Text>().color = txtcolor;
        }
        else
        {
            seedailog(0);
            txtdialog.SetActive(false);
            dotxt(txt);
            txtdialog.GetComponentInChildren<Text>().color = txtcolor;
        }
    }

    private void dotxt (string txt)
    {
        move = false;
        txtdialog.GetComponentInChildren<Text>().text = "";
        event_txt = txtdialog.GetComponentInChildren<Text>().DOText(txt, txt.Length * 0.07f).SetLoops(1);
        event_txt.OnComplete(txtover);
    }

    private void txtover ()
    {
        if (int_seechoice[nowindex - 1] != 0)
        {
            move = false;
            if (int_seechoice[nowindex - 1] == 1)
            {
                Debug.Log("1个选项");
                seechoice(bool_seechoice[nowindex - 1], 1, choice1[nowindex - 1], "");
            }
            if (int_seechoice[nowindex - 1] == 2)
            {
                Debug.Log("2个选项");
                seechoice(bool_seechoice[nowindex - 1], 2, choice1[nowindex - 1], choice2[nowindex - 1]);
            }
        }
        else
        {
            move = true;
        }
    }

    public void seedailog (int a)
    {
        if (a == -1)//menherachan
        {
            dialog_me.SetActive(false);
            dialog_menherachan.SetActive(true);
        }
        else if (a == 0)//
        {
            dialog_me.SetActive(false);
            dialog_menherachan.SetActive(false);
        }
        else if (a == 1)//
        {
            dialog_me.SetActive(true);
            dialog_menherachan.SetActive(false);
        }
        return;
    }

    public void seechoice (bool see, int math, string x, string y)
    {
        if (see == true)
        {
            move = false;

            if (math == 1)
            {
                int replt_length_x = x.Length;
                if (replt_length_x > 14)
                {
                    Vector2 temp = obj_choice1.GetComponent<RectTransform>().sizeDelta;
                    temp.x += (replt_length_x / 2) * 7;
                    obj_choice1.GetComponent<RectTransform>().sizeDelta = temp;
                    obj_choice1.SetActive(true);
                    obj_choice1.GetComponentInChildren<Text>().text = x;
                }
                else
                {
                    obj_choice1.GetComponent<RectTransform>().sizeDelta = new Vector2(133.0383f, 48.5244f);
                    obj_choice1.SetActive(true);
                    obj_choice1.GetComponentInChildren<Text>().text = x;
                }
            }
            else if (math == 2)
            {
                int replt_length_x = x.Length;
                int replt_length_y = y.Length;
                if (replt_length_x > 14)
                {
                    Vector2 temp_x = obj_choice1.GetComponent<RectTransform>().sizeDelta;
                    temp_x.x += (replt_length_x / 2) * 7;
                    obj_choice1.GetComponent<RectTransform>().sizeDelta = temp_x;
                }
                else obj_choice1.GetComponent<RectTransform>().sizeDelta = new Vector2(133.0383f, 48.5244f);
                if (replt_length_y > 14)
                {
                    Vector2 temp_y = obj_choice1.GetComponent<RectTransform>().sizeDelta;
                    temp_y.x += (replt_length_y / 2) * 7;
                    obj_choice2.GetComponent<RectTransform>().sizeDelta = temp_y;
                }
                else obj_choice2.GetComponent<RectTransform>().sizeDelta = new Vector2(133.0383f, 48.5244f);
                obj_choice1.SetActive(true);
                obj_choice2.SetActive(true);
                obj_choice1.GetComponentInChildren<Text>().text = x;
                obj_choice2.GetComponentInChildren<Text>().text = y;
            }
        }
        else
        {
            obj_choice1.SetActive(false);
            obj_choice2.SetActive(false);
        }
    }

    private IEnumerator maskevent ()
    {
        Color a = whitemask.GetComponent<Image>().color;
        // whitemask.GetComponent<Image>().color.Lerp((a, Color.clear, 1.5f));
        yield return new WaitForSeconds(1f);
    }

    public void onmaskwhite ()
    {
        whitemask.SetActive(true);
        StartCoroutine(maskevent());
    }

    public void loadTemplate (string file)//加载模板
    {
        string[] temp;
        string[] plottext = new string[100];
        string[] tempchoice;
        TextAsset textAsset = (TextAsset)Resources.Load(file, typeof(TextAsset));
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        XmlNodeList node = xmlDoc.SelectSingleNode("root").ChildNodes;
        foreach (XmlElement x1 in node)
        {
            foreach (XmlElement data1 in x1.ChildNodes)
            {
                plottext[fla] = data1.InnerText;
                temp = plottext[fla].Split('|');
                nowtext[fla] = temp[1];
                if (temp[0] == "Q")
                {
                    nowcharacter[fla] = "七濑胡桃:";
                }
                else if (temp[0] == "M")
                {
                    nowcharacter[fla] = "我:";
                }
                else if (temp[0] == "P")
                {
                    nowcharacter[fla] = "";
                }
                nowbackground[fla] = temp[2];
                nowcharacterbackground[fla] = temp[3];
                tempchoice = temp[4].Split('-');
                Debug.Log(tempchoice.Length);
                if (tempchoice.Length == 1)
                {
                    if (tempchoice[0] == "3")
                    {
                        bool_white[fla] = true;
                    }
                    int_seechoice[fla] = 0;
                    bool_seechoice[fla] = false;
                }
                else if (tempchoice.Length == 2)//因为有0，所以会分割出来0个数据成员数
                {
                    int_seechoice[fla] = 1;
                    bool_seechoice[fla] = true;
                    choice1[fla] = tempchoice[1];
                }
                else if (tempchoice.Length == 3)
                {
                    int_seechoice[fla] = 2;
                    choice1[fla] = tempchoice[1];
                    choice2[fla] = tempchoice[2];
                    bool_seechoice[fla] = true;
                }

                fla++;
            }
        }
    }

    private void dialogwhie ()
    {
        whitemask.GetComponent<Image>().DOColor(new Color32(255, 255, 255, 0), 2);
        whitemask.SetActive(false);
        changetxt(true, nowtext[nowindex], new Color(142 / 255f, 114 / 255f, 106 / 255f, 255 / 255f));
        changebackground(nowbackground[nowindex]);
        changecharacter(nowcharacterbackground[nowindex]);
        nowindex++;
    }

    private void startmap ()
    {
        Thread.Sleep(10);
        StartCoroutine(start_map());
    }

    private IEnumerator start_map ()
    {
        operation = SceneManager.LoadSceneAsync(2);
        yield return operation;
    }
}