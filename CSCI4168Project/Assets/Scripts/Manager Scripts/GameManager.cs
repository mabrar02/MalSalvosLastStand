using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject player;
    public GameObject buildSys;
    public GameObject placementSys;
    public GameState State;
    public static event Action<GameState> OnGameStateChanged;
    public static event Action<int> OnGearValsChanged;
    public static event Action<int> OnBaseHealthChanged;

    public GameObject switchCam;

    public int gears;
    public int baseHeath;

    public int playerHealth;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        UpdateGameState(GameState.BuildPhase);
    }


    public void UpdateGameState(GameState newState)
    {
        Debug.Log("CURRENT STATE: " +  newState);
        State = newState;

        switch (newState) {
            case GameState.BuildPhase:
                HandleBuildPhase();
                break;
            case GameState.SpawnPhase:
                HandleSpawnPhase(); 
                break;
            case GameState.BattlePhase:
                break;
            case GameState.CooldownPhase:
                StartCoroutine(HandleCooldownPhase());
                break;
            case GameState.VictoryPhase:
                break;
            case GameState.LosePhase:
                HandleLosePhase();
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }

    private void HandleLosePhase() {
        Time.timeScale = 0;
    }

    private void HandleBuildPhase() {
        buildSys.SetActive(true);
        player.SetActive(false);
        switchCam.GetComponent<SwitchCamera>().ChangeCamera();
    }

    private void HandleSpawnPhase() {
        placementSys.GetComponent<PlacementSystem>().StopPlacement();
        buildSys.SetActive(false);
        player.SetActive(true);
        switchCam.GetComponent<SwitchCamera>().ChangeCamera();
    }

    private IEnumerator HandleCooldownPhase() {

        yield return new WaitForSeconds(10f);

        UpdateGameState(GameState.BuildPhase);
    }


    public bool UseGears(int cost) {
        if (gears - cost >= 0) {
            gears -= cost;

            OnGearValsChanged?.Invoke(gears);
            return true;
        }
        else return false;
    }

    public void AddGears(int bonus) {
        gears += bonus;
        OnGearValsChanged?.Invoke(gears);
    }

    public int GetGears() {
        return gears;
    }

    public int GetBaseHealth() {
        return baseHeath;
    }

    public void TakeDamage(int damage) {
        baseHeath -= damage;

        OnBaseHealthChanged?.Invoke(baseHeath);

        if(baseHeath <= 0) {
            UpdateGameState(GameState.LosePhase); 
        }
    }

    public void PlayerTakeDamage(int damage) {
        playerHealth -= damage;
        if(playerHealth <= 0) {
            playerHealth = 0;
        }
    }
}

public enum GameState {
    BuildPhase,
    SpawnPhase,
    BattlePhase,
    CooldownPhase,
    LosePhase,
    VictoryPhase,
}