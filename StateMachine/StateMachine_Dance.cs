using System.Collections;
using UnityEngine;

public class StateMachine_Dance : CharacterStateMachine
{
    public override void Enter ()
    {
        MenheraStateMachine.SpecialState = 1;
        Debug.Log("开始跳舞");
        try
        {

            if (NewRandom.GetRandomInAB(1, 2) == 1)
            {
                //SetNewState
                var _Game = GameObject.Find("MenheraSystem");

                _Game.GetComponent<MenheraStateMachine>().StartCoroutine(CancelMenheraState(56.346f));
                string animationPath = "Animate/Dance_等着你回来";
                var GameObj = GameObject.Find("MenheraSystem/Menhera_Kitchen");

                Animator animator = GameObj.GetComponent<Animator>();
                AnimatorOverrideController overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
                var animatorController = Resources.Load<RuntimeAnimatorController>(animationPath);
                overrideController.runtimeAnimatorController = animatorController;
                animator.runtimeAnimatorController = overrideController;
                var audio = GameObject.Find("MenheraSystem/Menhera_Kitchen/Body").GetComponent<AudioSource>();
                audio.clip = Resources.Load<AudioClip>("Audio/Dance/等着你回来");
                audio.Play();
                GameObj.transform.position = new Vector3(3.92f, 0.06f, 7.87f);
            }
            else
            {
                var _Game = GameObject.Find("MenheraSystem");

                _Game.GetComponent<MenheraStateMachine>().StartCoroutine(CancelMenheraState(89.592f));
                string animationPath = "Animate/Dance_105";
                var GameObj = GameObject.Find("MenheraSystem/Menhera_Kitchen");

                Animator animator = GameObj.GetComponent<Animator>();
                AnimatorOverrideController overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
                var animatorController = Resources.Load<RuntimeAnimatorController>(animationPath);
                overrideController.runtimeAnimatorController = animatorController;
                animator.runtimeAnimatorController = overrideController;
                var audio = GameObject.Find("MenheraSystem/Menhera_Kitchen/Body").GetComponent<AudioSource>();
                audio.clip = Resources.Load<AudioClip>("Audio/Dance/105");
                audio.Play();
                GameObj.transform.position = new Vector3(3.92f, 0.06f, 5.46f);
            }
        }
        catch
        {

        }
        GameObject.Find("Main Camera").transform.position = new Vector3(4, 1.16999996f, 1.30599999f);
    }

    public override void Update ()
    {
        Debug.Log("跳舞状态更新中");
    }

    public override void Exit ()
    {
        Debug.Log("跳舞的状态结束");
    }
    public IEnumerator CancelMenheraState (float _time)
    {
        yield return new WaitForSeconds(_time);
        MenheraStateMachine.SpecialState = 0;
    }


}
