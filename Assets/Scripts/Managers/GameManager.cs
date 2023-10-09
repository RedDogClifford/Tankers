using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TankManager[] tanks; //List of tanks in level, 1st tank is player
    public CameraControl cameraControl;

    // Start is called before the first frame update
    void Start()
    {
        SpawnAllTanks();
        SetCameraTargets();    
    }

    private void SpawnAllTanks()
    {
        for(int i=0; i<tanks.Length; i++)
        {
            //Spawn tank instance (prefab, position, rotation)
            tanks[i].instance = Instantiate(tanks[i].tankPrefab, tanks[i].spawnPoint.position, tanks[i].spawnPoint.rotation) as GameObject;
            tanks[i].tankNumber = i + 1;
            tanks[i].SetUp();
        }
    }

    private void SetCameraTargets()
    {
        //Create collection of transforms for each tank
        Transform[] targets = new Transform[tanks.Length];

        for(int i=0; i < targets.Length; i++)
        {
            //Set targets to each tank transform
            targets[i] = tanks[i].instance.transform;
        }

        //Set targets camera will follow
        cameraControl.targets = targets;
    }
}
