using BackEnd;
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackEndChart : MonoBehaviour
{
    [Header("Gold")]
    public int usergold;
    public Text itemgold;
    private int itemnum;

    public void GetChartContents()  
    {
        Param param = new Param();
        Param updateParam = new Param();
        var BRO = Backend.Chart.GetChartContents("41919");
        
        if (ShopCategorySelect.Category == 1)
        {
            itemnum = (ShopCategorySelect.Page - 1) * 6 + ShopCategorySelect.buttonnum + 14;
        }
        if (ShopCategorySelect.Category == 2)
        {
            itemnum = (ShopCategorySelect.Page - 1) * 6 + ShopCategorySelect.buttonnum + 28;
        }
        if (ShopCategorySelect.Category == 3)
        {
            itemnum = (ShopCategorySelect.Page - 1) * 6 + ShopCategorySelect.buttonnum + 40;
        }
        if (ShopCategorySelect.Category == 4)
        {
            itemnum = (ShopCategorySelect.Page - 1) * 6 + ShopCategorySelect.buttonnum + 51;
        } // ��ư �ѹ��� ���� ��Ʈ �̾ƿ��� (1~73��)
        if (BRO.IsSuccess())
        {
            JsonData rows = BRO.GetReturnValuetoJSON()["rows"];
            Debug.Log("itemCode:" + rows[itemnum]["itemCode"][0]);
            Debug.Log("name:" + rows[itemnum]["name"][0]);
            Debug.Log("price:" + rows[itemnum]["price"][0]);
            Debug.Log("itemType:" + rows[itemnum]["itemType"][0]); // ��Ʈ�� �ִ� �÷� �� �޾ƿ���

            param.Add("itemCode", rows[itemnum]["itemCode"][0]);
            param.Add("item", rows[itemnum]["name"]);
            param.Add("price", rows[itemnum]["price"][0]); // �κ��丮 ���̺� ������ ������ ���� ����
            Backend.GameData.Insert("INVENTORY", param);

            /*updateParam.AddCalculation("price", GameInfoOperator.subtraction, usergold - int.Parse((string)rows[itemnum]["price"][0]));*/

            /*Where where = new Where();
            where.Equal("price", "95");

            Backend.GameData.UpdateWithCalculation("INVENTORY", where, updateParam);*/

        }
        
    }
}
