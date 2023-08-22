using config;
using System;
using UnityEngine;
using UnityEngine.UI;

public class DetailPlotCanvas : MonoBehaviour
{
    private ConfigXML configXML = new ConfigXML();
    public Text F_Plot_Title;
    public Text F_Plot_Text;
    public Image F_Plot_Texture;
    public GameObject F_PrizeList;

    private GameObject PrizeList;

    public void OnEnable ()
    {
        //Canvas/Horizontal Scroll View/Viewport/Content/choicewhat1/title
        try
        {
            PrizeList = (GameObject)Resources.Load("UI/Plot/PrizeList");
            F_Plot_Title.text = GameObject.Find("Canvas/Horizontal Scroll View/Viewport/Content/choicewhat" + ListPlotButton.Plotindex.ToString() + "/title").GetComponent<Text>().text;
            F_Plot_Text.text = configXML.∂¡≈‰÷√œÓ_StreamingAssets("plot" + ListPlotButton.Plotindex.ToString(), "config/plot/plotdetail.xml").Split('|')[0];
            F_Plot_Texture.sprite = GameObject.Find("Canvas/Horizontal Scroll View/Viewport/Content/choicewhat" + ListPlotButton.Plotindex.ToString() + "/plotphoto").GetComponent<Image>().sprite;
        }
        catch (Exception ex)
        {
            new PopNewMessage(ex.Message);
        }
        //config/plot/plotprize.xml
        string _prize = configXML.∂¡≈‰÷√œÓ_StreamingAssets("plot" + ListPlotButton.Plotindex.ToString(), "config/plot/plotprize.xml");
        string[] _prizeinfo = _prize.Split('|');

        //var _temp = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Plot/PrizeList"), F_PrizeList.transform);
        //_temp.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Icons/LotteryPanel_Diamond_Icon");
    }
}