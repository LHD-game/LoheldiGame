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

    public void BuyBtn()
    {
        //todo: 해당버튼에서 아이템 이름, 사진, 가격 가져오기
        //GameObject item_img = this.transform.Find("ItemImg").gameObject;
        //Image img = item_img.GetComponent<Image>();

        //아이템 이름 가져와서 txt에 저장
        GameObject item_name = this.transform.Find("ItemName").gameObject;
        Text txt = item_name.GetComponent<Text>();
 
        //아이템 가격 가져와서 price_txt에 저장
        GameObject price_parent = this.transform.Find("CostImg").gameObject;
        GameObject item_price = price_parent.transform.Find("CostTxt").gameObject;
        Text price_txt = item_price.GetComponent<Text>();

        //todo: 구매 패널 객체 생성
        GameObject canvas = GameObject.Find("Canvas");
        main_ui = canvas.transform.Find("mainUI").gameObject;
        GameObject child = Instantiate(StoreBuyPanel, main_ui.transform);

        
        GameObject Asset_StorePopup = child.transform.Find("Asset_StorePopup").gameObject;

        //GameObject buy_item_img = BuyBg.transform.Find("ItemImg").gameObject;
        //Image buy_img = buy_item_img.GetComponent<Image>();
        //buy_img.sprite = img.sprite;

        //아이템 이름 띄우기
        GameObject buy_item_name = Asset_StorePopup.transform.Find("ItemName").gameObject;
        Text buy_txt = buy_item_name.GetComponent<Text>();
        buy_txt.text = txt.text;

        //내 보유 코인 띄우기
        GameObject my_coin_parent = Asset_StorePopup.transform.Find("MyCoin").gameObject;
        GameObject my_coin = my_coin_parent.transform.Find("Text").gameObject;
        Text my_coin_txt = my_coin.GetComponent<Text>();
        my_coin_txt.text = "80"; //todo: 임시 코인입니다. 

        //아이템 가격 띄우기
        GameObject item_cost_parent = Asset_StorePopup.transform.Find("ItemCost").gameObject;
        GameObject item_cost = item_cost_parent.transform.Find("Text").gameObject;
        Text item_cost_txt = item_cost.GetComponent<Text>();
        item_cost_txt.text = price_txt.text;

        //잔액 띄우기
        int result_cost = Convert.ToInt32(my_coin_txt.text) - Convert.ToInt32(item_cost_txt.text);

        GameObject change_cost_parent = Asset_StorePopup.transform.Find("ChangeCost").gameObject;
        GameObject change_cost = change_cost_parent.transform.Find("Text").gameObject;
        Text change_cost_txt = change_cost.GetComponent<Text>();
        change_cost_txt.text = result_cost.ToString();

        GameObject DisableBuyBtn = Asset_StorePopup.transform.Find("DisableBuyBtn").gameObject;
        if(result_cost >= 0) //보유 코인>아이템 가격
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

    public void CancleBtn()
    {
        
        GameObject panel = main_ui.transform.Find("StoreBuyPanel(Clone)").gameObject;
        print("삭제");
        Destroy(panel);
    }
}
