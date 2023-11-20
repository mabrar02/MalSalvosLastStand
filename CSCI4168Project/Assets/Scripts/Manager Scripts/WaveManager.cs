using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{


    [System.Serializable]
    public class Wave {
        public string name;
        public GameObject enemy;
        public int count;
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
    }

    private void Update() {
        if (GameManager.Instance.State != GameState.BattlePhase && GameManager.Instance.State != GameState.SpawnPhase) return;

        if (GameManager.Instance.State == GameState.BattlePhase) {
            if (!EnemyIsAlive()) {
                WaveCompleted();
            }
        }

        if (waveCountDown <= 0) {
            if(GameManager.Instance.State == GameState.SpawnPhase) {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else {
            waveCountDown -= Time.deltaTime;
        }


    }

    private void WaveCompleted() {
        Debug.Log("WAVE COMPLETED");
        if(nextWave + 1 > waves.Length -1) {
            nextWave = 0;
            GameManager.Instance.UpdateGameState(GameState.VictoryPhase);
        }
        else {
            nextWave++;
            GameManager.Instance.UpdateGameState(GameState.CooldownPhase);
        }

        waveCountDown = waveStartTime;
    }

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

    IEnumerator SpawnWave(Wave _wave) {

        Debug.Log("Spawning wave " + _wave.name);
        GameManager.Instance.UpdateGameState(GameState.BattlePhase);
        
        for(int i = 0; i< _wave.count; i++) {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.spawnRate);
        }

        yield break;
    }

    private void SpawnEnemy(GameObject _enemy) {
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemyObj = Instantiate(_enemy, _sp.position, Quaternion.identity);

        MoveTo enemyMove = enemyObj.GetComponent<MoveTo>();
        if(enemyMove != null) {
            enemyMove.goal = homebase;
        }
        else {
            Debug.Log("ERROR SETTING HOMEBASE FOR ENEMY");
        }
    }

}
