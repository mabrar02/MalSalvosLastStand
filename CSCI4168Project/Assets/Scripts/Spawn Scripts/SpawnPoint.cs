using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public int difficulty;
    public TempEnemyMovement enemy;
    public float spawnRate = 3;
    private float timer = 0;

    public void setDifficulty(int difficulty)
    {
        this.difficulty = difficulty;
        setStats();
    }

    private void setStats() {
        if (this.difficulty == 1)
        {
            enemy.speed *= 1;
            spawnRate = 3;
        }
        else if (this.difficulty == 2)
        {
            enemy.speed *= 2;
            spawnRate = 2;
        }
        else if (this.difficulty == 3)
        {
            enemy.speed *= 3;
            spawnRate = 1;
        }
    }

    void Start()
    {
    }

    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else {
            Instantiate(enemy, transform.position, transform.rotation);
            timer = 0;
        }

        
    }
}
