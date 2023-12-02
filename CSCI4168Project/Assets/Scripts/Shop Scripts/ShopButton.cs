using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    public Shoppable shopItem;
    private TextMeshProUGUI costText;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnButtonClick);
        costText = GetComponentInChildren<TextMeshProUGUI>();

    }

    private void Update() {
        costText.text = shopItem.cost.ToString();
    }

    private void OnButtonClick() {
        MenuManager.Instance.OnShopItemClick(shopItem);

        AudioManager.Instance.Play("ButtonPress");
    }
}
