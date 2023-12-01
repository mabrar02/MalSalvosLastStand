using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    public ShopItem shopItem;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick() {
        MenuManager.Instance.OnShopItemClick(shopItem);

        AudioManager.Instance.Play("ButtonPress");
    }
}
