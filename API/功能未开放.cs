using UnityEngine;

public class 功能未开放 : MonoBehaviour
{
    public GameObject message_error;

    public void 打开消息_功能未开放 ()
    {
        Temp_skybox.Temp_Core_touch_on();
        SceneCameraMove.bool_move = false;
        UIAnimate.ToMiddle(message_error);
    }

    public void message_ok ()
    {
        SceneCameraMove.SetMoveTrue();
        UIAnimate.ToButtom(message_error);
        Temp_skybox.Cancel_Temp_skybox();
    }
}