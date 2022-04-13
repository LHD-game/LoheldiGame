using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class LodingTxt : MonoBehaviour
{
    public Text Txt;
    public Text name;
    public GameObject Arrow;
    public GameObject block;
    //private string[] TxtValue;
    int j;
    public GameObject Chat;
    string LoadTxt;
    List<Dictionary<string, object>> data_Dialog = new List<Dictionary<string, object>>();
    public string FileAdress= "Scripts/Quest/Dialog";

    public void NMD()
    {
        Chat.SetActive(true);
        j = 0;
        data_Dialog = CSVReader.Read(FileAdress);
        Line();

        
    }

    void Line()  //�ٳѱ�
    {
        if (j == data_Dialog.Count)
        {
            Chat.SetActive(false);
            Arrow.SetActive(false);
            Txt.text = " ";
            name.text = " ";
        }
        else
        {
            LoadTxt = data_Dialog[j]["dialog"].ToString();
            name.text = data_Dialog[j]["name"].ToString();
            StartCoroutine(_typing());
            Arrow.SetActive(false);
            block.SetActive(true);

        }

    }

    IEnumerator _typing()  //Ÿ���� ȿ��
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < LoadTxt.Length + 1; i++)
        {
            Txt.text = LoadTxt.Substring(0, i);
            yield return new WaitForSeconds(0.05f);
            block.SetActive(false);
            Arrow.SetActive(true);
        }
        j++;
    }
}

