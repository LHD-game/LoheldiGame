using BackEnd;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayInfoManager : MonoBehaviour
{
    //����ġ�� ���� �޼ҵ�
    public static void GetExp(float exp)
    {
        float now_exp = PlayerPrefs.GetFloat("NowExp");
        float max_exp = PlayerPrefs.GetFloat("MaxExp");
        int level = PlayerPrefs.GetInt("Level");

        //���� ����ġ += ���� ����ġ
        now_exp += exp;

        // over_exp = �ִ� ����ġ - ���� ����ġ
        float over_exp = max_exp - now_exp;

        //if (over_exp <= 0) --> ���� ���� += 1, ���� ����ġ = over_exp, �ִ� ����ġ += 20
        if(over_exp <= 0)
        {
            level += 1;
            now_exp = over_exp;
            max_exp += 20;      // ������ ������ �ִ� ����ġ�� 20 �����Ѵ�.
        }
        //prefs ����
        PlayerPrefs.SetInt("Level", level);
        PlayerPrefs.SetFloat("NowExp", now_exp);
        PlayerPrefs.SetFloat("MaxExp", max_exp);

        SavePlayInfo();
    }

    //��ȭ ���� �޼ҵ�
    public static void GetCoin(int coin)  //�������� ������ ���Ž�, coin�� ����. �̴ϰ���/����Ʈ ������ ������ ��� ��� coin�� ���
    {
        int wallet = PlayerPrefs.GetInt("Wallet");

        wallet += coin;

        PlayerPrefs.SetInt("Wallet", wallet);

        SavePlayInfo();
    }


    //���� �� play_info�� prefs�����ϴ� �޼ҵ�
    static void  SavePlayInfo()
    {
        int new_wallet = PlayerPrefs.GetInt("Wallet");
        int new_level = PlayerPrefs.GetInt("Level");
        float new_now_exp = PlayerPrefs.GetFloat("NowExp");
        float new_max_exp = PlayerPrefs.GetFloat("MaxExp");
        string new_quest_preg = PlayerPrefs.GetString("QuestPreg");
        int new_last_q_time = PlayerPrefs.GetInt("LastQTime");

        Param param = new Param();
        param.Add("Wallet", new_wallet);
        param.Add("Level", new_level);
        param.Add("NowExp", new_now_exp);
        param.Add("MaxExp", new_max_exp);
        param.Add("QuestPreg", new_quest_preg);
        param.Add("LastQTime", new_last_q_time);

        //���� ���� row �˻�
        var bro = Backend.GameData.Get("PLAY_INFO", new Where());
        string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

        //�ش� row�� ���� update
        var bro2 = Backend.GameData.UpdateV2("PLAY_INFO", rowIndate, Backend.UserInDate, param);

        if (bro2.IsSuccess())
        {
            Debug.Log("SavePlayInfo ����. PLAY_INFO�� ������Ʈ �Ǿ����ϴ�.");
        }
        else
        {
            Debug.Log("SavePlayInfo ����.");
        }
        
    }
}