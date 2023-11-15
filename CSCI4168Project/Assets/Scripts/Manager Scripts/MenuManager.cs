using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gearText;
    [SerializeField] private TextMeshProUGUI homebaseHealthText;
    [SerializeField] private GameObject buildMenu, battleMenu;
    [SerializeField] private GameObject switchCam;

    private void Start() {
        gearText.text = GameManager.Instance.GetGears().ToString();
        homebaseHealthText.text = GameManager.Instance.GetBaseHealth().ToString();
    }

    void Update()
    {
    }

    private void Awake() {
        GameManager.OnGameStateChanged += GameManagerStateChange;
        GameManager.OnGearValsChanged += GameManagerGearChange;
        GameManager.OnBaseHealthChanged += GameManagerHealthChange;
    }

    private void OnDestroy() {
        GameManager.OnGameStateChanged -= GameManagerStateChange;
        GameManager.OnGearValsChanged -= GameManagerGearChange;
        GameManager.OnBaseHealthChanged -= GameManagerHealthChange;
    }

    private void GameManagerHealthChange(int val) {
        homebaseHealthText.text = val.ToString();
    }

    private void GameManagerGearChange(int val) {
        gearText.text = val.ToString();
    }

    private void GameManagerStateChange(GameState state) {
        buildMenu.SetActive(state == GameState.BuildPhase);
        battleMenu.SetActive(state != GameState.BuildPhase);
        if(state == GameState.LosePhase) {
            Debug.Log("YOU LOSE!");
        }
    }

    public void StartBuild() {
        GameManager.Instance.UpdateGameState(GameState.BuildPhase);
    }
    public void StartBattle() {
        GameManager.Instance.UpdateGameState(GameState.SpawnPhase);
    }
}
