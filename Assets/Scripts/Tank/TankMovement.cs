using UnityEngine;
using UnityEngine.InputSystem;

public class TankMovement : MonoBehaviour
{
    public int tankNumber = 1;
    public float speed = 0.1f;

    [SerializeField]
    private InputActionReference movement;

    private Rigidbody tankBody;
    private Vector2 movementInputValue;

    private void Awake()
    {
        tankBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        movementInputValue = movement.action.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector3 direction = new Vector3(movementInputValue.x, 0, movementInputValue.y);

        tankBody.MovePosition(tankBody.position + direction * speed);
    }
}
