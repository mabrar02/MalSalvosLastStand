
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
    //add^ for playerHealth
    public static event Action<int> OnPlayerHealthChanged;

    public GameObject switchCam;

    public int gears;
    private int baseHealth;
    private int playerHealth;

    public int maxPlayerHealth;
    public int maxBaseHealth;
    public float timer = 0;
    public float maxTime = 5;

    private void Awake() {
        Time.timeScale = 1;
        Instance = this;
    }

    private void Start() {
        UpdateGameState(GameState.BuildPhase);
        baseHealth = maxBaseHealth;
        playerHealth = maxPlayerHealth;
        OnPlayerHealthChanged?.Invoke(playerHealth);
        OnBaseHealthChanged?.Invoke(baseHealth);
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
                Invoke(nameof(HandleVictoryPhase), 3f);
                break;
            case GameState.LosePhase:
                Invoke(nameof(HandleLosePhase), 3f);
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }
    private void HandleVictoryPhase() {
        AudioManager.Instance.Stop("RespawnSound");
        AudioManager.Instance.Play("VictorySound");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
        player.SetActive(false);
        AudioManager.Instance.Stop("BattleTheme");
    }

    private void HandleLosePhase() {
        AudioManager.Instance.Stop("RespawnSound");
        AudioManager.Instance.Play("DefeatSound");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
        player.SetActive(false);
        AudioManager.Instance.Stop("BattleTheme");
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

        yield return new WaitForSeconds(3f);

        UpdateGameState(GameState.BuildPhase);
    }


    public bool UseGears(int cost) {
        if (gears - cost >= 0) {
            gears -= cost;

            OnGearValsChanged?.Invoke(gears);
            return true;
        }
        else {
            MenuManager.Instance.SetError("Not enough gears!");
            return false;
        }
    }

    public void AddGears(int bonus) {
        gears += bonus;
        OnGearValsChanged?.Invoke(gears);
    }

    public int GetGears() {
        return gears;
    }

    public int GetBaseHealth() {
        return baseHealth;
    }

    public int GetPlayerHealth()
    {
        return playerHealth;
    }

    public void IncreasePlayerHealth(int amount) {
        maxPlayerHealth += amount;
        playerHealth = maxPlayerHealth;
        OnPlayerHealthChanged?.Invoke(playerHealth);
    }

    public void IncreaseHomebaseHealth(int amount) {
        maxBaseHealth += amount;
        baseHealth = maxBaseHealth;

        OnBaseHealthChanged?.Invoke(baseHealth);
    }

    public void TakeDamage(int damage) {
        if (baseHealth <= 0) return;
        baseHealth -= damage;
        AudioManager.Instance.Play("BaseDamage");

        if(baseHealth <= 0) {
            baseHealth = 0;
            UpdateGameState(GameState.LosePhase); 
        }
        OnBaseHealthChanged?.Invoke(baseHealth);
    }


    public void PlayerTakeDamage(int damage) {
        if (playerHealth <= 0) return;
        AudioManager.Instance.Play("PlayerDamage");
        playerHealth -= damage;

        if (playerHealth <= 0) {
            playerHealth = 0;
            player.SetActive(false);
            player.transform.Find("PlayerObj").gameObject.SetActive(false);
            AudioManager.Instance.Play("RespawnSound");
            Invoke(nameof(respawnPlayer), 6f);
            //respawnCountdown.SetActive(true);
        }

        //add^ for playerHealth
        OnPlayerHealthChanged?.Invoke(playerHealth);
    }
    public void respawnPlayer() {
        player.transform.position = new Vector3(4.6f, 5.11f, 3.4f);
        playerHealth = maxPlayerHealth;
        OnPlayerHealthChanged?.Invoke(playerHealth);
        player.transform.Find("PlayerObj").gameObject.SetActive(true);

        if(State == GameState.BattlePhase) {
            player.SetActive(true);
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