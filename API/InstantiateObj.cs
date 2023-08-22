using UnityEngine;

public class InstantiateObj : MonoBehaviour
{
    public GameObject _gameObject;
    public Transform ts;

    public void CreatObject ()
    {
        if (transform == null) ts = this.transform;
        Instantiate(_gameObject, ts);
    }

    public void CreatObject_OnParent ()
    {
        if (transform == null) ts = this.transform;
        Instantiate(_gameObject, ts.parent.transform);
    }

    public void CreatObject (Transform transform)
    {
        Instantiate(_gameObject, transform);
    }
}