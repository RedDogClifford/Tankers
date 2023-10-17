using System;
using UnityEngine;

[Serializable]
public class TankManager
{
    public Color tankColor;
    public Transform spawnPoint;
    public GameObject tankPrefab;

    [HideInInspector] public int tankNumber;
    [HideInInspector] public GameObject instance;

    private TankMovement movement;
    private GameObject canvasGameObject;

    public void SetUp()
    {
        //Grab Tank Components
        //
        movement = instance.GetComponent<TankMovement>();
        canvasGameObject = instance.GetComponentInChildren<Canvas>().gameObject;

        //Use tank number?
        movement.tankNumber = tankNumber;

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
