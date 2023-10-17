using UnityEngine;
using UnityEngine.InputSystem;

public class TankMovement : MonoBehaviour
{
    public int tankNumber = 1;
    public float speed = 12f;
    public float turnSpeed = 180f;

    [HideInInspector] public Rigidbody rigidbody;

    [SerializeField]
    private InputActionReference movement;

    private Vector2 movementInputValue;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        //Store value of input axes
        movementInputValue = movement.action.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Move();

        Turn();
    }

    private void Move()
    {
        Vector3 movement = transform.forward * movementInputValue.y * speed * Time.deltaTime;

        rigidbody.MovePosition(rigidbody.position + movement);
    }

    private void Turn()
    {
        float turn = movementInputValue.x * turnSpeed * Time.deltaTime;

        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        rigidbody.MoveRotation(rigidbody.rotation * turnRotation);
    }
}
