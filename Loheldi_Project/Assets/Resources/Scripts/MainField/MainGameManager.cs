using BackEnd;
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameManager : MonoBehaviour
{
    public int money;
    public static int level;
    public static float exp;
    float Maxexp;

    public Text moneyText;
    public Text levelText;
    public Text conditionLevelText;                //상태창 레벨
    public Text expBarleftText;                    //필요한 경험치량
    public Text expBarrightText;                   //현재 경험치량
    public Slider slider;
    public Slider conditionSlider;

    public GameObject SoundManager;

    void Start()
    {
        //레벨 멈춰!
        level = 1;
        Maxexp = 100;
        exp = 0;


        var bro = Backend.GameData.GetMyData("USER_GAME_DATA", new Where());
        JsonData rows = bro.GetReturnValuetoJSON()["rows"];
       
            //var inDate = bro.Rows()[i]["inDate"]["S"].ToString();
            //var level = bro.Rows()[i]["level"]["S"].ToString();
        
            //Debug.Log(inDate);
        
        
        if (bro.IsSuccess())
        {
            /*level = bro.Rows()[0]["level"]["N"].ToString();*/
            /*Maxexp = 100;
            exp = 0;*/
            print("레벨 정보 있음");
            var level2 = rows[0]["level"]["N"].ToString();
            Debug.Log(level2);
            /*string level1 = (string)rows[0]["level"]["N"];*/
            //level = level2;
            levelText.text = level2;

        }
        else
        {
            if (bro.GetReturnValuetoJSON()["rows"].Count <= 0)
            {
                level = 1;
                Maxexp = 100;
                exp = 0;
            }
            
        }
        
    }

    void Update()
    {
        moneyText.text = money.ToString();
        expBarleftText.text = exp.ToString("F0");                      //설명설명
        expBarrightText.text = Maxexp.ToString("F0");                  //설명설명설명
        levelText.text = level.ToString();

        slider.maxValue = Maxexp;
        conditionSlider.maxValue = Maxexp;
        slider.value = exp;
        conditionSlider.value = exp;
        if (exp >= Maxexp)
        {
            SoundManager.GetComponent<SoundEffect>().Sound("LevelUp");
            LevelUp();
            
        }
    }


    void LevelUp()
    {
        conditionSlider.value = 0;
        slider.value = 0;
        exp = exp - Maxexp;
        Maxexp = Maxexp * 1.2f;
        level++;
        var bro = Backend.GameData.GetMyData("USER_GAME_DATA", new Where(), 10);
        JsonData rows = bro.GetReturnValuetoJSON()["rows"];

        string indate = rows[0]["inDate"]["S"].ToString();

        Param param = new Param();
        param.Add("level", level);
        /*param.Add("newMaxexp", Maxexp);
        param.Add("newexp", exp);*/
        var bro2 = Backend.GameData.UpdateV2("USER_GAME_DATA", indate, Backend.UserInDate, param);
        if (bro2.IsSuccess())
        {
            print("동기 방식 데이터 수정 성공");
        }
    }
}
