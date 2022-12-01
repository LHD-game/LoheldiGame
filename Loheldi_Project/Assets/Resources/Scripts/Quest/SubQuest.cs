using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using BackEnd;
using System;

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


    public void TimeCheck()
    {
        var bro = Backend.GameData.GetMyData("USER_SUBQUEST", new Where());

        if (bro.IsSuccess() == false)
        {
            Debug.Log("요청 실패");
            return;
        }
        int time = 0;
        if (bro.GetReturnValuetoJSON()["rows"].Count <= 0)
        {
            Param param = new Param();  // 새 객체 생성

            param.Add("LastThankTreeTime", time);    //객체에 값 추가

            Backend.GameData.Insert("USER_SUBQUEST", param);   //객체를 서버에 업로드
        }
        else
        {
            var json = bro.GetReturnValuetoJSON();
            var json_data = json["rows"][0];
            ParsingJSON pj = new ParsingJSON();
            MySubQuest data = pj.ParseBackendData<MySubQuest>(json_data);
            time = data.ThankTreeLastTime;
        }
        int nowTime = Int32.Parse(DateTime.Now.ToString("yyyyMMdd"));
        if (time == nowTime);
        

    }
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
        Invoke("HeartFX", 0.15f);
    }

    public void HeartFX()    //나무 하트 파티클
    {
        Debug.Log("반짝");
        ParticleSystem newfx = Instantiate(HeartFx);
        newfx.transform.position = AppleTreeOBJ.transform.position + new Vector3(0, 7, -3);

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
