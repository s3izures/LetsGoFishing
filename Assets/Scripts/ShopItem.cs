using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ShopItem", order = 1)]
public class ShopItem : ScriptableObject
{
    public string itemName;
    public int itemValue;
    public Sprite itemImage;
    public string itemDesc;

    public bool oneTimePurchase = true;
    public bool upgradeable = false;
    public bool firstUpgrade = true;
    public ShopItem upgradeVersion;

    public bool isUnlockFish = false;
    public bool isFishMultiplier = false;
    public bool isFishBonus = false;

    public int fishMultiplier = 1;
    public FishObject unlockFish;
    public int fishBonus = 0;
}
