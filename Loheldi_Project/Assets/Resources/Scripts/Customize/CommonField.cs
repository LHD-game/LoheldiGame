using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonField
{
    //DB colmn name
    public static string nCID = "ICode";
    public static string nName = "IName";
    public static string nKorName = "KorName";
    public static string nModel = "Model";
    public static string nMeterial = "Meterial";
    public static string nTexture = "Texture";

    //DB model name
    public static string m_skin = "Skin";
    public static string m_eyes = "Eyes";
    public static string m_mouth = "Mouth";
    public static string m_hair = "Hair";

    public static string m_upper = "Upper";
    public static string m_lower = "Lower";
    public static string m_socks = "Socks";
    public static string m_shoes = "Shoes";

    //custom DB
    static List<Dictionary<string, object>> data_dialog = new List<Dictionary<string, object>>();
    static int cnt = 0;

    public static void SetDataDialog(CustomItem data)
    {
        data_dialog.Add(new Dictionary<string, object>());

        data_dialog[cnt].Add("ICode", data.ICode);
        data_dialog[cnt].Add("IName", data.IName);
        data_dialog[cnt].Add("Model", data.Model);
        data_dialog[cnt].Add("Material", data.Material);
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
