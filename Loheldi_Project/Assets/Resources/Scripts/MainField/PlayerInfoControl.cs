using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoControl : MonoBehaviour
{
    [SerializeField]
    Text PlayerNameTxt;
    [SerializeField]
    Text LevelTxt;
    [SerializeField]
    Text ExpTxt;
    [SerializeField]
    Slider ExpSlider;
    [SerializeField]
    Text BirthTxt;
    [SerializeField]
    Text QuestPregTxt;
    [SerializeField]
    Text WalletTxt;

    static bool is_get_b_chart = false; //이미 배지 차트 가져온 상태라면

    //내정보 창 열었을 때
    public void PopPlayerInfo()
    {
        UpdateInfoText();
        if (is_get_b_chart)
        {
            GetBadgeChart();
        }
    }

    void UpdateInfoText()
    {
        PlayerNameTxt.text = PlayerPrefs.GetString("Nickname");
        LevelTxt.text = PlayerPrefs.GetInt("Level").ToString();
        float now_exp = PlayerPrefs.GetFloat("NowExp");
        float max_exp = PlayerPrefs.GetFloat("MaxExp");
        ExpTxt.text = now_exp + " / " + max_exp;
        BirthTxt.text = "생년월일: " + PlayerPrefs.GetString("Birth");
        Debug.Log(PlayerPrefs.GetString("Birth"));
        QuestPregTxt.text = "건강도: " + PlayerPrefs.GetString("QuestPreg");
        WalletTxt.text = PlayerPrefs.GetInt("Wallet").ToString();

        ExpSlider.value = (now_exp / max_exp)*100;  //백분율로 변환
    }

    void GetBadgeChart()
    {

    }
}
