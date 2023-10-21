using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[Serializable]
public class TankManager
{
    public Color tankColor;
    public Transform spawnPoint;
    public GameObject tankPrefab;

    [HideInInspector] public int tankNumber;
    [HideInInspector] public GameObject instance;

    [HideInInspector] public Camera camera;
    [HideInInspector] public EventSystem eventSystem;
    [HideInInspector] public GraphicRaycaster raycaster;

    private TankMovement movement;
    private GameObject canvasGameObject;
    private TankShoot turret;
    private TankAim turretPiece;

    public void SetUp()
    {
        //Grab Tank Components
        //
        movement = instance.GetComponent<TankMovement>();
        canvasGameObject = instance.GetComponentInChildren<Canvas>().gameObject;
        turret = instance.GetComponent<TankShoot>();
        turretPiece = instance.GetComponentInChildren<TankAim>();

        //Use tank number?
        movement.tankNumber = tankNumber;

        turretPiece.camera = camera;

        turret.camera = camera;
        turret.eventSystem = eventSystem;
        turret.raycaster = raycaster;

        //Update tank color
        MeshRenderer[] renderers = instance.GetComponentsInChildren<MeshRenderer>();

        for(int i=0; i<renderers.Length; i++)
        {
            renderers[i].material.color = tankColor;
        }
    }

    public void DisableControl()
    {
        movement.enabled = false;

        canvasGameObject.SetActive(false);
    }

    public void EnableControl()
    {
        movement.enabled = true;

        canvasGameObject.SetActive(true);
    }

    public void Reset()
    {
        instance.transform.position = spawnPoint.position;
        instance.transform.rotation = spawnPoint.rotation;

        instance.SetActive(false);
        instance.SetActive(true);
    }
}
