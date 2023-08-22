using System;
using TMPro;
using UnityEngine;

/// <summary>
/// 显示圆圈加载页面，本类在实例化的那一刻起就生效
/// </summary>
public class ShowLoading
{
    private GameObject _GameObjectLoading;

    /// <summary>
    /// 实例化的那一刻起就生效
    /// </summary>
    /// <param name="_Title">提示文本</param>
    public ShowLoading (string _Title = "")
    {
        Debug.Log("开始加载");
        _GameObjectLoading = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/UI/WaitLoading"));

        if (_Title != string.Empty)
        {
            foreach (var t in _GameObjectLoading.GetComponentsInChildren<Transform>())
            {
                if (t.name == "title")
                {
                    var _textMeshProUGUI = t.GetComponent<TextMeshProUGUI>();
                    _textMeshProUGUI.text = _Title;
                }
            }
        }
    }

    public void SetTitle (string _Title)
    {
        foreach (var t in _GameObjectLoading.GetComponentsInChildren<Transform>())
        {
            if (t.name == "title")
            {
                var _textMeshProUGUI = t.GetComponent<TextMeshProUGUI>();
                _textMeshProUGUI.text = _Title;
            }
        }
    }

    /// <summary>
    /// 关闭当前加载
    /// </summary>
    /// <returns></returns>
    public bool KillLoading ()
    {
        try
        {
            Debug.Log("销毁加载");
            MonoBehaviour.Destroy(_GameObjectLoading.gameObject);
            return true;
        }
        catch (Exception ex)
        {
            new PopNewMessage(ex.Message);
            return false;
        }
    }

    /// <summary>
    /// 重新显示加载页面
    /// </summary>
    public bool ReShowLoading ()
    {
        if (_GameObjectLoading == null)
        {
            _GameObjectLoading = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/UI/WaitLoading"));
            return true;
        }
        else
        {
            return false;
        }
    }
}