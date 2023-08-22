using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ipadclose : MonoBehaviour
{
    public GameObject mainca;
    public Material close;
    public GameObject truepbj;
    public GameObject allobj;
    public AudioSource audio_ipad;
    public AudioClip clip_close;

    public void onipadclose ()
    {
        Debug.Log("you close ipad");
        Temp_skybox.Cancel_Temp_skybox();
        audio_ipad.clip = clip_close;
        audio_ipad.Play();
        allobj.transform.DOMove(ipad.posutea, 1);
        allobj.transform.DORotate(new Vector3(0, -90, -127), 1).OnComplete(Over);
        truepbj.GetComponent<MeshRenderer>().material = close;
        SceneCameraMove.bool_move = true;
    }

    private void Over ()
    {
        allobj.GetComponent<Button>().interactable = true;
    }
}