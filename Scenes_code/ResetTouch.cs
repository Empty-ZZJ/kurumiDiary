using UnityEngine;

/// <summary>
/// 将这个类放到场景任何一个激活的物体上就可以
/// </summary>
public class ResetTouch : MonoBehaviour
{
    public void Awake ()
    {
        SceneCameraMove.SetMoveTrue();
    }
}