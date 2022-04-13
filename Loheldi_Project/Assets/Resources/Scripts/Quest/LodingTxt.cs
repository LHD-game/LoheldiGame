using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class LodingTxt : MonoBehaviour
{
    public Text Txt;
    public Text name;
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

    void Line()  //줄넘김
    {
        if (j == data_Dialog.Count)
        {
            Chat.SetActive(false);
            Txt.text = " ";
            name.text = " ";
        }
        else
        {
            LoadTxt = data_Dialog[j]["dialog"].ToString();
            name.text = data_Dialog[j]["name"].ToString();
            StartCoroutine(_typing());

        }

    }

    IEnumerator _typing()  //타이핑 효과
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < LoadTxt.Length + 1; i++)
        {
            Txt.text = LoadTxt.Substring(0, i);
            yield return new WaitForSeconds(0.05f);
        }
        j++;
    }
    /*public void FirstQuest(string filePath)
    {
        
        string[] _value = new string[];
        string value = "";

        FileInfo fileInfo = new FileInfo(filePath);

        if (fileInfo.Exists)
        {
            StreamReader reader = new StreamReader(filePath);
            value = reader.ReadToEnd(); //파일 처음부터 끝까지 읽기
            reader.Close();
        }
    }*/
}

