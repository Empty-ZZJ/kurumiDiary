using UnityEngine;
using UnityEngine.UI;

public class touchleft : MonoBehaviour
{
    public Transform people_obj;
    public Transform people_camera;
    public static float mouseXSensitivity;
    private float xRotation = 0f;
    public static int RightTouchCount;
    public GameObject text_debug;
    private bool isRight;
    private int index;

    public void Start ()
    {
        mouseXSensitivity = 5f;
    }

    public int GetRightTouchCount ()
    {
        if (Input.touchCount == 0) return 0;
        for (int i = 0; i < Input.touchCount; i++)//因为安卓是多点触控，要循环找到滑动视角的那根手指
        {
            if (Input.touches[i].position.x > UnityEngine.Screen.width / 2)
            {
                return i;
            }
        }
        return 0;
    }

    private void Update ()
    {
        text_debug.GetComponent<Text>().text = "触控点数:" + Input.touchCount + "\r\n" + "滑动屏幕手机索引：" + GetRightTouchCount();
        if (Input.touchCount > 0)
        {
            if (index != -1)
            {
                goto tag_touch;
            }
            if (Input.touches[GetRightTouchCount()].position.x > UnityEngine.Screen.width / 2)
            {
                index = GetRightTouchCount();
                isRight = true;
            }
            else
            {
                index = -1;
                isRight = !isRight;
                return;
            }
        }
        tag_touch:
        if (Input.touchCount > 0 && isRight)
        {
            float mouseY, mouseX;
            {
                mouseX = Input.touches[index].deltaPosition.x * mouseXSensitivity * Time.deltaTime;
                mouseY = Input.touches[index].deltaPosition.y * mouseXSensitivity * Time.deltaTime;
            }
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            people_obj.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            people_camera.Rotate(Vector3.up * mouseX);
        }
    }
}