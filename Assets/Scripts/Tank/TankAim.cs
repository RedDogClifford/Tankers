using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TankAim : MonoBehaviour
{
    [HideInInspector] public Vector2 bulletDirection;
    [HideInInspector] public Camera gameCamera;

    [SerializeField]
    private InputActionReference aim;

    // Start is called before the first frame update
    void Start()
    {
        OnEnable();
    }

    private void OnEnable()
    {
        aim.action.performed += AimTurret;
    }

    private void OnDisable()
    {
        aim.action.performed -= AimTurret;
    }

    private void AimTurret(InputAction.CallbackContext context)
    {
        Vector2 bulletDirection = context.ReadValue<Vector2>();

        //Update turrent direction
        Vector3 direction = new Vector3(transform.position.x + bulletDirection.x, transform.position.y, transform.position.z + bulletDirection.y);

        //Add camera rotation

        transform.LookAt(direction);
    }
}
