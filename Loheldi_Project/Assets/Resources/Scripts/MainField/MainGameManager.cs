using BackEnd;
using LitJson;
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
        level = 1;
        Maxexp = 100;
        exp = 0;

        /*Param param = new Param();
        param.Add("level", level);
        param.Add("Maxexp", Maxexp);
        param.Add("exp", exp);
        var bro = Backend.GameData.Insert("USER_GAME_DATA", param);
        if (bro.IsSuccess())
        {
            print("동기 방식 데이터 입력 성공");
        }*/
        var bro = Backend.GameData.GetMyData("USER_GAME_DATA", new Where());
        JsonData rows = bro.GetReturnValuetoJSON()["rows"];
        if (bro.IsSuccess())
        {
            print("정보 있음");
            levelText.text = rows[0]["level"].ToString();
            /*expBarleftText.text = rows[0]["exp"].ToString();              
            expBarrightText.text = rows[0]["Maxexp"].ToString();*/
        }
        /*else
        {
            level = 1;
            Maxexp = 100;
            exp = 0;
            Param param = new Param();
            param.Add("level", level);
            param.Add("Maxexp", Maxexp);
            param.Add("exp", exp);
            var bro2 = Backend.GameData.Insert("USER_GAME_DATA", param);
            if (bro2.IsSuccess())
            {
                print("동기 방식 데이터 입력 성공");
            }
        }*/

        /*var bro2 = Backend.GameData.Insert("USER_GAME_DATA", param);
        if (bro2.IsSuccess())
        {
            print("동기 방식 데이터 입력 성공");
        }*/
        /*var bro = Backend.GameData.GetMyData("USER_GAME_DATA", new Where(), 10);
        JsonData rows = bro.GetReturnValuetoJSON()["rows"];
        if (bro.IsSuccess())
        {
            string level = rows[0]["level"]["S"].ToString();
            string Maxexp = rows[0]["Maxexp"]["S"].ToString();
            string exp = rows[0]["exp"]["S"].ToString();

            levelText.text = level.ToString();
                  

        }*/
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
            var bro = Backend.GameData.GetMyData("USER_GAME_DATA", new Where(), 10);
            JsonData rows = bro.GetReturnValuetoJSON()["rows"];

            string indate = rows[0]["inDate"]["S"].ToString();

            Param param = new Param();
            param.Add("newlevel", level);
            param.Add("newMaxexp", Maxexp);
            param.Add("newexp", exp);
            var bro2 = Backend.GameData.UpdateV2("USER_GAME_DATA", indate, Backend.UserInDate, param);
            if (bro2.IsSuccess())
            {
                print("동기 방식 데이터 입력 성공");
            }
        }
    }


    void LevelUp()
    {
        conditionSlider.value = 0;
        slider.value = 0;
        exp = exp - Maxexp;
        Maxexp = Maxexp * 1.2f;
        level++;
    }
}
