using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonField
{
    //DB colmn name
    public static string nCID = "ICode";
    public static string nName = "IName";
    public static string nCategory = "Category";
    public static string nItemType = "ItemType";
    public static string nTexture = "Texture";

    //DB model name
    public static string m_skin = "skin";
    public static string m_eyes = "eyes";
    public static string m_mouth = "mouth";
    public static string m_hair = "hair";

    public static string m_upper = "upper";
    public static string m_lower = "lower";
    public static string m_socks = "socks";
    public static string m_shoes = "shoes";

    //custom DB
    static List<Dictionary<string, object>> data_dialog = new List<Dictionary<string, object>>();
    static int cnt = 0;

    public static void SetDataDialog(CustomStoreItem data)
    {
        data_dialog.Add(new Dictionary<string, object>());

        data_dialog[cnt].Add("ICode", data.ICode);
        data_dialog[cnt].Add("IName", data.IName);
        data_dialog[cnt].Add("Category", data.Category);
        data_dialog[cnt].Add("ItemType", data.ItemType);
        data_dialog[cnt].Add("Texture", data.Texture);
        cnt++;
    }
    public static List<Dictionary<string, object>> GetDataDialog()
    {
        return data_dialog;
    }

    public static void ResetCnt()
    {
        cnt = 0;
    }


    //
}
