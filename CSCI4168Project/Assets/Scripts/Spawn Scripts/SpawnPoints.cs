using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    public int difficulty;
    public SpawnPoint spawnPoint1;
    public SpawnPoint spawnPoint2;

    public void setDifficulty(int difficulty)
    {
        this.difficulty = difficulty;
        spawnPoint1.setDifficulty(this.difficulty);
        spawnPoint2.setDifficulty(this.difficulty);
    }

    void Start()
    {
    }

    void Update()
    {
    }
}
