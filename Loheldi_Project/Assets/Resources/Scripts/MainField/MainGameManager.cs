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
        UpdateField();
    }

    public void UpdateField()
    {
        UpdateFieldMyInfo();
    }

    //���� ����� ������ ui ���� ������Ʈ
    void UpdateFieldMyInfo()
    {
        //��ȭ
        FieldWalletTxt.text = PlayerPrefs.GetInt("Wallet").ToString();
        //����
        FieldLevelTxt.text = PlayerPrefs.GetInt("Level").ToString();
        //����ġ
        float now_exp = PlayerPrefs.GetFloat("NowExp");
        float max_exp = PlayerPrefs.GetFloat("MaxExp");
        FieldExpTxt.text = now_exp + " / " + max_exp;
        //����ġ �����̵� value - (���� ����ġ / �ִ� ����ġ) * 100 : �����
        ExpSlider.value = (now_exp / max_exp) * 100;
    }

    public void GetExpBtn()    //�ӽ� ����ġ ȹ�� ��ư �޼ҵ�
    {
        float exp_get = 10;

        PlayInfoManager.GetExp(exp_get);
        UpdateFieldMyInfo();
    }

    public void GetCoinBtn()    //�ӽ� ���� ȹ�� ��ư �޼ҵ�
    {
        int coin_get = 20;

        PlayInfoManager.GetCoin(coin_get);
        UpdateFieldMyInfo();
    }



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
                print("������ ������Ʈ ����");
            }
        }
    }*/
}
