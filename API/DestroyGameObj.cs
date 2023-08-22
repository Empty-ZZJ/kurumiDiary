using UnityEngine;

public class DestroyGameObj : MonoBehaviour
{
    public void DesThisGameObject ()
    {
        Destroy(this);
    }

    public void DesGameObject (GameObject _gameObject)
    {
        Destroy(_gameObject);
    }

    public static void RemoveAllChildren (GameObject parent)
    {
        Transform transform;
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            transform = parent.transform.GetChild(i);
            GameObject.Destroy(transform.gameObject);
        }
    }
}