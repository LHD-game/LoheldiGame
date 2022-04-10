using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class LodingTxt : MonoBehaviour
{
    public Text Txt;
    private string[] TxtValue;
    int j;
    public GameObject Chat;
    string flieadress = "test.txt";

    public void NMD()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, flieadress); //파일 불러오기
        FirstQuest(filePath);
    }
    public void FirstQuest(string filePath)
    {
        j = 0;
        string[] _value;
        string value = "";

        FileInfo fileInfo = new FileInfo(filePath);

        if (fileInfo.Exists)
        {
            StreamReader reader = new StreamReader(filePath);
            value = reader.ReadToEnd(); //파일 처음부터 끝까지 읽기
            reader.Close();
        }
        _value = value.Split('\n');
        TxtValue = _value;
        Line();

    }

    public void Line()  //줄넘김
    {
        if (j==TxtValue.Length)
        {
            Chat.SetActive(false);
            Txt.text = " ";
        }
        else
        {
            StartCoroutine(_typing());
        }
            
    }

    IEnumerator _typing()  //타이핑 효과
    {
        yield return new WaitForSeconds(0.5f);
        for (int i=0;i<TxtValue[j].Length+1;i++)
        {
            
            Txt.text = TxtValue[j].Substring(0,i);
            yield return new WaitForSeconds(0.05f);
        }
        Debug.Log(TxtValue[j].Length);
        j++;
    }
    
}
