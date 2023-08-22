using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class app_time : MonoBehaviour
{
    private AsyncOperation operation;

    public void openscence ()
    {
        StartCoroutine(start_game());
    }

    private IEnumerator start_game ()
    {
        operation = SceneManager.LoadSceneAsync(5);
        yield return operation;
    }
}