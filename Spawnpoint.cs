using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnpoint : Saveable
{
    const string CUR_SPAWNPOINT_TAG = "_CurSpawnpoint";
    public Transform spawnPosition;
    public static Spawnpoint curSpawnPoint;

    public override void Awake()
    {
        base.Awake();
        if (curSpawnPoint == null) curSpawnPoint = this;

        if (spawnPosition == null)
            Debug.Log("Fatal warning! Spawnpoint " + gameObject.name + " has no spawn position!!");
    }

    public GameObject Respawn(GameObject player)
    {
        GameObject playerInstance = Instantiate(player, spawnPosition.position, spawnPosition.rotation);
        return playerInstance;
    }

    public void Set_Spawn()
    {
        curSpawnPoint = this;
    }
}
