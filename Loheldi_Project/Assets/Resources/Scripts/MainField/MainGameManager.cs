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
    public static int QuestPreg;
    public static int LastQTime;
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
        BackendReturnObject bro = Backend.GameData.GetMyData("PLAY_INFO", new Where(), 10);
        var json = bro.GetReturnValuetoJSON();
        var json_data = json["rows"][0];
        ParsingJSON pj = new ParsingJSON();
        PlayInfo data = pj.ParseBackendData<PlayInfo>(json_data);

        Money = PlayerPrefs.GetInt("Wallet", data.Wallet);
        Level = PlayerPrefs.GetInt("Level", data.Level);
        NowExp = PlayerPrefs.GetFloat("NowExp", data.NowExp);
        MaxExp = PlayerPrefs.GetFloat("MaxExp", data.MaxExp);
        Debug.Log(data.MaxExp);
        //서버에 연동할때 쓰던것 1
        /*var bro = Backend.GameData.GetMyData("PLAY_INFO", new Where());
        JsonData rows = bro.GetReturnValuetoJSON()["rows"];

        QuestPreg = GameObject.Find("DontDestroyQuest").GetComponent<QuestDontDestroy>().QuestIndex;
        LastQTime = GameObject.Find("DontDestroyQuest").GetComponent<QuestDontDestroy>().LastDay;

        if (rows != null)
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
                ParameterUpload();
            }
            
        }*/
    }

    void Update()
    {
        moneyText.text = Money.ToString();
/*        NowExpBarleftText.text = NowExp.ToString("F0");                      //설명설명
        NowExpBarrightText.text = MaxExp.ToString("F0");                  //설명설명설명
        LevelText.text = Level.ToString();

        slider.maxValue = MaxExp;
        conditionSlider.maxValue = MaxExp;
        slider.value = NowExp;
        conditionSlider.value = NowExp;
        if (NowExp >= MaxExp)
        {
            SoundManager.GetComponent<SoundEffect>().Sound("LevelUp");
            //LevelUp();
            Debug.Log("LevelUP!");
        }
        if (tempNowExp != NowExp || tempMoney != Money)
        {
            //ParameterUpload();
            Debug.Log("ParameterUpdate");
        }
        tempNowExp = NowExp;*/
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
        Param param = new Param();
        param.Add("Level", Level);
        param.Add("QuestPreg", QuestPreg);
        param.Add("MaxExp", MaxExp);
        param.Add("NowExp", NowExp);
        param.Add("Wallet", Money);
        param.Add("LastQTime", LastQTime);

        var bro = Backend.GameData.Get("PLAY_INFO", new Where());
        JsonData rows = bro.GetReturnValuetoJSON()["rows"];

        if (rows != null)
        {
            Backend.GameData.Insert("PLAY_INFO", param);
        }
        else
        {
            var inDate = bro.Rows()[0]["inDate"]["S"].ToString();
            var bro2 = Backend.GameData.UpdateV2("PLAY_INFO", inDate, Backend.UserInDate, param);
            if (bro2.IsSuccess())
            {
                print("데이터 업데이트 성공");
            }
        }
    }
}
