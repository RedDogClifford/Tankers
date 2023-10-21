using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class TankShoot : MonoBehaviour
{
    public GameObject bulletPrefab;

    [HideInInspector] public Camera camera;
    [HideInInspector] public EventSystem eventSystem;
    [HideInInspector] public GraphicRaycaster raycaster;

    private PointerEventData pointerEventData;

    public float reloadDelay = 1f;
    private float timer = 0f;

    public TankAim tankAim;

    [SerializeField]
    private InputActionReference shoot;

    private Button touch;
    private bool canShoot = false;

    // Start is called before the first frame update
    private void Awake()
    {
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
        if (canShoot && NotUI())
        {
            //Shoot bullet
            canShoot = false;
            Instantiate(bulletPrefab, tankAim.transformTA.TransformPoint(tankAim.bulletSpawnLocation.center), tankAim.transformTA.rotation);
        }
    }

    //Ensure user is not clicking on a UI element
    private bool NotUI()
    {
        pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = tankAim.bulletDirection;

        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerEventData, results);

        if (results.Count == 0)
        {
            return true;
        }

        return false;
    }
}
