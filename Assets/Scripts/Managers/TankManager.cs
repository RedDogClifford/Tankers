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

    public void SetUp()
    {
        //Grab Tank Components
        //

        //Use tank number?



    }
}
