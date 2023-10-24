using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class TankShoot : MonoBehaviour
{
    public GameObject bulletPrefab;

    [HideInInspector] public Camera gameCamera;
    [HideInInspector] public EventSystem eventSystem;
    [HideInInspector] public GraphicRaycaster raycaster;
    private BoxCollider bulletSpawnLocation;

    private PointerEventData pointerEventData;

    public float reloadDelay = 1f;
    private float timer = 0f;

    [SerializeField]
    private InputActionReference shoot;

    private bool canShoot = false;

    // Start is called before the first frame update
    private void Awake()
    {
        bulletSpawnLocation = GetComponent<BoxCollider>();
        OnEnable();
    }

    private void OnEnable()
    {
        shoot.action.performed += ShootBullet;
    }

    private void OnDisable()
    {
        shoot.action.performed -= ShootBullet;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!canShoot)
        {
            ShootDelay();
        }
    }

    private void ShootDelay()
    {
        timer += Time.deltaTime;
        
        if (timer >= reloadDelay)
        {
            timer = 0f;
            canShoot = true;
        }
    }

    private void ShootBullet(InputAction.CallbackContext context)
    {
        //Player wants to shoot bullet            
        Vector2 bulletDirection = context.ReadValue<Vector2>();

        Debug.Log("here! " + bulletDirection);

        if (canShoot)
        {
            Vector3 direction = new Vector3(transform.position.x + bulletDirection.x, transform.position.y, transform.position.z + bulletDirection.y);

            transform.LookAt(direction);

            //Shoot bullet
            canShoot = false;
            Instantiate(bulletPrefab, transform.TransformPoint(bulletSpawnLocation.center), transform.rotation);
        }
    }

    /*
    private void ShootBullet(InputAction.CallbackContext context)
    {
        //Player wants to shoot bullet            
        Vector2 bulletDirection = context.ReadValue<Vector2>();

        Debug.Log("here! " + bulletDirection);

        if (canShoot && NotUI(bulletDirection))
        {
            RaycastHit hit;
            Ray ray = gameCamera.ScreenPointToRay(bulletDirection);

            if (Physics.Raycast(ray, out hit))
            {
                //maybe filter to mask layer
                //then create a collider layer for this with specific mask name

                Vector3 touchWorldPosition = hit.point;
                touchWorldPosition.y = transform.position.y;

                transform.LookAt(touchWorldPosition);

                //Shoot bullet
                canShoot = false;
                Instantiate(bulletPrefab, transform.TransformPoint(bulletSpawnLocation.center), transform.rotation);
            }
        }        
    }

    //Ensure user is not clicking on a UI element
    private bool NotUI(Vector2 bulletDirection)
    {
        pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = bulletDirection;

        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerEventData, results);

        if (results.Count == 0)
        {
            return true;
        }

        return false;
    }
    */
}
