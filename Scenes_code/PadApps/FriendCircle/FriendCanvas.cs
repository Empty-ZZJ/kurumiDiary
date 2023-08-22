using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class FriendCanvas : MonoBehaviour
{
    public Transform _FriendCanvas_Item_To_tranform;
    private const string apiUrl = "https://api.nanasekurumi.top/Menhera/GetFriendCircle";
    private List<FriendCanvas_InfoItem> FriendCanvas_List = new List<FriendCanvas_InfoItem>();
    private GameObject FriendCanvas_Item;
    public ScrollRect _ScrollRect;

    private struct DataIndex
    {
        public int From;
        public int End;
    }

    private DataIndex _DataIndex = new DataIndex();

    public void Start ()
    {
        _DataIndex.From = 1;
        _DataIndex.End = 10;
        FriendCanvas_Item = Resources.Load<GameObject>("Prefabs/UI/ScenesUI/Pad/FriendCanvas_Item");
        StartCoroutine(GetData());
    }

    public void FixedUpdate ()
    {
        if (_ScrollRect.verticalNormalizedPosition < 0)
        {
            Debug.Log("滑倒了尽头");
            _DataIndex.From += 10;
            _DataIndex.End += 10;
            StartCoroutine(GetData());
        }
    }

    private IEnumerator GetData ()
    {
        string url = apiUrl + "?from=" + _DataIndex.From + "&end=" + _DataIndex.End;

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                string responseText = webRequest.downloadHandler.text;
                FriendCanvas_InfoList infoData = JsonUtility.FromJson<FriendCanvas_InfoList>(responseText);

                FriendCanvas_List = infoData.infoList;

                foreach (FriendCanvas_InfoItem item in FriendCanvas_List)
                {
                    var _Item = Instantiate(FriendCanvas_Item, _FriendCanvas_Item_To_tranform);
                    _Item.name = $"FriendCanvas_Item{item.id}";
                    _Item.GetComponent<FriendCanvas_Item>().Init(item.title);
                }
            }
        }
    }
    public void Button_Click_News ()
    {
        new PopNewMessage("暂未开放！");
    }

}

[System.Serializable]
public class FriendCanvas_InfoItem
{
    public int id;
    public string title;
    public string imgUrl;
}

[System.Serializable]
public class FriendCanvas_InfoList
{
    public List<FriendCanvas_InfoItem> infoList;
}