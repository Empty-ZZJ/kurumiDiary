using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public Transform target; public int mode;

    private void Update ()
    {
        if (mode == 1)
        {
            Vector3 targetDirection = target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = lookRotation;
        }
        else
        {
            transform.LookAt(target);
        }
    }
}