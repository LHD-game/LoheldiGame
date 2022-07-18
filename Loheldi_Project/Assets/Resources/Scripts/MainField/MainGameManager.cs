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
    public Text FieldHPTxt;
    public Text FieldLevelTxt;
    public Text FieldExpTxt;
    public Slider ExpSlider;

    public GameObject SoundManager;

    void Start()
    {
        GetDailyHP();
        UpdateField();
    }

    public void UpdateField()
    {
        UpdateFieldMyInfo();
    }

    //좌측 상단의 내정보 ui 정보 업데이트
    void UpdateFieldMyInfo()
    {
        //재화
        FieldWalletTxt.text = PlayerPrefs.GetInt("Wallet").ToString();
        //HP
        FieldHPTxt.text = PlayerPrefs.GetInt("HP") + " / 5";
        //레벨
        FieldLevelTxt.text = PlayerPrefs.GetInt("Level").ToString();
        //경험치
        float now_exp = PlayerPrefs.GetFloat("NowExp");
        float max_exp = PlayerPrefs.GetFloat("MaxExp");
        FieldExpTxt.text = now_exp + " / " + max_exp;
        //경험치 슬라이드 value - (현재 경험치 / 최대 경험치) * 100 : 백분율
        ExpSlider.value = (now_exp / max_exp) * 100;
    }

    public void GetExpBtn()    //임시 경험치 획득 버튼 메소드
    {
        float exp_get = 10;

        PlayInfoManager.GetExp(exp_get);
        UpdateFieldMyInfo();
    }

    public void GetCoinBtn()    //임시 코인 획득 버튼 메소드
    {
        int coin_get = 20;

        PlayInfoManager.GetCoin(coin_get);
        UpdateFieldMyInfo();
    }

    void GetDailyHP()   //일자를 검사하여 hp를 5 제공
    {
        //if(오늘 일자 != 서버에 저장된 hp수령 일자){
        // if(hp<5){
        //      hp = 5
        //      서버에 저장된 hp 수령 일자 = 오늘 일자
        //  }
        //}

        DateTime today = DateTime.Today;
        int today_day = today.Day;
        int hp_day = PlayerPrefs.GetInt("LastHPTime");
        int now_hp = PlayerPrefs.GetInt("HP");
        if (today_day != hp_day)    //마지막 수령 일자에서 날짜가 바뀐 상태라면
        {
            if (now_hp < 5) //또한 마지막 hp 소지 수가 5 이하라면
            {
                PlayerPrefs.SetInt("LastHPTime", today_day);
                PlayInfoManager.GetHP(5-now_hp);
                
                Debug.Log("날짜가 바뀌어 hp가 회복되었습니다.");
            }
        }
    }
}
