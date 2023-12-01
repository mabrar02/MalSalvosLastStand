using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    [SerializeField] private TextMeshProUGUI gearText;
    [SerializeField] private TextMeshProUGUI homebaseHealthText;
    [SerializeField] private GameObject buildMenu, battleMenu, gameOverMenu, victoryMenu;
    //add^ for playerHealthText
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
        GameManager.OnGameStateChanged += GameManagerStateChange;
        GameManager.OnGearValsChanged += GameManagerGearChange;
        GameManager.OnBaseHealthChanged += GameManagerHealthChange;
        //add^ for playerHealthText
        GameManager.OnPlayerHealthChanged += GameManagerPlayerHealthChange;
    }

    private void OnDestroy() {
        GameManager.OnGameStateChanged -= GameManagerStateChange;
        GameManager.OnGearValsChanged -= GameManagerGearChange;
        GameManager.OnBaseHealthChanged -= GameManagerHealthChange;
        //add^ for playerHealthText
        GameManager.OnPlayerHealthChanged -= GameManagerPlayerHealthChange;
    }

    private void GameManagerHealthChange(int val) {
        homebaseHealthText.text = val.ToString();
    }

    //add^ for playerHealthText
    private void GameManagerPlayerHealthChange(int val){
        if(val > playerHealthSlider.maxValue) {
            playerHealthSlider.maxValue = val;
        }
        playerHealthSlider.value = val;
        playerHealth.text = val.ToString();
    }

    private void GameManagerGearChange(int val) {
        gearText.text = val.ToString();
    }

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
        if (GameManager.Instance.UseGears(currentItemData.cost)) {
            AudioManager.Instance.Play("ShopBuy");
            currentItemData.OnPurchase();
            ClosePopup();
        }
    }

    public void DeclinePopup() {
        AudioManager.Instance.Play("ButtonPress");
        ClosePopup();
    }

    public void SetError(string msg) {
        errorText.GetComponent<ErrorText>().ShowErrorMessage(msg);
    }

    public void UpdateWaveText(string waveInfo) {
        waveText.text = waveInfo;
    }

}
