using UnityEngine;

public class ImageEvent_Shape : MonoBehaviour
{
    public float rotationSpeed = 100f;

    private void Update ()
    {
        // 每帧旋转一定角度
        transform.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);
    }
}