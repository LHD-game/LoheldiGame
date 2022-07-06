using BackEnd;
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameManager : MonoBehaviour
{
    public static int Money;
    public static int Level;
    public static float NowExp;
    public static float MaxExp;
    private float tempNowExp;
    private float tempMoney;

    public Text moneyText;
    public Text LevelText;
    public Text conditionLevelText;                //상태창 레벨
    public Text NowExpBarleftText;                    //필요한 경험치량
    public Text NowExpBarrightText;                   //현재 경험치량
    public Slider slider;
    public Slider conditionSlider;

    public GameObject SoundManager;

    void Start()
    {
        var bro = Backend.GameData.GetMyData("PLAY_INFO", new Where());
        JsonData rows = bro.GetReturnValuetoJSON()["rows"];

        if (bro.IsSuccess())
        {
            print("레벨 정보 있음");
            //var inDate = bro.Rows()[0]["inDate"]["S"].ToString();
            Level = int.Parse(bro.Rows()[0]["Level"]["N"].ToString());
            NowExp = float.Parse(bro.Rows()[0]["NowExp"]["N"].ToString());
            MaxExp = float.Parse(bro.Rows()[0]["MaxExp"]["N"].ToString());
            Money = int.Parse(bro.Rows()[0]["Wallet"]["N"].ToString());

        }
        else
        {
            if (bro.GetReturnValuetoJSON()["rows"].Count <= 0)
            {
                Level = 1;
                MaxExp = 100;
                NowExp = 0;
                Money = 10;
            }
            
        }
        
    }

    void Update()
    {

        moneyText.text = Money.ToString();
        NowExpBarleftText.text = NowExp.ToString("F0");                      //설명설명
        NowExpBarrightText.text = MaxExp.ToString("F0");                  //설명설명설명
        LevelText.text = Level.ToString();

        slider.maxValue = MaxExp;
        conditionSlider.maxValue = MaxExp;
        slider.value = NowExp;
        conditionSlider.value = NowExp;
        if (NowExp >= MaxExp)
        {
            SoundManager.GetComponent<SoundEffect>().Sound("LevelUp");
            LevelUp();
        }
        if (tempNowExp != NowExp || tempMoney != Money)
        {
            ParameterUpload();
        }
        tempNowExp = NowExp;
    }

    void LevelUp()
    {
        conditionSlider.value = 0;
        slider.value = 0;
        NowExp = NowExp - MaxExp;
        MaxExp = MaxExp * 1.2f;
        Level++;
        ParameterUpload();
    }

    void ParameterUpload()
    {
        var bro = Backend.GameData.GetMyData("USER_GAME_DATA", new Where(), 10);
        JsonData rows = bro.GetReturnValuetoJSON()["rows"];

        string indate = rows[0]["inDate"]["S"].ToString();

        Param param = new Param();
        param.Add("Level", Level);
        param.Add("MaxExp", MaxExp);
        param.Add("NowExp", NowExp);
        param.Add("Wallet", Money);
        var bro2 = Backend.GameData.UpdateV2("USER_GAME_DATA", indate, Backend.UserInDate, param);
        if (bro2.IsSuccess())
        {
            print("데이터 업로드 성공");
        }
    }
}
