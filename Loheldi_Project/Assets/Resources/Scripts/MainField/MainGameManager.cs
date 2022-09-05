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
        //StartCoroutine(NowTimeChk());
    }

    private void Awake()
    {
        SingletonInstance = this;
    }

    IEnumerator NowTimeChk()    //9 to 6 시간 체크 메소드
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
            yield return new WaitForSecondsRealtime(60.0f); //1min 대기
        }

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
        HouseLv = PlayerPrefs.GetInt("HouseLv");
        HouseShape = PlayerPrefs.GetString("HouseShape");
        HouseChange();
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
        //로컬 로그인 정보 삭제
        PlayerPrefs.DeleteKey("ID");
        PlayerPrefs.DeleteKey("PW");
        //서버 로그아웃
        var bro = Backend.BMember.Logout();
        if (bro.IsSuccess())
        {
            //로그아웃 성공, 시작 화면으로 이동
            SceneLoader.instance.GotoWelcome();
        }
        else
        {
            Debug.Log("로그아웃 실패");
        }
        
    }
}
