

using System;
using UnityEngine;

public class PopNewPopup
{
    public PopNewPopup (string _title, string _description, Action _callback = null)
    {
        var _ = UnityEngine.Resources.Load<GameObject>("Prefabs/UI/PopNewPopup").GetComponent<CorePopup>();
        _.Init(_title, _description, _callback);
        UnityEngine.Object.Instantiate(_);
    }
}
