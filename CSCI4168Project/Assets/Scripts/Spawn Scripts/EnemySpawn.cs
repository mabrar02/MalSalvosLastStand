using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{

    public int difficulty = 1;
    public SpawnSector spawnSector;


    void Start()
    {
        spawnSector.setDifficulty(this.difficulty);
    }

    void Update()
    {
        spawnSector.setDifficulty(this.difficulty);
    }
}
