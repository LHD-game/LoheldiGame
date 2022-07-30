using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Inv_Item : ScriptableObject
{
    public enum ItemType //������ ����(����/���, �Ҹ�ǰ, ���, ��Ÿ)
    {
        Equipment,
        Used,
        Ingredient,
        ETC,
    }

    public string itemName; // �������� �̸�
    public ItemType itemType; // ������ ����
    public Sprite itemImage; // �������� �̹��� (�κ��丮 �ȿ��� ���)
    public GameObject itemPrefab; //�������� ������(������ ������ ���������� ��)
    
    /*public int quantity;
    public bool stackable;*/

}
