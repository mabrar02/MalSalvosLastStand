using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public int difficulty;
    public MoveTo enemy;
    public float spawnRate = 3;
    private float timer = 0;
    public Transform goal;


    public void setGoal(Transform goal)
    {
        this.goal = goal;

    }

    public void setDifficulty(int difficulty)
    {
        this.difficulty = difficulty;
        setStats();
    }

    private void setStats() {
        if (this.difficulty == 1)
        {
            spawnRate = 3;
        }
        else if (this.difficulty == 2)
        {
            spawnRate = 2;
        }
        else if (this.difficulty == 3)
        {
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
            enemy.setGoal(this.goal);
            Instantiate(enemy, transform.position, transform.rotation);
            timer = 0;
        }

        
    }
}
