using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSector : MonoBehaviour
{

    public int difficulty;
    public SpawnPoints spawnPoints;

    public void setDifficulty(int difficulty) { 
        this.difficulty = difficulty;
        spawnPoints.setDifficulty(this.difficulty);
    }

    void Awake()
    {
    }

    void Update()
    {
    }
}
