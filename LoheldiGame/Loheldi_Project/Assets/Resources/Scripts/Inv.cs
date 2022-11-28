using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inv : MonoBehaviour
{
    public GameObject slotPrefab;
    public const int numSlots = 10;
    Image[] itemImages = new Image[numSlots];
    Item[] items = new Item[numSlots];
    GameObject[] slots = new GameObject[numSlots];
    // Start is called before the first frame update
    void Start()
    {
        CreateSlots();
    }

    public void CreateSlots()
    {
        if(slotPrefab != null)
        {
            for(int i = 0; i < numSlots; i++)
            {
                GameObject newSlot = Instantiate(slotPrefab);
                newSlot.name = "ItemSlot_" + i;

                newSlot.transform.SetParent(gameObject.transform.GetChild(0).transform);

                slots[i] = newSlot;
                itemImages[i] = newSlot.transform.GetChild(1).GetComponent<Image>();
            }
        }
    }
}
