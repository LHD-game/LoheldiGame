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

    static bool is_get_b_chart = false; //�̹� ���� ��Ʈ ������ ���¶��

    //������ â ������ ��
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
        BirthTxt.text = "�������: " + PlayerPrefs.GetString("Birth");
        Debug.Log(PlayerPrefs.GetString("Birth"));
        QuestPregTxt.text = "�ǰ���: " + PlayerPrefs.GetString("QuestPreg");
        WalletTxt.text = PlayerPrefs.GetInt("Wallet").ToString();

        ExpSlider.value = (now_exp / max_exp)*100;  //������� ��ȯ
    }

    void GetBadgeChart()
    {

    }
}
