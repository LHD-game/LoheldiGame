using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonField
{
    //DB colmn name
    public static string nCID = "CID";
    public static string nName = "Name";
    public static string nModel = "Model";
    public static string nMeterial = "Meterial";
    public static string nTexture = "Texture";

    //DB model name
    public static string m_skin = "Skin";
    public static string m_eyes = "Eyes";
    public static string m_mouth = "Mouth";
    public static string m_hair = "Hair";

    //custom DB
    static List<Dictionary<string, object>> data_dialog; 

    public static void SetDataDialog(List<Dictionary<string, object>> list)
    {
        data_dialog = list;
    }
    public static List<Dictionary<string, object>> GetDataDialog()
    {
        return data_dialog;
    }


    //
}
