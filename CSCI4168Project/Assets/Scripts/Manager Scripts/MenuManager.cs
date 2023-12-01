using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    [SerializeField] private TextMeshProUGUI gearText;
    [SerializeField] private TextMeshProUGUI homebaseHealthText;
    [SerializeField] private GameObject buildMenu, battleMenu, gameOverMenu;
    [SerializeField] private GameObject switchCam;

    private Shoppable currentItemData;
    [SerializeField] private GameObject shopConfirmPopup;
    [SerializeField] private TextMeshProUGUI shopItemName;
    [SerializeField] private TextMeshProUGUI shopItemDescription;

    [SerializeField] private GameObject placementSys;


    private void Start() {
        gearText.text = GameManager.Instance.GetGears().ToString();
        homebaseHealthText.text = GameManager.Instance.GetBaseHealth().ToString();
    }

    void Update()
    {
    }

    private void Awake() {
        Instance = this;
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
        battleMenu.SetActive(state != GameState.BuildPhase && state != GameState.LosePhase);
        gameOverMenu.SetActive(state == GameState.LosePhase);
    }

    public void StartBuild() {
        GameManager.Instance.UpdateGameState(GameState.BuildPhase);
    }
    public void StartBattle() {
        GameManager.Instance.UpdateGameState(GameState.SpawnPhase);
    }

    public void OnShopItemClick(Shoppable shopItem) {
        placementSys.GetComponent<PlacementSystem>().StopPlacement();
        currentItemData = shopItem;
        shopConfirmPopup.SetActive(true);
        UpdatePopup();
    }

    public void UpdatePopup() {
        shopItemName.text = currentItemData.itemName;
        shopItemDescription.text = currentItemData.itemDescription;
    }

    public void ClosePopup() {
        shopConfirmPopup.SetActive(false);
    }

    public void AcceptPopup()
    {
        ClosePopup();
        currentItemData.OnPurchase();
    }
}
