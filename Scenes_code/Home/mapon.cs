using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mapon : MonoBehaviour
{
    // Start is called before the first frame update
    private AsyncOperation operation;

    public void mapmouseon ()
    {
        StartCoroutine(start_map());
    }

    private IEnumerator start_map ()
    {
        operation = SceneManager.LoadSceneAsync(2);
        yield return operation;
    }
}