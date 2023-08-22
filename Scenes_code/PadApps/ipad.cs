using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ipad : MonoBehaviour
{
    public Transform CameraTranform;
    private Tweener twe;

    // Start is called before the first frame update
    public GameObject truepbj;

    public GameObject allobj;
    public static Vector3 posutea;
    public AudioSource audio_ipad;
    public AudioClip clip_open;

    private void Awake ()
    {
        posutea = allobj.transform.position;
    }

    public void mouseon (Action callback = null)
    {
        if (GetComponent<Button>().interactable == true && SceneCameraMove.IsMove_Click())
        {
            GetComponent<Button>().interactable = false;
            audio_ipad.clip = clip_open;
            audio_ipad.Play();
            SceneCameraMove.bool_move = false;
            Vector3 ipadmove = CameraTranform.position;
            ipadmove.z += 0.279f;
            this.transform.DOMove(ipadmove, 0.4f);
            twe = this.transform.DORotate(new Vector3(0, -90, -90), 0.4f);
            twe.OnComplete(() =>
            {
                Instantiate(Resources.Load<GameObject>("Prefabs/UI/ScenesUI/Pad/IpadCanvas"));
                if (callback != null)
                {
                    callback();
                }
            });
        }
        else
        {
            return;
        }
    }

    public void mouseon ()
    {
        if (GetComponent<Button>().interactable == true && SceneCameraMove.IsMove_Click())
        {
            GetComponent<Button>().interactable = false;
            audio_ipad.clip = clip_open;
            audio_ipad.Play();
            SceneCameraMove.bool_move = false;
            Vector3 ipadmove = CameraTranform.position;
            ipadmove.z += 0.279f;
            this.transform.DOMove(ipadmove, 0.4f);
            twe = this.transform.DORotate(new Vector3(0, -90, -90), 0.4f);
            twe.OnComplete(() =>
            {
                Instantiate(Resources.Load<GameObject>("Prefabs/UI/ScenesUI/Pad/IpadCanvas"));
            });
        }
        else
        {
            return;
        }
    }

    public void OpenApp (int _index)
    {
        switch (_index)
        {
            case 0:
                mouseon(CreatApp);
                void CreatApp ()
                {
                    var _tran = GameObject.FindGameObjectsWithTag("IpadCanvas_Page")[0].transform;
                    Instantiate(Resources.Load<GameObject>("Prefabs/UI/ScenesUI/Pad/FriendCanvas"), _tran);
                }
                break;
        }
    }
}