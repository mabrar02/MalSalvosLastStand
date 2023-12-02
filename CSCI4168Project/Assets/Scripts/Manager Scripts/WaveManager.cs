using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{


    [System.Serializable]
    public class Wave {
        public string name; // name of wave
        public GameObject[] enemies; // array of enemies
        public float spawnRate; // rate at which they spawn
        public float dropPercent; // Percent of GearDrop to drop
        public float healthBoost; // Amount to boost health by
        public float damageBoost; // Amount to boost damage by
    }

    public class Adjustments
    {
        public float dropPercent; // Percent of GearDrop to drop
        public float healthBoost; // Amount to boost health by
        public float damageBoost; // Amount to boost damage by

        public Adjustments(float dropPercent, float healthBoost, float damageBoost)
        {
            this.dropPercent = dropPercent;
            this.healthBoost = healthBoost;
            this.damageBoost = damageBoost;
        }
    }

    public Wave[] waves; // array of waves
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
            MenuManager.Instance.UpdateWaveText("Wave: " + (nextWave + 1) + "/" + waves.Length);
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
        
        for(int i = 0; i< _wave.enemies.Length; i++) {
            Adjustments toAdjust = new(_wave.dropPercent, _wave.healthBoost, _wave.damageBoost);
            SpawnEnemy(_wave.enemies[i], toAdjust);
            yield return new WaitForSeconds(1f / _wave.spawnRate);
        }

        yield break;
    }

    private void SpawnEnemy(GameObject _enemy, Adjustments adjustments)
    {
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemyObj = Instantiate(_enemy, _sp.position, Quaternion.identity);

        MoveTo enemyMove = enemyObj.GetComponent<MoveTo>();
        MoveToPlayer enemyMoveToPlayer = enemyObj.GetComponent<MoveToPlayer>();

        makeAdjustments(adjustments, enemyObj);

        if (enemyMove != null)
        {
            enemyMove.goal = homebase;
        }
        else if (enemyMoveToPlayer != null)
        {
            enemyMoveToPlayer.goal = homebase;
        }
        else
        {
            Debug.Log("ERROR SETTING MOVEMENT FOR ENEMY");
        }

        // Adjust the enemy stats according to wave settings.
        static void makeAdjustments(Adjustments adjustments, GameObject enemyObj)
        {
            // Make adjustments according to wave settings
            Enemy enemyScript = enemyObj.GetComponent<Enemy>();
            // set gearAddition on enemy
            if (adjustments.dropPercent != 0)
            {
                // decreasing
                enemyScript.gearAddition = (int)(enemyScript.gearAddition * adjustments.dropPercent);
            }
            // set health on enemy
            if (adjustments.healthBoost != 0)
            {
                //increasing 
                enemyScript.maxHealth = enemyScript.maxHealth * (1.0f + adjustments.healthBoost);
            }
            // set damage on enemy
            if (adjustments.damageBoost != 0)
            {
                // increasing
                enemyScript.damageToBase = (int)(enemyScript.damageToBase * (1.0f + adjustments.damageBoost));
            }
        }
    }

}
