using BackEnd;
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameManager : MonoBehaviour
{

    public Text FieldWalletTxt;
    public Text FieldLevelTxt;
    public Text FieldExpTxt;
    public Slider ExpSlider;

    public GameObject SoundManager;

    void Start()
    {
        UpdateFieldMyInfo();
    }

    //좌측 상단의 내정보 ui 정보 업데이트
    void UpdateFieldMyInfo()
    {
        //재화
        FieldWalletTxt.text = PlayerPrefs.GetInt("Wallet").ToString();
        //레벨
        FieldLevelTxt.text = PlayerPrefs.GetInt("Level").ToString();
        //경험치
        float now_exp = PlayerPrefs.GetFloat("NowExp");
        float max_exp = PlayerPrefs.GetFloat("MaxExp");
        FieldExpTxt.text = now_exp + " / " + max_exp;
        //경험치 슬라이드 value - (현재 경험치 / 최대 경험치) * 100 : 백분율
        ExpSlider.value = (now_exp / max_exp) * 100;
    }

/*    void LevelUp()
    {
        conditionSlider.value = 0;
        slider.value = 0;
        NowExp = NowExp - MaxExp;
        MaxExp = MaxExp * 1.2f;
        Level++;
        ParameterUpload();
    }*/

    /*void ParameterUpload()
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
    }*/
}
