using UnityEngine;

public class GameObjectEvent : MonoBehaviour
{
    public GameObject FindInactiveObjectByName (string name)
    {
        Transform[] transforms = Resources.FindObjectsOfTypeAll<Transform>();

        foreach (Transform transform in transforms)
        {
            if (transform.name == name)
            {
                return transform.gameObject;
            }
        }

        return null;
    }
}