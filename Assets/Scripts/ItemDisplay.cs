using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    [SerializeField] ShopItem item;
    [SerializeField] TextMeshProUGUI itemName;
    [SerializeField] TextMeshProUGUI itemPrice;
    [SerializeField] TextMeshProUGUI itemDescription;
    [SerializeField] Image itemImage;
    [SerializeField] Button itemButton;

    int upgradableId = 0;

    private void Start()
    {
        itemName.text = item.itemName;
        itemPrice.text = item.itemValue.ToString();
        itemDescription.text = item.itemDesc;
        itemImage.sprite = item.itemImage;
    }

    void Update()
    {
        if (GameManager.Instance.GetFishAmount() < item.itemValue) //Not enough money
        {
            itemButton.interactable = false;
        }
        else
        {
            itemButton.interactable = true;
        }
    }
    public void BuyItem()
    {
        if (GameManager.Instance.GetFishAmount() >= item.itemValue)
        {
            GameManager.Instance.ModifyFishAmt(-item.itemValue);
            GameManager.Instance.AddToInventory(item);

            if (item.upgradeable)
            {
                if (!item.firstUpgrade)
                {
                    GameManager.Instance.GetInventory().RemoveAt(upgradableId); //Remove old version
                }
                upgradableId = GameManager.Instance.GetInventory().IndexOf(item);
            }
            if (item.isUnlockFish)
            {
                GameManager.Instance.AddFishToRoster(item.unlockFish);
            }
        }

        if (item.oneTimePurchase)
        {
            itemPrice.text = "Purchased!";
            itemButton.interactable = false;
        }
        else if (item.upgradeable)
        {
            item = item.upgradeVersion; //Change item

            //Reset everything
            itemName.text = item.itemName;
            itemPrice.text = item.itemValue.ToString();
            itemDescription.text = item.itemDesc;
            itemImage.sprite = item.itemImage;
        }
    }
}
