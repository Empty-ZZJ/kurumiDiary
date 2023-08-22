using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiaryProhibitCanvas : MonoBehaviour
{
    private static int Number = 0;
    public Text _Title;
    public Image _Image;
    public List<Sprite> _AngryPictures;

    private void Start ()
    {
        Number++;
        _Title.text = Get_不让看日记_Dialog();
        _Image.sprite = _AngryPictures[NewRandom.GetRandomInAB(1, 4)];
        StartCoroutine(DestroyAfterDelay(3f));
        if (Number >= 5)
        {
            //执行收回笔记的方法
            Number = 0;
            GameObject.Find("kitchen/Furniture_Kitchencabinet_Forest/PhotoAlbum_new").GetComponent<Diary_button>().button_out();
            Destroy(GameObject.FindWithTag("DiaryCanvas").gameObject);
        }
    }

    private IEnumerator DestroyAfterDelay (float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
    }

    /// <summary>
    /// 返回不让看日记的时候胡桃说的一句话。
    /// </summary>
    public string Get_不让看日记_Dialog ()
    {
        int temp = NewRandom.GetRandomInAB(1, 6);
        switch (temp)
        {
            case 1:
                return "不许偷看日记，被我发现的话...哼哼";

            case 2:
                return "这可是胡桃的隐私，不能看";

            case 3:
                return "不许不许不许，你怎么对女孩子的隐私这么感兴趣阿！";

            case 4:
                return "不许偷看女孩子的日记";

            case 5:
                return "胡桃才没有写日记，你什么也没看到";

            case 6:
                return "不许偷看日记，摸摸封面也不可以。";
        }
        return "胡桃才没有写日记，你什么也没看到";
    }
}