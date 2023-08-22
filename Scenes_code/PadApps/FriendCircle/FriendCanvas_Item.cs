using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FriendCanvas_Item : MonoBehaviour
{
    public TextMeshProUGUI _Title;
    public void Init(string _title)
    {
        _Title.text = _title;
    }
}
