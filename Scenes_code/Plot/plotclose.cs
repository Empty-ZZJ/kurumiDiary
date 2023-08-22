using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class plotclose : MonoBehaviour
{
    private AsyncOperation operation;

    // Start is called before the first frame update
    private void Start ()
    {
    }

    // Update is called once per frame
    private void Update ()
    {
    }

    public void plotcloseon ()
    {
        StartCoroutine(go_mian());
    }

    private IEnumerator go_mian ()
    {
        operation = SceneManager.LoadSceneAsync(2);
        yield return operation;
    }
}