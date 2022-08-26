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

    public GameObject HousePlace;
    public int HouseLv;

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
        HouseLv = PlayerPrefs.GetInt("HouseLv");
        HouseChange();
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

    void HouseChange()
    {
        HousePlace.transform.GetChild(0).gameObject.SetActive(false);
        HousePlace.transform.GetChild(1).gameObject.SetActive(false);
        HousePlace.transform.GetChild(2).gameObject.SetActive(false);
        HousePlace.transform.GetChild(3).gameObject.SetActive(false);
        switch (HouseLv)
        {
            case 1:
                HousePlace.transform.GetChild(0).gameObject.SetActive(true);
                break;
            case 2:
                HousePlace.transform.GetChild(1).gameObject.SetActive(true);
                break;
            case 3:
                HousePlace.transform.GetChild(2).gameObject.SetActive(true);
                break;
            case 4:
                HousePlace.transform.GetChild(3).gameObject.SetActive(true);
                break;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
