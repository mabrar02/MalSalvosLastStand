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

    public GameObject switchCam;

    public int gears;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        UpdateGameState(GameState.BuildPhase);
    }


    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState) {
            case GameState.BuildPhase:
                HandleBuildPhase();
                break;
            case GameState.BattlePhase:
                HandleBattlePhase();
                break;
            case GameState.CooldownPhase:
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }

    private void HandleBuildPhase() {
        buildSys.SetActive(true);
        player.SetActive(false);
        switchCam.GetComponent<SwitchCamera>().ChangeCamera();
    }

    private async void HandleBattlePhase() {
        placementSys.GetComponent<PlacementSystem>().StopPlacement();
        buildSys.SetActive(false);
        player.SetActive(true);
        switchCam.GetComponent<SwitchCamera>().ChangeCamera();

        await Task.Delay(5000);
        AddGears(50);
        UpdateGameState(GameState.BuildPhase);
    }


    public bool UseGears(int cost) {
        if (gears - cost >= 0) {
            gears -= cost;
            return true;
        }
        else return false;
    }

    public void AddGears(int bonus) {
        gears += bonus;
    }

    public int GetGears() {
        return gears;
    }

}

public enum GameState {
    BuildPhase,
    BattlePhase,
    CooldownPhase
}