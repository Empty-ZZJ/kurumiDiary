using System;
using System.Linq;
using UnityEngine;

public class OvenController : MonoBehaviour
{
    public AudioClip open;
    public AudioClip close;
    private bool oven_on = false;
    public GameObject now_obj;
    public int index;
    public int OpenTimes;
    public GameObject Menhera;
    private readonly string[] scentes = { "这样一直开开关关，微波炉会坏的吧？", "还是不要这样了吧？", "这样的话，微波炉会坏的吧？", "据说开关十次以上有惊喜呢", "开关10000次会不会炸啊？" };
    private void Start ()
    {
        Debug.Log(this.gameObject.name);
    }

    public void OnMouse ()
    {
        if (oven_on == false)//open
        {
            oven_on = true;
            GetComponent<Animation>().Play("open");
            AudioSource now_audio = now_obj.GetComponent<AudioSource>();
            now_audio.clip = open;
            now_audio.Play();
            index++;
            if (index == 10000)
            {
                boom();
            }
        }
        else
        {
            oven_on = false;
            GetComponent<Animation>().Play("close");
            AudioSource now_audio = now_obj.GetComponent<AudioSource>();
            now_audio.clip = close;
            now_audio.Play();
            var nowhour = DateTime.Now.Hour;
            if (nowhour >= 23 || (nowhour >= 0 && nowhour < 6))
            {
                OpenTimes++;
                if (OpenTimes > 2)
                {
                    new PopNewMessage(scentes[NewRandom.GetRandomInAB(0, scentes.Count() - 1)]);
                }
                if (OpenTimes >= 10)
                {
                    OpenTimes = 0;
                }
            }
            else
            {
                OpenTimes++;
                if (OpenTimes > 2)
                {
                    new PopNewMessage(scentes[NewRandom.GetRandomInAB(0, scentes.Count() - 1)]);
                }
                if (OpenTimes >= 10 && MenheraArrange.Find_bool_home_menherachan())
                {
                    //执行跳舞
                    new PopNewMessage("欸？胡桃你怎么了！");
                    Menhera.SetActive(true);
                    MenheraController.SetNewState(new StateMachine_Dance());

                    OpenTimes = 0;
                }
            }

        }
    }

    private void boom ()
    {
        throw new NotImplementedException();
    }
}