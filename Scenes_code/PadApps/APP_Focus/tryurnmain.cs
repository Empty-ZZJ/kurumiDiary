using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tryurnmain : MonoBehaviour
{
    // Start is called before the first frame update
    private AsyncOperation operation;

    public GameObject obj_action;

    private void Start ()
    {
        SceneCameraMove.bool_move = true;
    }

    // Update is called once per frame
    private void Update ()
    {
    }

    public void openscence ()
    {
        obj_action.SetActive(true);
        SceneCameraMove.bool_move = true;
        StartCoroutine(start_game());
    }

    private IEnumerator start_game ()
    {//异步加载游戏场景
        operation = SceneManager.LoadSceneAsync(1);
        yield return operation;
    }
}