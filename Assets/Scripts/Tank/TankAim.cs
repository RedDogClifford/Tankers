using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TankAim : MonoBehaviour
{
    [HideInInspector] public Vector2 bulletDirection;
    [HideInInspector] public Camera camera;

    [SerializeField]
    private InputActionReference aim;

    [HideInInspector] public Vector3 touchWorldPosition;
    [HideInInspector] public BoxCollider bulletSpawnLocation;
    [HideInInspector] public Transform transformTA;

    // Start is called before the first frame update
    void Start()
    {
        bulletSpawnLocation = GetComponent<BoxCollider>();
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
        bulletDirection = context.ReadValue<Vector2>();

        //Update turrent direction

        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(bulletDirection);

        if(Physics.Raycast(ray, out hit))
        {
            //maybe filter to mask layer
            //then create a collider layer for this with specific mask name

            touchWorldPosition = hit.point;
            touchWorldPosition.y = transform.position.y;

            transform.LookAt(touchWorldPosition);

            transformTA = transform;
        }
    }
}
