using UnityEngine;

public class PopNewMessage
{
    /// <summary>
    /// 构造调用，从屏幕上方弹出一个消息
    /// </summary>
    /// <param name="_message"></param>
    public PopNewMessage (string _message)
    {
        Debug.Log(_message);
        CoreMessage._message = _message;
        GameObject _new = Resources.Load<GameObject>("Prefabs/UI/NewMessage");
        Object.Instantiate(_new);
    }
}