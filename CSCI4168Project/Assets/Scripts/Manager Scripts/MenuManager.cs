using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/**
 * class used to manage the UI for the entire game
 */
public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    [SerializeField] private TextMeshProUGUI gearText;
    [SerializeField] private TextMeshProUGUI homebaseHealthText;
    [SerializeField] private GameObject buildMenu, battleMenu, gameOverMenu, victoryMenu;
    [SerializeField] private Slider playerHealthSlider;
    [SerializeField] private TextMeshProUGUI playerHealth;
    [SerializeField] private GameObject switchCam;

    private Shoppable currentItemData;
    [SerializeField] private GameObject shopConfirmPopup;
    [SerializeField] private TextMeshProUGUI shopItemName;
    [SerializeField] private TextMeshProUGUI shopItemDescription;

    [SerializeField] private GameObject placementSys;
    [SerializeField] private GameObject errorText;
    [SerializeField] private TextMeshProUGUI waveText;


    private void Start() {
        gearText.text = GameManager.Instance.GetGears().ToString();
        homebaseHealthText.text = GameManager.Instance.GetBaseHealth().ToString();
        playerHealth.text = GameManager.Instance.GetPlayerHealth().ToString();
        //add^ for playerHealthText
        playerHealthSlider.value = GameManager.Instance.GetPlayerHealth();
    }

    void Update()
    {
    }

    private void Awake() {
        Instance = this;
        // subscribes to all the game manager events
        GameManager.OnGameStateChanged += GameManagerStateChange;
        GameManager.OnGearValsChanged += GameManagerGearChange;
        GameManager.OnBaseHealthChanged += GameManagerHealthChange;
        GameManager.OnPlayerHealthChanged += GameManagerPlayerHealthChange;
    }

    private void OnDestroy() {
        GameManager.OnGameStateChanged -= GameManagerStateChange;
        GameManager.OnGearValsChanged -= GameManagerGearChange;
        GameManager.OnBaseHealthChanged -= GameManagerHealthChange;
        GameManager.OnPlayerHealthChanged -= GameManagerPlayerHealthChange;
    }

    // syncs base health UI
    private void GameManagerHealthChange(int val) {
        homebaseHealthText.text = val.ToString();
    }

    // syncs player health slider
    private void GameManagerPlayerHealthChange(int val){
        if(val > playerHealthSlider.maxValue) {
            playerHealthSlider.maxValue = val;
        }
        playerHealthSlider.value = val;
        playerHealth.text = val.ToString();
    }

    // syncs gear amount
    private void GameManagerGearChange(int val) {
        gearText.text = val.ToString();
    }

    // show a particular UI panel based on the current game state
    private void GameManagerStateChange(GameState state) {
        buildMenu.SetActive(state == GameState.BuildPhase);
        battleMenu.SetActive(state != GameState.BuildPhase && state != GameState.LosePhase);
        if(state == GameState.LosePhase) {
            Invoke(nameof(Defeat), 3f);
        }
        if(state == GameState.VictoryPhase) {
            Invoke(nameof(Victory), 3f);
        }
    }

    private void Victory() {
        victoryMenu.SetActive(true);
    }

    private void Defeat() {
        gameOverMenu.SetActive(true);
    }

    public void StartBuild() {
        GameManager.Instance.UpdateGameState(GameState.BuildPhase);
    }
    public void StartBattle() {
        AudioManager.Instance.Play("ButtonPress");
        GameManager.Instance.UpdateGameState(GameState.SpawnPhase);
    }

    // if an item in the shop is clicked, ensure that item's data is displayed in the confirm pop up
    public void OnShopItemClick(Shoppable shopItem) {
        placementSys.GetComponent<PlacementSystem>().StopPlacement();
        currentItemData = shopItem;
        shopConfirmPopup.SetActive(true);
        UpdatePopup();
    }

    // update the pop up window with current item's data
    public void UpdatePopup() {
        shopItemName.text = currentItemData.itemName;
        shopItemDescription.text = currentItemData.itemDescription;
    }

    public void ClosePopup() {
        shopConfirmPopup.SetActive(false);
    }

    public void AcceptPopup()
    {
        if (GameManager.Instance.UseGears(currentItemData.cost)) {
            currentItemData.OnPurchase();
            ClosePopup();
        }
    }

    public void DeclinePopup() {
        AudioManager.Instance.Play("ButtonPress");
        ClosePopup();
    }

    // set the error message
    public void SetError(string msg) {
        errorText.GetComponent<ErrorText>().ShowErrorMessage(msg);
    }

    public void UpdateWaveText(string waveInfo) {
        waveText.text = waveInfo;
    }

}
