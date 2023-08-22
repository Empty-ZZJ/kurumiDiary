using UnityEngine;

public class CameraLimit : MonoBehaviour
{
    public Vector3 vect;
    private float ycream;

    private void Update ()
    {
        //LimitAngleUandD(60);
    }

    private void LimitAngleUandD (float angle)
    {
        vect = this.transform.eulerAngles;
        //当前相机y轴旋转的角度(0~360)
        ycream = IsPosNum(vect.y);
        if (ycream > angle)
            this.transform.rotation = Quaternion.Euler(vect.x, angle, 0);
        else if (ycream < -angle)
            this.transform.rotation = Quaternion.Euler(vect.x, -angle, 0);
    }

    private float IsPosNum (float x)
    {
        x -= 180;
        if (x < 0)
            return x + 180;
        else return x - 180;
    }
}