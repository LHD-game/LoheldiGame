using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyItem : MonoBehaviour
{
    [SerializeField]
    private GameObject BuyItemPanel;


    public void BuyBtn()
    {
        //todo: 해당버튼에서 아이템 이름, 사진, 가격 가져오기
        GameObject item_img = this.transform.Find("ItemImg").gameObject;
        Image img = item_img.GetComponent<Image>();

        GameObject item_name = this.transform.Find("ItemName").gameObject;
        Text txt = item_name.GetComponent<Text>();
        //print(txt.text);

        GameObject price_parent = this.transform.Find("CostImg").gameObject;
        GameObject item_price = price_parent.transform.Find("CostTxt").gameObject;
        Text price_txt = item_price.GetComponent<Text>();

        //todo: 구매 패널 객체 생성
        GameObject canvas = GameObject.Find("Canvas");
        GameObject child = Instantiate(BuyItemPanel, canvas.transform);
        GameObject BuyBg = child.transform.Find("BuyBg").gameObject;

        GameObject buy_item_img = BuyBg.transform.Find("ItemImg").gameObject;
        Image buy_img = buy_item_img.GetComponent<Image>();
        buy_img.sprite = img.sprite;
        GameObject buy_item_name = buy_item_img.transform.Find("ItemName").gameObject;
        Text buy_txt = buy_item_name.GetComponent<Text>();
        buy_txt.text = txt.text;
        //print(buy_txt.text);
        GameObject buy_price_parent = BuyBg.transform.Find("BuyBtn").gameObject;
        GameObject buy_item_price = buy_price_parent.transform.Find("CostTxt").gameObject;
        Text buy_price_txt = buy_item_price.GetComponent<Text>();
        buy_price_txt.text = price_txt.text;
    }

    public void CancleBtn()
    {
        print("삭제");
        GameObject panel = GameObject.Find("BuyItemPanel(Clone)");
        Destroy(panel);
    }
}
