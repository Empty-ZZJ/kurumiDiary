using UnityEngine;

public class cabinet : MonoBehaviour
{
    public Animator animator;
    private bool temp;

    public void buttoncabinet ()
    {
        if (!temp)
        {
            animator.Play("open");
            Invoke("tempf", 0.24f);
        }
        else
        {
            animator.Play("close");

            Invoke("tempf", 0.24f);
        }
    }

    private void tempf ()
    {
        temp = !temp;
    }
}