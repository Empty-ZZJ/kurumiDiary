using System;
using TMPro;
using UnityEngine;

public class CorePopup : MonoBehaviour
{
    public TextMeshProUGUI _Title;
    public TextMeshProUGUI _Description;
    private Action _Action;
    public void Init (string _title, string _description, Action _callback = null)
    {
        _Title.text = _title;
        _Description.text = _description;
        _Action = _callback;
    }
    public void Button_Click_Call ()
    {
        if (_Action != null)
        {
            _Action.Invoke();
        }

    }
}