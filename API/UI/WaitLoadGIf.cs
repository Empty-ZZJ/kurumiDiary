using UnityEngine;

public class WaitLoadGIf : MonoBehaviour
{
    public const string path = "Prefabs/UI/WaitLoading";
    private GameObject _gameObject;

    public WaitLoadGIf ()
    {
        _gameObject = Instantiate(Resources.Load<GameObject>(path));
    }

    public void Exit ()
    {
        Destroy(_gameObject);
    }
}