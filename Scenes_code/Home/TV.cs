using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TV : MonoBehaviour
{
    public GameObject MainCamera;

    private Vector3 P_From;

    public void Button_Click_In ()
    {
        if (this.GetComponent<Button>().interactable == true && SceneCameraMove.IsMove_Click())
        {
            SceneCameraMove.SetMoveFalse();
            P_From = MainCamera.transform.position;
            this.GetComponent<Button>().interactable = false;
            MainCamera.transform.DOMove(new Vector3(5.43900013f, 0.95599997f, 7.5f), 1.2f).OnComplete(() =>
            {
                Instantiate(Resources.Load<GameObject>("Prefabs/UI/MiniGame/MiniGameCanvas"));
            });

            //Vector3(5.43900013,0.95599997,7.5)
        }
        else
        {
            //someting....
            return;
        }
    }

    public void Button_Click_Quit ()
    {
        MainCamera.transform.DOMove(P_From, 1.2f).OnComplete(() =>
        {
            this.GetComponent<Button>().interactable = true;
            SceneCameraMove.SetMoveTrue();
        });
    }
}