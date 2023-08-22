using UnityEngine;

public class Draggable3D : MonoBehaviour
{
    public float 允许超出屏幕范围;

    private Vector3 targetPosition; // 物体的目标位置

    private void Update ()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began) // 当手指开始接触屏幕时
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject == gameObject) // 如果手指触碰到了该物体
                    {
                        // 将物体的目标位置设置为当前位置
                        targetPosition = transform.position;
                    }
                }
            }
            else if (touch.phase == TouchPhase.Moved) // 当手指在屏幕上移动时
            {
                if (IsTouchingThisObject(touch.fingerId)) // 如果手指正在与该物体交互
                {
                    // 将屏幕坐标转换为世界坐标
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    Plane plane = new Plane(Vector3.forward, transform.position);
                    float distance;
                    if (plane.Raycast(ray, out distance))
                    {
                        // 将物体的目标位置设置为手指的位置
                        targetPosition = ray.GetPoint(distance);

                        // 计算物体能够移动的最大偏移量
                        Bounds bounds = GetComponent<Collider>().bounds;
                        float maxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, Mathf.Abs(transform.position.z - Camera.main.transform.position.z))).x - bounds.extents.x + 允许超出屏幕范围; // 允许超出屏幕范围
                        float minX = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Mathf.Abs(transform.position.z - Camera.main.transform.position.z))).x + bounds.extents.x - 允许超出屏幕范围; // 允许超出屏幕范围
                        float maxY = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, Mathf.Abs(transform.position.z - Camera.main.transform.position.z))).y - bounds.extents.y + 允许超出屏幕范围; // 允许超出屏幕范围
                        float minY = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Mathf.Abs(transform.position.z - Camera.main.transform.position.z))).y + bounds.extents.y - 允许超出屏幕范围; // 允许超出屏幕范围

                        // 限制物体位置在摄像机可视区域内
                        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
                        targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);

                        // 使用 Lerp 函数平滑移动物体
                        transform.position = targetPosition;
                    }
                }
            }
        }
    }

    private bool IsTouchingThisObject (int fingerId)
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            if (touch.fingerId == fingerId && Physics.Raycast(Camera.main.ScreenPointToRay(touch.position), out RaycastHit hit) && hit.collider.gameObject == gameObject)
            {
                return true;
            }
        }
        return false;
    }
}