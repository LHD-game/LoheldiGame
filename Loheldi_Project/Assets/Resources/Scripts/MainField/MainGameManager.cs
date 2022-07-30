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
    public static MainGameManager SingletonInstance;

    [SerializeField]
    GameObject BikeBtn;
    [SerializeField]
    GameObject NineOClockPanel;
    void Start()
    {
        GetDailyHP();
        UpdateField();
        //StartCoroutine(NowTimeChk());
    }

    private void Awake()
    {
        SingletonInstance = this;
    }

    IEnumerator NowTimeChk()    //9 to 6 �ð� üũ �޼ҵ�
    {
        while (true)
        {
            DateTime now = DateTime.Now;
            Debug.Log(now.Hour);
            if (now.Hour >= 21 || now.Hour < 6)
            {
                NineOClockPanel.SetActive(true);
            }
            else
            {
                NineOClockPanel.SetActive(false);
            }
            yield return new WaitForSecondsRealtime(60.0f); //1min ���
        }

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
        //HP
        FieldHPTxt.text = PlayerPrefs.GetInt("HP") + " / 5";
        //����
        FieldLevelTxt.text = PlayerPrefs.GetInt("Level").ToString();
        //����ġ
        float now_exp = PlayerPrefs.GetFloat("NowExp");
        float max_exp = PlayerPrefs.GetFloat("MaxExp");
        FieldExpTxt.text = now_exp + " / " + max_exp;
        //����ġ �����̵� value - (���� ����ġ / �ִ� ����ġ) * 100 : �����
        ExpSlider.value = (now_exp / max_exp) * 100;

        string[] q_preg = PlayerPrefs.GetString("QuestPreg").Split('_');
        int front_q_preg = int.Parse(q_preg[0]);
        if(front_q_preg > 4)
        {
            BikeBtn.SetActive(true);
        }
        else
        {
            BikeBtn.SetActive(false);
        }
    }

    void GetDailyHP()   //���ڸ� �˻��Ͽ� hp�� 5 ����
    {
        //if(���� ���� != ������ ����� hp���� ����){
        // if(hp<5){
        //      hp = 5
        //      ������ ����� hp ���� ���� = ���� ����
        //  }
        //}

        DateTime today = DateTime.Today;
        int today_day = today.Day;
        int hp_day = PlayerPrefs.GetInt("LastHPTime");
        int now_hp = PlayerPrefs.GetInt("HP");
        if (today_day != hp_day)    //������ ���� ���ڿ��� ��¥�� �ٲ� ���¶��
        {
            if (now_hp < 5) //���� ������ hp ���� ���� 5 ���϶��
            {
                PlayerPrefs.SetInt("LastHPTime", today_day);
                PlayInfoManager.GetHP(5-now_hp);
                
                Debug.Log("��¥�� �ٲ�� hp�� ȸ���Ǿ����ϴ�.");
            }
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
