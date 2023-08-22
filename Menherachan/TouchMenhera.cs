using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TouchMenhera : MonoBehaviour
{
    /// <summary>
    /// Ö÷ÉãÏñ»ú
    /// </summary>
    public Camera MainCamera;
    private Vector3 FromTransForm;

    public void ClickMenhera (Transform _CharacterTransForm)
    {
        MainCamera.transform.DOMove(_CharacterTransForm.position, 1f);
    }
    public void Button_Click_Living ()
    {
        var button = GameObject.Find("MenheraSystem/Menhera_Nothing/livingroom_hutao").GetComponent<Button>();
        if (button.interactable)
        {
            FromTransForm = MainCamera.transform.position;
            SceneCameraMove.SetMoveFalse();
            Debug.Log(FromTransForm);
            MainCamera.transform.DOMove(new Vector3(5.43499994f, 0.700999975f, 5.09000015f), 1f).OnComplete(() =>
            {

                Instantiate(Resources.Load<GameObject>("Prefabs/MenherachanCanvas"));
            });
        }

    }
    public Tweener Button_Click_Living_out ()
    {
        return MainCamera.transform.DOMove(FromTransForm, 1f).OnComplete(() =>
        {
            SceneCameraMove.SetMoveTrue();
            var button = GameObject.Find("MenheraSystem/Menhera_Nothing/livingroom_hutao").GetComponent<Button>();
            button.interactable = true;
            this.DOKill();
        });
    }

}
