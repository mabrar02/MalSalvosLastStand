using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gearText;
    [SerializeField] private GameObject buildMenu, battleMenu;
    [SerializeField] private GameObject switchCam;

    void Update()
    {
        gearText.text = GameManager.Instance.GetGears().ToString();
    }

    private void Awake() {
        GameManager.OnGameStateChanged += GameManagerStateChange;
    }

    private void OnDestroy() {
        GameManager.OnGameStateChanged -= GameManagerStateChange;
    }

    private void GameManagerStateChange(GameState state) {
        buildMenu.SetActive(state == GameState.BuildPhase);
        battleMenu.SetActive(state == GameState.BattlePhase || state == GameState.SpawnPhase);
    }

    public void StartBuild() {
        GameManager.Instance.UpdateGameState(GameState.BuildPhase);
    }
    public void StartBattle() {
        GameManager.Instance.UpdateGameState(GameState.SpawnPhase);
    }
}
