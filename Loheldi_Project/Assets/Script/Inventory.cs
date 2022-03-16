using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    private int mGold = 0;
    private int gold = 0;
    public int Gold { get; set; }
    private int mSpaces = 40;
    public int Space { get; set; }

    private string mName;
    public Text uNum;
    private int mCount;
    private int mHigh;
    

    public List<Item> mListSpace;

    public delegate void WasItemChange();
    public WasItemChange wasItemChanged;

    public delegate void WasGoldChange();
    public WasGoldChange wasGoldChanged;

    public delegate void WasCountChange();
    public WasCountChange wasCountChanged;

    public GameObject cTotalPanel;
    public Transform cSlot;
    public InventorySlot[] mSlot;

    public Text uGold;

    private Inventory mInventory;
    private Item mItem;
    public Text uName;
    private int mTotalPrice;

   /* public bool AddItem(Item item, int count)
    {
        if(mListSpace.Count >= mSpaces)
        {
            return false;
        }

        if(!IsExistItem(item, count))
        {
            Space space = new Space(item, count);

            mSpaces.add(space);
        }
        
        if(wasItemChanged != null)
        {
            wasItemChanged.Invoke();
        }
        return true;
    }

    bool IsExistItem(Item item, int count)
    {
        for(int i = 0; i < mSpaces.Count; i++)
        {
            if(mSpaces[i]._Item == item)
            {
                mSpaces[i].Count += count;

                return true;
            }
        }
        return false;
    }*/
    public void RemoveItem(Item item)
    {
        mListSpace.Remove(item);

        if(wasItemChanged != null)
        {
            wasItemChanged.Invoke();
        }
    }

    public void AddGold(int Gold)
    {
        Gold += gold;

        if(wasGoldChanged != null)
        {
            wasGoldChanged.Invoke();
        }
    }

    public void RemoveGold(int Gold)
    {
        Gold -= gold;

        if(wasGoldChanged != null)
        {
            wasGoldChanged.Invoke();
        }
    }

    public void ClickNext()
    {
        if (mCount < mHigh)
        {
            mCount++;
        }

        else
        {
            mCount = 1;
        }
        if(wasCountChanged != null)
        {
            wasCountChanged.Invoke();
        }

        UpdateText();
    }

    void UpdateText()
    {
        if(mName == string.Empty)
        {
            uNum.text = string.Format("{0}", mCount);

        }
        else
        {
            uNum.text = string.Format("{0} {1}", mName, mCount);
        }
    }

    void UpdateItemInfo(Item item)
    {
        mItem = item;

        uName.text = mItem.name;
    }

    /*public void ClickBuy()
    {
        Inventory inventory = GameManager.cInstance.cInventory;

        if(mTotalPrice < inventory.Gold)
        {
            inventory.AddGold(-mTotalPrice);

            inventory.AddItem(mItem, cSwitchSelect.Count);
        }

        else
        {
            Debug.Log("돈이 부족합니다..");
        }

        ClickBuyCancel();
    }*/

    public void ClickBuyCancel()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        mSlot = cSlot.GetComponentsInChildren<InventorySlot>();

        /*mInventory = GameManager.cInstance.cInventory;*/
        mInventory.wasItemChanged += UpdateInventory;
        mInventory.wasGoldChanged += UpdateGold;
    }

    public void UpdateGold()
    {
        uGold.text = string.Format("Gold:{0}", mInventory.Gold);
    }

    public void UpdateInventory()
    {
        for(int i = 0; i < mSlot.Length; i++)
        {
            if(IsEqual(i, mInventory.mListSpace.Count))
            {
                mSlot[i].AddItem(mInventory.mListSpace[i]);
            }
            else
            {
                mSlot[i].ResetItem();
            }
        }
    }

    private bool IsEqual(int i, int count)
    {
        throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class InventorySlot
{
    internal void AddItem(Item item)
    {
        throw new NotImplementedException();
    }

    internal void ResetItem()
    {
        throw new NotImplementedException();
    }
}