using System;
using UnityEngine;

public class MenheraStateMachine : MonoBehaviour
{
    private CharacterStateMachine currentState;
    public GameObjectEvent gameObjectEvent;
    public string NowState;

    /// <summary>
    /// 特殊状态，状态机状态等待特殊状态之后再更新
    /// 1:跳舞
    /// </summary>
    public static int SpecialState;

    public GameObject Menhera_Nothing;
    public GameObject Menhera_Sleep;
    public GameObject Menhera;
    public void Start ()
    {
        currentState = GetCurrentState();
        currentState.Enter();
    }
    public void FixedUpdate ()
    {
        if (SpecialState == 0)
        {
            CharacterStateMachine nextState = GetCurrentState();

            if (nextState != currentState)
            {
                currentState.Exit();
                currentState = nextState;
                currentState.Enter();
            }

            currentState.Update();
        }
        else
        {
            switch (SpecialState)
            {
                case 1:
                    //dance
                    CharacterStateMachine nextState = new MiddayState();

                    if (nextState != currentState)
                    {
                        currentState.Exit();
                        currentState = nextState;
                        currentState.Enter();
                    }
                    break;
            }
        }

    }

    CharacterStateMachine GetCurrentState ()
    {

        DateTime currentTime = DateTime.Now;
        int nowHour = currentTime.Hour;

        if (nowHour >= 8 && nowHour < 11)
            return new MorningState();
        else if (nowHour >= 12 && nowHour < 13)
            return new MiddayState();
        else if (nowHour >= 13 && nowHour <= 19)
            return new AfternoonState();
        else if (nowHour >= 23 || (nowHour >= 0 && nowHour < 6))
            return new NightState();
        else
            return new NightToMorningState();
    }

    public void ChangeState (CharacterStateMachine newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
    public string GetCurrentStateName ()
    {
        return currentState.GetType().Name;
    }
    public void SetMenheraState (string _state)
    {
        if (NowState == _state) return; NowState = _state;
        Menhera_Nothing.SetActive(false);

        Menhera_Sleep.SetActive(false);
        Menhera.SetActive(false);
        if (_state == "None") return;
        switch (_state)
        {
            case "Menhera_Nothing":
                Menhera_Nothing.SetActive(true);
                break;
            case "Menhera_Sleep":
                Menhera_Sleep.SetActive(true);
                break;
            case "Menhera_Kitchen":
                Animator animator = Menhera.GetComponent<Animator>();
                AnimatorOverrideController overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
                var animatorController = Resources.Load<RuntimeAnimatorController>("Animate/Kitchen");
                overrideController.runtimeAnimatorController = animatorController;
                animator.runtimeAnimatorController = overrideController;
                Menhera.transform.position = new Vector3(1.45f, 0.06f, 6.14f);
                Menhera.SetActive(true);
                break;
        }

    }

}

public abstract class CharacterStateMachine
{
    public abstract void Enter ();
    public abstract void Update ();
    public abstract void Exit ();
}

public class MorningState : CharacterStateMachine
{
    public override void Enter ()
    {
        GameObject.Find("MenheraSystem").GetComponent<MenheraStateMachine>().SetMenheraState("Menhera_Kitchen");
        Debug.Log("进入上午的状态");
        // 在这里进行上午状态需要的逻辑
    }

    public override void Update ()
    {
        Debug.Log("上午状态更新中");
    }

    public override void Exit ()
    {
        Debug.Log("退出上午的状态");
    }
}

public class MiddayState : CharacterStateMachine
{
    public override void Enter ()
    {
        GameObject.Find("MenheraSystem").GetComponent<MenheraStateMachine>().SetMenheraState("Menhera_Kitchen");
        Debug.Log("进入正中午的状态");
        // 在这里进行正中午状态需要的逻辑
    }

    public override void Update ()
    {
        Debug.Log("正中午状态更新中");
    }

    public override void Exit ()
    {
        Debug.Log("退出正中午的状态");
    }
}

public class AfternoonState : CharacterStateMachine
{
    public override void Enter ()
    {
        Debug.Log("进入下午的状态");
        // 在这里进行下午状态需要的逻辑
    }

    public override void Update ()
    {
        Debug.Log("在家：" + MenheraArrange.Find_bool_home_menherachan());
        if (MenheraArrange.Find_bool_home_menherachan())
        {
            GameObject.Find("MenheraSystem").GetComponent<MenheraStateMachine>().SetMenheraState("Menhera_Nothing");
        }
        else
        {
            GameObject.Find("MenheraSystem").GetComponent<MenheraStateMachine>().SetMenheraState("None");
        }
        Debug.Log("下午状态更新中");
    }

    public override void Exit ()
    {
        Debug.Log("退出下午的状态");
    }
}

public class NightState : CharacterStateMachine
{
    public override void Enter ()
    {
        Debug.Log("进入晚上的状态");
        // 在这里进行晚上状态需要的逻辑
        GameObject.Find("MenheraSystem").GetComponent<MenheraStateMachine>().SetMenheraState("Menhera_Sleep");
    }

    public override void Update ()
    {
        Debug.Log("晚上状态更新中");

    }

    public override void Exit ()
    {
        Debug.Log("退出晚上的状态");
    }
}

public class NightToMorningState : CharacterStateMachine
{
    public override void Enter ()
    {
        GameObject.Find("MenheraSystem").GetComponent<MenheraStateMachine>().SetMenheraState("Menhera_Nothing");
        Debug.Log("进入夜晚到早上的状态");
        // 在这里进行夜晚到早上状态需要的逻辑
    }

    public override void Update ()
    {

        Debug.Log("夜晚到早上状态更新中");
    }

    public override void Exit ()
    {
        Debug.Log("退出夜晚到早上的状态");
    }
}

public static class MenheraController
{
    public static void SetNewState (CharacterStateMachine _NewState)
    {
        GameObject.Find("MenheraSystem").GetComponent<MenheraStateMachine>().ChangeState(_NewState);
    }
}

