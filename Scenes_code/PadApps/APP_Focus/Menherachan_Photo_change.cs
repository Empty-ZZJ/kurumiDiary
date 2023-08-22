using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menherachan_Photo_change : MonoBehaviour
{
    public List<Sprite> Sprite_writting = new List<Sprite>();
    public List<Sprite> Sprite_Awake = new List<Sprite>();
    public List<Sprite> Sprite_ResaultSptite = new List<Sprite>();
    public List<Sprite> Sprite_Fall = new List<Sprite>();
    public Image MenheraPhoto;
    private int bool_state = 0;

    public void Start ()
    {
        MenheraPhoto.sprite = Sprite_Awake[NewRandom.GetRandomInAB(1, 3)];

    }

    private void FixedUpdate ()
    {
        //core_time.State
        if (bool_state != TimeFocus_Core.State)
        {
            switch (TimeFocus_Core.State)
            {
                case 0:
                    break;

                case 1:
                    bool_state = TimeFocus_Core.State;
                    StartCoroutine(ChangeStateCoroutine());
                    break;

                case 2:
                    bool_state = TimeFocus_Core.State;
                    MenheraPhoto.sprite = Sprite_ResaultSptite[NewRandom.GetRandomInAB(1, 3)];
                    break;

                case 3:
                    bool_state = TimeFocus_Core.State;
                    MenheraPhoto.sprite = Sprite_Fall[NewRandom.GetRandomInAB(1, 4)];
                    break;

                default:
                    Debug.LogError("·Ç·¨µÄ×´Ì¬");
                    break;
            }
        }
    }

    public IEnumerator ChangeStateCoroutine ()
    {
        while (bool_state == TimeFocus_Core.State && bool_state == 1)
        {
            MenheraPhoto.sprite = Sprite_writting[NewRandom.GetRandomInAB(1, 5)];
            yield return new WaitForSeconds(0.2f);
        }
    }
}