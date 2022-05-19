using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorld : MonoBehaviour
{
  
   
    public static ItemWorld SpawnItemWorld(Vector3 position,Item item)
    {
       Transform transform = Instantiate(ItemAssets.Instance.pfItemWorld,position,Quaternion.identity);

        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        //Debug.Log(transform);

        itemWorld.SetItem(item);
        Debug.Log(item);
        return itemWorld;

    }
    private Item item;

    private SpriteRenderer spriteRenderer;
    private MeshRenderer meshRenderer;
    private void Awake()
    {
       spriteRenderer = GetComponent<SpriteRenderer>();
        meshRenderer = GetComponent<MeshRenderer>();
    }


    public void SetItem(Item item)
    {
        this.item = item;
      //  spriteRenderer.sprite = item.GetSprite();
        meshRenderer = item.GetSprite2();
    }
 
}
