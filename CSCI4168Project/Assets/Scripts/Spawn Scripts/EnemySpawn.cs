using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{

    public int difficulty = 1;
    public SpawnSector spawnSector;
    public Transform goal;


    void Start()
    {
        spawnSector.setDifficulty(this.difficulty);
        spawnSector.setGoal(this.goal);
    }

    void Update()
    {
        spawnSector.setDifficulty(this.difficulty);
        spawnSector.setGoal(this.goal);
    }
}
