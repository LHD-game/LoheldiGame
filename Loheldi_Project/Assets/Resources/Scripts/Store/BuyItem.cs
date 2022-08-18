using BackEnd;
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyItem : MonoBehaviour
{
    [SerializeField]
    private GameObject StoreBuyPanel;

    static GameObject main_ui;
    static GameObject buy_suc_panel;
    static string iCode = "";

    static int this_cost = 0;

    public void PopBuyBtn()
    {
        //������ �ڵ� �����ͼ� iCode ������ ����
        GameObject item_code = this.transform.Find("ItemCode").gameObject;
        Text item_code_txt = item_code.GetComponent<Text>();
        iCode = item_code_txt.text;

        //������ �̸� �����ͼ� txt�� ����
        GameObject item_name = this.transform.Find("ItemName").gameObject;
        Text txt = item_name.GetComponent<Text>();
 
        //������ ���� �����ͼ� price_txt�� ����
        GameObject price_parent = this.transform.Find("CostImg").gameObject;
        GameObject item_price = price_parent.transform.Find("CostTxt").gameObject;
        Text price_txt = item_price.GetComponent<Text>();

        //todo: ���� �г� ��ü ����
        GameObject canvas = GameObject.Find("Canvas");
        main_ui = canvas.transform.Find("mainUI").gameObject;
        GameObject child = Instantiate(StoreBuyPanel, main_ui.transform);

        buy_suc_panel = canvas.transform.Find("StoreBuySucPanel").gameObject;

        GameObject Asset_StorePopup = child.transform.Find("Asset_StorePopup").gameObject;

        //������ �̸� ����
        GameObject buy_item_name = Asset_StorePopup.transform.Find("ItemName").gameObject;
        Text buy_txt = buy_item_name.GetComponent<Text>();
        buy_txt.text = txt.text;

        //�� ���� ���� ����
        GameObject my_coin_parent = Asset_StorePopup.transform.Find("MyCoin").gameObject;
        GameObject my_coin = my_coin_parent.transform.Find("Text").gameObject;
        Text my_coin_txt = my_coin.GetComponent<Text>();
        my_coin_txt.text = PlayerPrefs.GetInt("Wallet").ToString(); //���� ���� ���� �� ����.

        //������ ���� ����
        GameObject item_cost_parent = Asset_StorePopup.transform.Find("ItemCost").gameObject;
        GameObject item_cost = item_cost_parent.transform.Find("Text").gameObject;
        Text item_cost_txt = item_cost.GetComponent<Text>();
        item_cost_txt.text = price_txt.text;

        this_cost = Int32.Parse(item_cost_txt.text);    //���� ������ ����

        //�ܾ� ����
        int result_cost = Convert.ToInt32(my_coin_txt.text) - Convert.ToInt32(item_cost_txt.text);

        GameObject change_cost_parent = Asset_StorePopup.transform.Find("ChangeCost").gameObject;
        GameObject change_cost = change_cost_parent.transform.Find("Text").gameObject;
        Text change_cost_txt = change_cost.GetComponent<Text>();
        change_cost_txt.text = result_cost.ToString();

        GameObject DisableBuyBtn = Asset_StorePopup.transform.Find("DisableBuyBtn").gameObject;
        if(result_cost >= 0) //���� ����>������ ����
        {
            change_cost_txt.color = Color.black;
            DisableBuyBtn.SetActive(false);
        }
        else
        {
            change_cost_txt.color = Color.red;
            DisableBuyBtn.SetActive(true);
        }
    }

    public void PopUpgradeBtn() {
        //������ �̸� �����ͼ� txt�� ����
        GameObject name_parent = this.transform.Find("NameBg").gameObject;
        GameObject item_name = name_parent.transform.Find("ItemTxt").gameObject;
        Text txt = item_name.GetComponent<Text>();

        //������ ���� �����ͼ� price_txt�� ����
        GameObject price_parent = this.transform.Find("BtnPrefab").gameObject;
        GameObject item_price = price_parent.transform.Find("Text").gameObject;
        Text price_txt = item_price.GetComponent<Text>();

        //todo: ���� �г� ��ü ����
        GameObject canvas = GameObject.Find("Canvas");
        main_ui = canvas.transform.Find("mainUI").gameObject;
        GameObject child = Instantiate(StoreBuyPanel, main_ui.transform);

        buy_suc_panel = canvas.transform.Find("StoreBuySucPanel").gameObject;

        GameObject Asset_StorePopup = child.transform.Find("Asset_StorePopup").gameObject;

        //������ �̸� ����
        GameObject buy_item_name = Asset_StorePopup.transform.Find("ItemName").gameObject;
        Text buy_txt = buy_item_name.GetComponent<Text>();
        buy_txt.text = txt.text;

        //�� ���� ���� ����
        GameObject my_coin_parent = Asset_StorePopup.transform.Find("MyCoin").gameObject;
        GameObject my_coin = my_coin_parent.transform.Find("Text").gameObject;
        Text my_coin_txt = my_coin.GetComponent<Text>();
        my_coin_txt.text = PlayerPrefs.GetInt("Wallet").ToString(); //���� ���� ���� �� ����.

        //������ ���� ����
        GameObject item_cost_parent = Asset_StorePopup.transform.Find("ItemCost").gameObject;
        GameObject item_cost = item_cost_parent.transform.Find("Text").gameObject;
        Text item_cost_txt = item_cost.GetComponent<Text>();
        item_cost_txt.text = price_txt.text;

        this_cost = Int32.Parse(item_cost_txt.text);    //���� ������ ����

        //�ܾ� ����
        int result_cost = Convert.ToInt32(my_coin_txt.text) - Convert.ToInt32(item_cost_txt.text);

        GameObject change_cost_parent = Asset_StorePopup.transform.Find("ChangeCost").gameObject;
        GameObject change_cost = change_cost_parent.transform.Find("Text").gameObject;
        Text change_cost_txt = change_cost.GetComponent<Text>();
        change_cost_txt.text = result_cost.ToString();

        GameObject DisableBuyBtn = Asset_StorePopup.transform.Find("DisableBuyBtn").gameObject;
        if (result_cost >= 0) //���� ����>������ ����
        {
            change_cost_txt.color = Color.black;
            DisableBuyBtn.SetActive(false);
        }
        else
        {
            change_cost_txt.color = Color.red;
            DisableBuyBtn.SetActive(true);
        }
    }

    public void BuyItemBtn()
    {
        //Inventory ���̺� �ҷ��ͼ�, ���⿡ �ش��ϴ� �����۰� ��ġ�ϴ� �ڵ尡 ���� ��� ������ 1�������Ѽ� ������Ʈ
        
        Where where = new Where();
        where.Equal("ICode", iCode);
        var bro = Backend.GameData.GetMyData("INVENTORY", where);

        if (bro.IsSuccess() == false)
        {
            Debug.Log("��û ����");
        }
        else
        {
            JsonData rows = bro.GetReturnValuetoJSON()["rows"];
            //���� ��� ������ �� �߰�
            if (rows.Count <= 0)
            {
                Param param = new Param();
                param.Add("ICode", iCode);
                param.Add("Amount", 1);

                var insert_bro = Backend.GameData.Insert("INVENTORY", param);

                if (insert_bro.IsSuccess())
                {
                    Debug.Log("������ ���� �Ϸ�: " +iCode);
                    PlayInfoManager.GetCoin(-this_cost);
                    CancleBtn();
                    buy_suc_panel.SetActive(true);
                    Gagu_Category.instance.PopGaguStore();  //������̾��� ��� ������Ʈ �ʿ�
                }
                else
                {
                    Debug.Log("������ ���� ����");
                }
            }
            //���� ��� �ش� ������ indateã��, ���� ����
            else
            {
                string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

                int item_amount = (int)bro.FlattenRows()[0]["Amount"];
                item_amount++;
                Debug.Log(item_amount);

                Param param = new Param();
                param.Add("ICode", iCode);
                param.Add("Amount", item_amount);

                var update_bro = Backend.GameData.UpdateV2("INVENTORY", rowIndate, Backend.UserInDate, param);
                if (update_bro.IsSuccess())
                {
                    Debug.Log("������ ���� �Ϸ�: " + iCode);
                    PlayInfoManager.GetCoin(-this_cost);
                    CancleBtn();
                    buy_suc_panel.SetActive(true);
                    
                }
                else
                {
                    Debug.Log("������ ���� ����");
                }
            }
        }
    }
    //�ǻ� ������ ����
    public void BuyClothesBtn()
    {
        Param param = new Param();
        param.Add("ICode", iCode);

        var insert_bro = Backend.GameData.Insert("ACC_CLOSET", param);

        if (insert_bro.IsSuccess())
        {
            Debug.Log("������ ���� �Ϸ�: " + iCode);
            PlayInfoManager.GetCoin(-this_cost);
            CancleClothesBtn();
            buy_suc_panel.SetActive(true);
            Clothes_Category.instance.PopClothesStore();
        }
        else
        {
            Debug.Log("������ ���� ����");
        }
    }

    //�ǻ� ������ ����
    public void BuyCustomBtn()
    {
        Param param = new Param();
        param.Add("ICode", iCode);

        var insert_bro = Backend.GameData.Insert("ACC_CUSTOM", param);

        if (insert_bro.IsSuccess())
        {
            Debug.Log("������ ���� �Ϸ�: " + iCode);
            PlayInfoManager.GetCoin(-this_cost);
            CancleCustomBtn();
            buy_suc_panel.SetActive(true);
            Custom_Category.instance.PopCustomStore();
        }
        else
        {
            Debug.Log("������ ���� ����");
        }
    }

    public void BuyUpgradeBtn()
    {
        int new_house_lv = PlayerPrefs.GetInt("HouseLv");
        new_house_lv++;
        PlayerPrefs.SetInt("HouseLv", new_house_lv);
        PlayInfoManager.GetCoin(-this_cost);

        if (new_house_lv == 2)
        {
            BadgeManager.GetBadge("B11");
        }
        else if( new_house_lv == 3)
        {
            BadgeManager.GetBadge("B12");
        }

        CancleUpgradeBtn();
        buy_suc_panel.SetActive(true);
        Gagu_Category.instance.ChkUpgrade();
    }


    public void CancleBtn()
    {
        GameObject panel = main_ui.transform.Find("StoreBuyPanel(Clone)").gameObject;
        Destroy(panel);
    }
    public void CancleClothesBtn()
    {
        GameObject panel = main_ui.transform.Find("ClothesBuyPanel(Clone)").gameObject;
        Destroy(panel);
    }
    public void CancleCustomBtn()
    {
        GameObject panel = main_ui.transform.Find("CustomBuyPanel(Clone)").gameObject;
        Destroy(panel);
    }
    public void CancleUpgradeBtn()
    {
        GameObject panel = main_ui.transform.Find("UpgradeBuyPanel(Clone)").gameObject;
        Destroy(panel);
    }
}
