using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * class used to manage the enemy wave spawning system
 */
public class WaveManager : MonoBehaviour
{

    // class representing a wave, with corresponding list of enemies
    [System.Serializable]
    public class Wave {
        public string name;
        public GameObject[] enemies;
        public float spawnRate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] spawnPoints;

    public float waveStartTime = 5f;
    public float waveCountDown;

    private float searchCountdown = 1f;

    [SerializeField] private Transform homebase;

    private void Start() {
        waveCountDown = waveStartTime;
        MenuManager.Instance.UpdateWaveText("Wave: " + (nextWave + 1) + "/" + waves.Length);
    }

    private void Update() {
        // only spawn if the state is battle/spawn
        if (GameManager.Instance.State != GameState.BattlePhase && GameManager.Instance.State != GameState.SpawnPhase) return;

        if (GameManager.Instance.State == GameState.BattlePhase) {
            // continue battle phase until wave is completed
            if (!EnemyIsAlive()) {
                WaveCompleted();
            }
        }

        // start the wave once the countdown is completed
        if (waveCountDown <= 0) {
            if(GameManager.Instance.State == GameState.SpawnPhase) {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else {
            waveCountDown -= Time.deltaTime;
        }


    }

    // if wave is completed, if no other waves left commence victory, otherwise go to build phase and ready next wave 
    private void WaveCompleted() {
        Debug.Log("WAVE COMPLETED");
        if(nextWave + 1 > waves.Length -1) {
            nextWave = 0;
            GameManager.Instance.UpdateGameState(GameState.VictoryPhase);
        }
        else {
            nextWave++;
            GameManager.Instance.UpdateGameState(GameState.CooldownPhase);
            MenuManager.Instance.UpdateWaveText("Wave: " + (nextWave + 1) + "/" + waves.Length);
        }

        waveCountDown = waveStartTime;
    }

    // check if any enemies are on the map
    private bool EnemyIsAlive() {
        searchCountdown -= Time.deltaTime;
        if(searchCountdown <= 0f) {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null) {
                return false;
            }
        }
        return true;
    }

    // iterate through each enemy in the wave list and spawn it
    IEnumerator SpawnWave(Wave _wave) {
        Debug.Log("Spawning wave " + _wave.name);
        GameManager.Instance.UpdateGameState(GameState.BattlePhase);
        
        for(int i = 0; i< _wave.enemies.Length; i++) {
            SpawnEnemy(_wave.enemies[i]);
            yield return new WaitForSeconds(1f / _wave.spawnRate);
        }

        yield break;
    }

    // instantiate an enemy at one of the 4 random spawn locations
    private void SpawnEnemy(GameObject _enemy) {
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemyObj = Instantiate(_enemy, _sp.position, Quaternion.identity);

        MoveTo enemyMove = enemyObj.GetComponent<MoveTo>();
        MoveToPlayer enemyMoveToPlayer = enemyObj.GetComponent<MoveToPlayer>();
        if(enemyMove != null) {
            enemyMove.goal = homebase;
        }
        else if(enemyMoveToPlayer != null){
            enemyMoveToPlayer.goal = homebase;
        }
        else {
            Debug.Log("ERROR SETTING MOVEMENT FOR ENEMY");
        }
    }

}
