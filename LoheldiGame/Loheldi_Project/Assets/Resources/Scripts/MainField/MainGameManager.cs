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
    public string HouseShape;

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
        StartCoroutine(NowTimeChk());
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
        HouseShape = PlayerPrefs.GetString("HouseShape");
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
        Destroy(HousePlace.transform.GetChild(0).gameObject);
        if (HouseShape == "")
            HouseShape = "Plane";
        Debug.Log(HouseLv);
        Debug.Log(HouseShape);
        switch (HouseLv)
        {
            case 1:
                switch (HouseShape)
                {
                    case "Plane":
                        Instantiate(Resources.Load<GameObject>("Prefabs/House/h0"), HousePlace.transform);
                        break;
                    case "Hanok":
                        Instantiate(Resources.Load<GameObject>("Prefabs/House/HANOK_01"), HousePlace.transform);
                        break;
                    case "RRok":
                        Instantiate(Resources.Load<GameObject>("Prefabs/House/R_HOME_01"), HousePlace.transform);
                        break;
                    case "Wood":
                        Instantiate(Resources.Load<GameObject>("Prefabs/House/th000"), HousePlace.transform);
                        break;
                }
                break;
            case 2:
                switch (HouseShape)
                {
                    case "Plane":
                        Instantiate(Resources.Load<GameObject>("Prefabs/House/h1"), HousePlace.transform);
                        break;
                    case "Hanok":
                        Instantiate(Resources.Load<GameObject>("Prefabs/House/HANOK_02"), HousePlace.transform);
                        break;
                    case "RRok":
                        Instantiate(Resources.Load<GameObject>("Prefabs/House/R_HOME_02"), HousePlace.transform);
                        break;
                    case "Wood":
                        Instantiate(Resources.Load<GameObject>("Prefabs/House/th001"), HousePlace.transform);
                        break;
                }
                break;
            case 3:
                switch (HouseShape)
                {
                    case "Plane":
                        Instantiate(Resources.Load<GameObject>("Prefabs/House/h2"), HousePlace.transform);
                        break;
                    case "Hanok":
                        Instantiate(Resources.Load<GameObject>("Prefabs/House/HANOK_03"), HousePlace.transform);
                        break;
                    case "RRok":
                        Instantiate(Resources.Load<GameObject>("Prefabs/House/R_HOME_03"), HousePlace.transform);
                        break;
                    case "Wood":
                        Instantiate(Resources.Load<GameObject>("Prefabs/House/th002"), HousePlace.transform);
                        break;
                }
                break;
            case 4:
                switch (HouseShape)
                {
                    case "Plane":
                        Instantiate(Resources.Load<GameObject>("Prefabs/House/h3"), HousePlace.transform);
                        break;
                    case "Hanok":
                        Instantiate(Resources.Load<GameObject>("Prefabs/House/HANOK_04"), HousePlace.transform);
                        break;
                    case "RRok":
                        Instantiate(Resources.Load<GameObject>("Prefabs/House/R_HOME_04"), HousePlace.transform);
                        break;
                    case "Wood":
                        Instantiate(Resources.Load<GameObject>("Prefabs/House/th003"), HousePlace.transform);
                        break;
                }
                break;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Logout()
    {
        //���� �α��� ���� ����
        PlayerPrefs.DeleteKey("ID");
        PlayerPrefs.DeleteKey("PW");
        //���� �α׾ƿ�
        var bro = Backend.BMember.Logout();
        if (bro.IsSuccess())
        {
            //�α׾ƿ� ����, ���� ȭ������ �̵�
            SceneLoader.instance.GotoWelcome();
        }
        else
        {
            Debug.Log("�α׾ƿ� ����");
        }
        
    }
}
