using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SubQuest : MonoBehaviour
{
    public GameObject Main_UI;
    public GameObject AppleTree;
    public GameObject AppleTreeOBJ;
    public TMP_InputField AppleTreeTxt;
    public GameObject ErrorWin;
    public Text ErrorWinTxt;
    public MainGameManager MainUI;

    [SerializeField]
    private ParticleSystem HeartFx;

    public void AppleTreeQ()
    {
        if(AppleTreeTxt.text.Length<10)
        {
            ErrorWin.SetActive(true);
            ErrorWinTxt.text = "감사했던 일을 조금만 더 자세히 적어봐요! \n <10글자 이상 적어주세요>";
        }
        else
        {
            AppleTreeSave();
        }
    }

    private void AppleTreeSave()
    {
        PlayInfoManager.GetExp(10);
        PlayInfoManager.GetCoin(10);
        MainUI.UpdateField();
        AppleTree.SetActive(false);
        Main_UI.SetActive(true);
        HeartFX(AppleTreeOBJ);
    }

    public void HeartFX(GameObject go)    //머리 반짝!하는 파티클
    {
        Debug.Log("반짝");
        ParticleSystem newfx = Instantiate(HeartFx);
        newfx.transform.position = go.transform.position + new Vector3(0, 7, -3);

        newfx.Play();
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
