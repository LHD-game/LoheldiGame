using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParsingTest : MonoBehaviour
{
    List<Dictionary<string, object>> data_Dialog;
    List<Dictionary<string, object>> skin_Dialog;
    List<Dictionary<string, object>> eyes_Dialog;

    //DB �÷���
    string CID = "CID";
    string Name = "Name";
    string Model = "Model";
    string Meterial = "Meterial";
    string Texture = "Texture";

    //Model ��
    string m_skin = "Skin";

    void Start()
    {
        data_Dialog = CSVReader.Read("Customize/CustomDB");    //DB �Ľ�

        for(int i = 0; i < data_Dialog.Count; i++)
        {
            if (data_Dialog[i][Model].ToString().Equals(m_skin))
            {
                initSkin(data_Dialog[i]);
            }
            Debug.Log(data_Dialog[i][Name].ToString());
            Debug.Log(data_Dialog[i][Texture].ToString());
        }
    }

    void initSkin(Dictionary<string, object> d)
    {
        skin_Dialog.Add(d);
    }


}
