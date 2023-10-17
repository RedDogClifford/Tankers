using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TankManager[] playerTanks; //List of tanks in level, 1st tank is player
    public TankAgentManager[] tankAgents;
    public CameraControl cameraControl;

    // Start is called before the first frame update
    void Start()
    {
        SpawnAllTanks();
        SetCameraTargets();    
    }

    private void SpawnAllTanks()
    {
        for(int i=0; i< tankAgents.Length; i++)
        {
            //Spawn tank instance (prefab, position, rotation)
            tankAgents[i].instance = Instantiate(tankAgents[i].tankPrefab, tankAgents[i].spawnPoint.position, tankAgents[i].spawnPoint.rotation) as GameObject;
            tankAgents[i].tankNumber = i + 1;
            tankAgents[i].SetUp();
        }

        for(int i=0; i< playerTanks.Length; i++)
        {
            //Spawn tank instance (prefab, position, rotation)
            playerTanks[i].instance = Instantiate(playerTanks[i].tankPrefab, playerTanks[i].spawnPoint.position, playerTanks[i].spawnPoint.rotation) as GameObject;
            playerTanks[i].tankNumber = i + 1;
            playerTanks[i].SetUp();
        }
    }

    private void SetCameraTargets()
    {
        //Create collection of transforms for each tank
        Transform[] targets = new Transform[playerTanks.Length + tankAgents.Length];

        for(int i=0; i < playerTanks.Length; i++)
        {
            //Set targets to each tank transform
            targets[i] = playerTanks[i].instance.transform;
        }

        for(int i=0; i< tankAgents.Length; i++)
        {
            targets[playerTanks.Length + i] = tankAgents[i].instance.transform; 
        }
        

        //Set targets camera will follow
        cameraControl.targets = targets;
    }
}
