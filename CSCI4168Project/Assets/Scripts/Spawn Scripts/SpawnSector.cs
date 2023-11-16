using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSector : MonoBehaviour
{

    public int difficulty;
    public SpawnPoints spawnPoints;
    public Transform goal;

    public void setDifficulty(int difficulty) { 
        this.difficulty = difficulty;
        spawnPoints.setDifficulty(this.difficulty);
    }

    public void setGoal(Transform goal)
    {
        this.goal = goal;
        spawnPoints.setGoal(this.goal);
    }

    void Awake()
    {
    }

    void Update()
    {
    }
}
