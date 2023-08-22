using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Logo : MonoBehaviour
{

    public GameObject MainGame;
    public GameObject LogoImage;
    public void Start ()
    {
        Invoke(nameof(Fide), 2.5f);
    }
    public void Fide ()
    {
        LogoImage.GetComponent<Image>().DOFade(0, 1f).OnComplete(SetMainGame);
    }

    public void SetMainGame ()
    {
        MainGame.SetActive(true);
        this.gameObject.SetActive(false);
    }

}