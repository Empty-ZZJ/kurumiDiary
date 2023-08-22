using UnityEngine;

public class to_table : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator ani;

    private void Start ()
    {
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update ()
    {
    }

    public void endplay ()
    {
        ani.speed = 0;
    }
}