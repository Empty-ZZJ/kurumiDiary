using UnityEngine;

public class openWeb : MonoBehaviour
{
    public string web;

    public void openweburl ()
    {
        Application.OpenURL(web);
    }
}