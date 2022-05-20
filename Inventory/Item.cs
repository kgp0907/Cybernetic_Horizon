using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
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
            case ItemType.HealthPotion: return ItemAssets.Instance.healthPotionSprite2;
            case ItemType.CoolTimePotion: return ItemAssets.Instance.cooltimePotionSprite2;
                //case ItemType.HealthPotion: return ItemAssets.Instance.healthPotionSprite;
                //case ItemType.CoolTimePotion: return ItemAssets.Instance.cooltimePotionSprite;
                //case ItemType.Coin: return ItemAssets.Instance.coinSprite;
        }

    }

    public Color GetColor()
    {
        switch (itemType)
        {
            default:
            case ItemType.Sword: return new Color(0, 0, 0);
            case ItemType.HealthPotion: return new Color(1, 0, 0);
            case ItemType.CoolTimePotion: return new Color(0, 0, 1);
            case ItemType.Coin: return new Color(1, 1, 0);
        }
    }

    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.Coin:
            case ItemType.HealthPotion:
                return true;
            case ItemType.CoolTimePotion:
                return true;
            case ItemType.Sword:
                return false;

        }
    }

}
