using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        HealthPotion,
        BuffPotion,
        Coin,
        CoolTimePotion,
        Sword
    }

    public ItemType itemType;
    public int amount;


    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Sword: return ItemAssets.Instance.swordSprite;
            case ItemType.HealthPotion: return ItemAssets.Instance.healthPotionSprite;
            case ItemType.CoolTimePotion: return ItemAssets.Instance.cooltimePotionSprite;
            case ItemType.Coin: return ItemAssets.Instance.coinSprite;
        }
         
    }
    public MeshRenderer GetSprite2()
    {
        switch (itemType)
        {
            default:
            case ItemType.Sword: return ItemAssets.Instance.swordSprite2;
            //case ItemType.HealthPotion: return ItemAssets.Instance.healthPotionSprite;
            //case ItemType.CoolTimePotion: return ItemAssets.Instance.cooltimePotionSprite;
            //case ItemType.Coin: return ItemAssets.Instance.coinSprite;
        }

    }
}
