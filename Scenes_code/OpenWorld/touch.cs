using UnityEngine;

public class touch : MonoBehaviour
{
    public VariableJoystick variableJoystick;
    public CharacterController controller;
    public Transform people;
    private Vector2 touch_position;
    private Vector3 velocity;
    public float speed = 5f;
    public float gravity = -15f;

    public void Start ()
    {
        variableJoystick.SetMode(JoystickType.Floating);
    }

    private void Update ()
    {
        //坐标：前后 x z  上下 y
        // touch_position = new Vector2(people.position.x, people.position.z);
        Vector3 move = transform.right * variableJoystick.Direction.x + transform.forward * variableJoystick.Direction.y;
        controller.Move(move * speed * Time.deltaTime);

        controller.Move(move * speed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        /*
            touch_position.x += variableJoystick.Direction.x * 0.1f;
            touch_position.y += variableJoystick.Direction.y * 0.1f;
        Debug.Log(variableJoystick.Direction);
        people.DOMoveX(touch_position.x, 0.25f);
        people.DOMoveZ(touch_position.y, 0.25f);
        */
    }
}