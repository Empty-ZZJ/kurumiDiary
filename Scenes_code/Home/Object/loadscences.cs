using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadscences : MonoBehaviour
{
    private AsyncOperation operation = new AsyncOperation();
    public int wantscecscindex = 0;

    public void openscence ()
    {
        GameObject load = Resources.Load<GameObject>("Prefabs/UI/LoadingEvent");
        Instantiate(load);
        StartCoroutine(start_game());
    }

    private IEnumerator start_game ()
    {
        operation = SceneManager.LoadSceneAsync(wantscecscindex);
        yield return operation;
    }

    public void OpenLoadAction (int OpenScence)
    {
        GameObject load = Resources.Load<GameObject>("Prefabs/UI/LoadingEvent");
        Instantiate(load);
        if (OpenScence >= 0)//需要跳转场景
        {
            wantscecscindex = OpenScence;
            StartCoroutine(start_game());
        }
    }
}