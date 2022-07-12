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
