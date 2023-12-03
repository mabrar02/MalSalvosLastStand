
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UnityEngine;

/**
 * Manages the game logic for the entire game
 */
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
        // initialize variables, start the build phase
        UpdateGameState(GameState.BuildPhase);
        baseHealth = maxBaseHealth;
        playerHealth = maxPlayerHealth;
        OnPlayerHealthChanged?.Invoke(playerHealth);
        OnBaseHealthChanged?.Invoke(baseHealth);
    }

    // method used to change the current game state of the game
    public void UpdateGameState(GameState newState)
    {
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

        // trigger event
        OnGameStateChanged?.Invoke(newState);
    }

    // on victory, freeze game and play victory sound effect
    private void HandleVictoryPhase() {
        AudioManager.Instance.Stop("RespawnSound");
        AudioManager.Instance.Play("VictorySound");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
        player.SetActive(false);
        AudioManager.Instance.Stop("BattleTheme");
    }

    // on defeat, freeze game and play defeat sound effect
    private void HandleLosePhase() {
        AudioManager.Instance.Stop("RespawnSound");
        AudioManager.Instance.Play("DefeatSound");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
        player.SetActive(false);
        AudioManager.Instance.Stop("BattleTheme");
    }

    // when building starts, disable player, switch cameras, and return player to full health
    private void HandleBuildPhase() {
        buildSys.SetActive(true);
        player.SetActive(false);
        switchCam.GetComponent<SwitchCamera>().ChangeCamera();
        HealPlayer(10000);
        HealBase(5);
    }

    // when spawning starts, enable player, switch cameras
    private void HandleSpawnPhase() {
        placementSys.GetComponent<PlacementSystem>().StopPlacement();
        buildSys.SetActive(false);
        player.SetActive(true);
        switchCam.GetComponent<SwitchCamera>().ChangeCamera();
    }

    // wait 3 seconds then start build phase
    private IEnumerator HandleCooldownPhase() {

        yield return new WaitForSeconds(3f);

        UpdateGameState(GameState.BuildPhase);
    }


    // if you have gears, use them and trigger the change event, else trigger an error notification
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

    // increases current amount of gears
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

    // heals player based on amount passed in as param
    public void HealPlayer(int amount) {
        playerHealth += amount;
        if(playerHealth > maxPlayerHealth) {
            playerHealth = maxPlayerHealth;
        }
        OnPlayerHealthChanged?.Invoke(playerHealth);
    }

    // heals base based on amount passed in as param
    public void HealBase(int amount) {
        baseHealth += amount;
        if (baseHealth > maxBaseHealth) {
            baseHealth = maxBaseHealth;
        }
        OnBaseHealthChanged?.Invoke(baseHealth);
    }

    // used to increase player max health
    public void IncreasePlayerHealth(int amount) {
        maxPlayerHealth += amount;
        playerHealth = maxPlayerHealth;
        OnPlayerHealthChanged?.Invoke(playerHealth);
    }

    // used to increase base max health
    public void IncreaseHomebaseHealth(int amount) {
        maxBaseHealth += amount;
        baseHealth = maxBaseHealth;

        OnBaseHealthChanged?.Invoke(baseHealth);
    }

    // inflicts damage to homebase, if below 0, lose the game
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


    // inflicts damage to player, if below 0, die and respawn
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

        }


        OnPlayerHealthChanged?.Invoke(playerHealth);
    }

    // reset players position and health as well as re-enable the player object
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

// list of possible game states
public enum GameState {
    BuildPhase,
    SpawnPhase,
    BattlePhase,
    CooldownPhase,
    LosePhase,
    VictoryPhase,
}