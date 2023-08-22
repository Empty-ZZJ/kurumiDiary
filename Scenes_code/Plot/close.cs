using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class close : MonoBehaviour
{
    private AsyncOperation operation;
    public GameObject loadingaction;

    public void mapmouseon ()
    {
        loadingaction.SetActive(true);
        StartCoroutine(start_map());
    }

    private IEnumerator start_map ()
    {
        operation = SceneManager.LoadSceneAsync(1);
        yield return operation;
    }
}