using UnityEngine;
using UnityEngine.InputSystem;

public class TankBase : MonoBehaviour
{
    [SerializeField]
    private InputActionReference movement;

    private Vector2 movementInputValue;

    private void Awake()
    {
        OnEnable();
    }

    private void OnEnable()
    {
        movement.action.performed += TurnBase;
    }

    private void OnDisable()
    {
        movement.action.performed -= TurnBase;
    }

    private void TurnBase(InputAction.CallbackContext context)
    {
        movementInputValue = context.ReadValue<Vector2>();

        Vector3 direction = new Vector3(transform.position.x + movementInputValue.x, transform.position.y, transform.position.z + movementInputValue.y);
        transform.LookAt(direction);
    }
}
