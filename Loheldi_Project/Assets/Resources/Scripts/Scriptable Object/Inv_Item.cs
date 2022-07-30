using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Inv_Item : ScriptableObject
{
    public enum ItemType //아이템 유형(무기/장비, 소모품, 재료, 기타)
    {
        Equipment,
        Used,
        Ingredient,
        ETC,
    }

    public string itemName; // 아이템의 이름
    public ItemType itemType; // 아이템 유형
    public Sprite itemImage; // 아이템의 이미지 (인벤토리 안에서 띄울)
    public GameObject itemPrefab; //아이템의 프리팹(아이템 생성시 프리팹으로 찍어냄)
    
    /*public int quantity;
    public bool stackable;*/

}
