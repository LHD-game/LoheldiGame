using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SubQuest : MonoBehaviour
{
    public GameObject Main_UI;
    public GameObject AppleTree;
    public TMP_InputField AppleTreeTxt;
    public GameObject ErrorWin;
    public Text ErrorWinTxt;
    public void AppleTreeQ()
    {
        if(AppleTreeTxt.text.Length<10)
        {
            ErrorWin.SetActive(true);
            ErrorWinTxt.text = "�����ߴ� ���� ���ݸ� �� �ڼ��� �������! \n <10���� �̻� �����ּ���>";
        }
        else
        {
            AppleTreeSave();
        }
    }

    private void AppleTreeSave()
    {
        AppleTree.SetActive(false);
        Main_UI.SetActive(true);
    }
    // Start is called before the first frame update
    /*void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/
}
