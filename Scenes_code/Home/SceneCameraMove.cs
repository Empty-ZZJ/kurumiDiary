using DG.Tweening;
using UnityEngine;

public class SceneCameraMove : MonoBehaviour
{
    public Vector2 firstPos = Vector2.zero;
    public Vector2 secondPos = Vector2.zero;
    public Camera main_game;
    public static bool bool_move = true;
    public float pos;
    public float speed;
    public static Tweener move_event;

    private void Start ()
    {
        pos = main_game.transform.position.x;
    }

    private void FixedUpdate ()
    {
        float.TryParse(GameConfig.GetValue("sensitivity", "setting.xml"), out speed);
        if (speed == 0f) speed = 0.00339f;
    }

    /// <summary>
    /// 返回为真的时候可以触发点击和移动事件。
    /// 返回为假则不可以。
    /// 这个方法用于判断当前是否有其他交互
    /// </summary>
    /// <returns></returns>
    public static bool IsMove_Click ()
    {
        return bool_move;
    }

    public static void SetMoveFalse ()
    {
        bool_move = false;
    }

    public static void SetMoveTrue ()
    {
        bool_move = true;
    }

    private void OnGUI ()
    {
        if (bool_move == true)
        {
            if (Event.current.type == EventType.MouseDown)
            {
                //Debug.Log(555);
                firstPos = Event.current.mousePosition;
            }
            if (Event.current.type == EventType.MouseDrag)
            {
                secondPos = Event.current.mousePosition;
                //Debug.Log(secondPos.x - firstPos.x);
                pos = main_game.transform.position.x;
                if (secondPos.x - firstPos.x > 1)//该往左
                {
                    if (main_game.transform.position.x >= 0.593f && main_game.transform.position.x <= 5.764f)
                    {
                        pos += ((firstPos.x - secondPos.x) * speed);
                        if (pos >= 0.593f && pos <= 5.764f)
                        {
                            move_event = main_game.transform.DOMove(new Vector3(pos, 1.17f, 1.306f), 0.18f);
                            //main_game.transform.position = new Vector3(pos, 1.17f, 1.753f);
                        }
                        else
                        {
                            pos -= ((secondPos.x - firstPos.x) * speed);
                        }
                    }
                }
                if (secondPos.x - firstPos.x < 1)//该往右
                {
                    if (main_game.transform.position.x >= 0.593f && main_game.transform.position.x <= 5.764f)
                    {
                        pos -= ((secondPos.x - firstPos.x) * speed);
                        if (pos >= 0.593f && pos <= 5.764f)
                        {
                            move_event = main_game.transform.DOMove(new Vector3(pos, 1.17f, 1.306f), 0.18f);
                            // main_game.transform.position= new Vector3(pos, 1.17f, 1.753f)
                        }
                        else
                        {
                            pos += ((firstPos.x - secondPos.x) * speed);
                        }
                    }
                }
                firstPos = secondPos;
            }
            // Debug.Log(pos);
        }
    }
}