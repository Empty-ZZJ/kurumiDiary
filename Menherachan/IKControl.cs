using UnityEngine;

public class IKControl : MonoBehaviour
{
    public Animator anim;      //动画
    public Transform head;     //头部
    public Transform lefthand;  //左手
    public Transform righthand; //右手
    public Transform leftfoot;  //左脚
    public Transform rightfoot; //右脚



    private void OnAnimatorIK (int layerIndex)
    {
        anim.SetLookAtWeight(1);
        anim.SetLookAtPosition(head.position); //头部看向
        /*
        anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        anim.SetIKPosition(AvatarIKGoal.LeftHand, lefthand.position); //左手位置
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        anim.SetIKPosition(AvatarIKGoal.RightHand, righthand.position); //右手位置
        anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
        anim.SetIKPosition(AvatarIKGoal.LeftFoot, leftfoot.position); //左脚位置
        anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
        anim.SetIKPosition(AvatarIKGoal.RightFoot, rightfoot.position); //右脚位置
        */

    }
}
