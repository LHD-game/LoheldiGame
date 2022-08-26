using BackEnd;
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GardenControl : MonoBehaviour
{
    private static GardenControl _instance;
    public static GardenControl instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GardenControl>();
            }
            return _instance;
        }
    }

    [SerializeField]
    GameObject FarmUI;          
    [SerializeField]
    GameObject c_seed;          //씨앗 카테고리
    [SerializeField]
    GameObject crops_sell_panel;    //작물 판매 보상 패널
    [SerializeField]
    GameObject[] garden_ground = new GameObject[4]; //텃밭 오브젝트. 텃밭의 수: 4
    public GameObject[] garden_crops = new GameObject[4]; //심겨진 작물 오브젝트 객체.
    string[] g_seed = new string[4];    //심겨진 씨앗의 ICode
    DateTime[] g_timer = new DateTime[4];    //심겨진 씨앗의 성장시간
    public static bool[] empty_ground = new bool[4];        //빈 텃밭의 수
    public static bool[] is_grown = new bool[4];        //수확 가능한 텃밭인지 여부
    public String Tree;

    public GameObject Interaction;
    public Camera getCamera;
    public string TreeCode;
    public GameObject Farms;
    public GameObject TreePlace;
    public GameObject TreeObject;



    private void Start()
    {
        for(int i = 0; i<4; i++)
        {
            empty_ground[i] = true;
            is_grown[i] = false;
        }
        UpdateFieldGarden();    //1. 로컬에서 심은 시간과 심은 작물의 정보를 가져온다.(*4)
        StartCoroutine(GrowTimeCorutine()); //2. 시간 검사하는 메소드 4번 실행(을 코루틴으로 1분마다 반복)
    }

    IEnumerator GrowTimeCorutine()
    {
        while (true)
        {
            for(int i = 0; i < 4; i++)
            {
                if (!empty_ground[i])   //빈 텃밭이 아니라면,
                {
                    GrowTimeChk(i); //시간 충족 여부 체크
                }
            }
            yield return new WaitForSecondsRealtime(30f);
        }
    }

    void GrowTimeChk(int garden_num) //3.시간이 충족된 작물은 모델링을 변경하고, 스위치를 켜준다.
    {
        if (!is_grown[garden_num])  //이미 수확가능한 작물인 것은 아닌지 체크.
        {
            DateTime now = DateTime.Now;
            DateTime end_time = g_timer[garden_num].AddHours(2);

            int compare = DateTime.Compare(now, end_time);  // t1<t2 --> -1 / t1 == t2 --> 0 / t1 > t2 --> 1

            if (compare >= 0) //end_time과 같거나 end_time보다 나중이다 (now >= end_time)
            {
                //수확할 수 있다.
                is_grown[garden_num] = true;    //수확가능 스위치 true로 변경
                Destroy(garden_crops[garden_num]);  //기존에 존재하던 작물(새싹)객체 삭제
                garden_crops[garden_num] = Instantiate(Resources.Load<GameObject>("Prefabs/Crops/" + g_seed[garden_num] + "_crops"));
                garden_crops[garden_num].transform.SetParent(garden_ground[garden_num].transform);
                garden_crops[garden_num].transform.localPosition = new Vector3(0, 0, 0); //작물 객체 위치 재설정
                Debug.Log("작물 다 자라서 객체 변화");
            }
        }
        Debug.Log("GrowTimeChk() 실행");
        return;
    }
    

    void UpdateFieldGarden()     //텃밭의 정보를 업데이트하여 필드에 반영
    {
        //1. 로컬에 저장해둔 텃밭의 정보를 가져온다.
        if (PlayerPrefs.GetString("G1") != "")
        {
            g_seed[0] = PlayerPrefs.GetString("G1");   //비어있다면, null이 아닌 공백이 들어있습니다. ("" != null)
            g_timer[0] = DateTime.ParseExact(PlayerPrefs.GetString("G1Time"), "yyyy-MM-dd tt h:mm", null);
        }
        else
            g_seed[0] = "";
        if (PlayerPrefs.GetString("G2") != "")
        {
            g_seed[1] = PlayerPrefs.GetString("G2");
            g_timer[1] = DateTime.ParseExact(PlayerPrefs.GetString("G2Time"), "yyyy-MM-dd tt h:mm", null);
        }
        else
            g_seed[1] = "";
        if (PlayerPrefs.GetString("G3") != "")
        {
            g_seed[2] = PlayerPrefs.GetString("G3");
            g_timer[2] = DateTime.ParseExact(PlayerPrefs.GetString("G3Time"), "yyyy-MM-dd tt h:mm", null);
        }
        else
            g_seed[2] = "";
        if (PlayerPrefs.GetString("G4") != "")
        {
            g_seed[3] = PlayerPrefs.GetString("G4");
            g_timer[3] = DateTime.ParseExact(PlayerPrefs.GetString("G4Time"), "yyyy-MM-dd tt h:mm", null);
        }
        else
            g_seed[3] = "";
        if (PlayerPrefs.GetString("Tree") != "")
        {
            Tree = PlayerPrefs.GetString("Tree");
        }
        else
            Tree = "";

        for (int i = 0; i < g_seed.Length; i++)
        {
            //기존에 있던 작물 객체가 있다면 삭제한다.
            //2. 빈 텃밭의 위치를 계산한다. > 새로이 심어질 텃밭의 번호를 알기 위함
            if (g_seed[i].Equals(""))
            {
                Destroy(garden_crops[i]);
                empty_ground[i] = true;
                is_grown[i] = false;
            }
            else    //3. 비어있지 않은 텃밭에는 해당 코드의 이름을 가진 작물 프리펩을 객체로 생성한다. "ICode_crops"
            {
                if (Farms.transform.GetChild(i).transform.childCount == 0)
                {
                    empty_ground[i] = false;
                    garden_crops[i] = Instantiate(Resources.Load<GameObject>("Prefabs/Crops/Spring")); //객체 생성: 새싹
                    garden_crops[i].transform.SetParent(garden_ground[i].transform);   //이를 텃밭 오브젝트에 하위로 넣는다
                    garden_crops[i].transform.GetChild(0).GetComponent<Text>().text = g_seed[i];    //icode를 입력해둔다
                    garden_crops[i].transform.GetChild(1).GetComponent<Text>().text = g_timer[i].ToString();    //성장시간을 입력해둔다
                    garden_crops[i].transform.localPosition = new Vector3(0, 0, 0); //작물 객체 위치 재설정
                }
            }
        }

        if (TreeCode.Equals(""))
        {
            Destroy(TreeObject);
        }
        else
        {
            TreeObject = Instantiate(Resources.Load<GameObject>("Prefabs/Crops/Spring"));        //일단 서버에서 블러와 생성하는것만 만듬.
            TreeObject.transform.SetParent(TreeObject.transform);
            TreeObject.transform.GetChild(0).GetComponent<Text>().text = TreeCode;               //icode를 입력해둔다
            TreeObject.transform.localPosition = new Vector3(0, 0, 0);                           //객체 위치 재설정
        }
    }

    //todo: 심기 위해 씨앗 버튼을 클릭하면, 로컬에 씨앗과 날짜를 저장한다. 이후 UpdateFieldGarden() 실행 및 todo3 메소드 실행
    public void GroundIsUpdated()
    {
        UpdateFieldGarden();
        SaveUserGarden();
    }

    //todo: 수확하기 버튼을 만든다. 이를 클릭하면 1번 텃밭부터 차례로 작물 심겨짐 여부를 체크 후, 수확한다. 이후 UpdateFieldGarden() 실행 및 todo3 메소드 실행
    public void HarvestCrops()  //실행 후 MainGameManager의 UpdateField() 실행 필요
    {
        for(int i = 0; i < 4; i++)
        {
            if (is_grown[i])    //수확할 수 있다면
            {
                empty_ground[i] = true;
                is_grown[i] = false;

                DateTime now = DateTime.Now;
                string now_t = now.ToString("g");

                switch (i)
                {
                    case 0:
                        PlayerPrefs.SetString("G1", "");
                        PlayerPrefs.SetString("G1Time", now_t);
                        break;
                    case 1:
                        PlayerPrefs.SetString("G2", "");
                        PlayerPrefs.SetString("G2Time", now_t);
                        break;
                    case 2:
                        PlayerPrefs.SetString("G3", "");
                        PlayerPrefs.SetString("G3Time", now_t);
                        break;
                    case 3:
                        PlayerPrefs.SetString("G4", "");
                        PlayerPrefs.SetString("G4Time", now_t);
                        break;
                    default:
                        break;
                }
                //todo: 수확 후 보상 & ui를 띄운다.
                System.Random rand = new System.Random();
                int rand_num = rand.Next(2, 5) * 5;
                PlayInfoManager.GetCoin(rand_num);    // 일단은 10 ~ 20코인 정도
                PopSellPanel(rand_num);
                
                UpdateFieldGarden();
                SaveUserGarden();
                break;
            }
        }
    }

    //3.서버에 update 메소드
    //서버 상 play_info에 prefs저장하는 메소드
    void SaveUserGarden()
    {
        Param param = new Param();
        if (PlayerPrefs.GetString("G1") != "")
        {
            string G1 = PlayerPrefs.GetString("G1");
            DateTime G1Time = DateTime.Parse(PlayerPrefs.GetString("G1Time"));
            param.Add("G1", G1);
            param.Add("G1Time", G1Time);
        }
        else
        {
            DateTime G1Time = DateTime.Parse(PlayerPrefs.GetString("G1Time"));
            param.Add("G1", "");
            param.Add("G1Time", G1Time);
        }
        if (PlayerPrefs.GetString("G2") != "")
        {
            string G2 = PlayerPrefs.GetString("G2");
            DateTime G2Time = DateTime.Parse(PlayerPrefs.GetString("G2Time"));
            param.Add("G2", G2);
            param.Add("G2Time", G2Time);
        }
        else
        {
            DateTime G2Time = DateTime.Parse(PlayerPrefs.GetString("G2Time"));
            param.Add("G2", "");
            param.Add("G2Time", G2Time);
        }
        if (PlayerPrefs.GetString("G3") != "")
        {
            string G3 = PlayerPrefs.GetString("G3");
            DateTime G3Time = DateTime.Parse(PlayerPrefs.GetString("G3Time"));
            param.Add("G3", G3);
            param.Add("G3Time", G3Time);
        }
        else
        {
            DateTime G3Time = DateTime.Parse(PlayerPrefs.GetString("G3Time"));
            param.Add("G3", "");
            param.Add("G3Time", G3Time);
        }
        if (PlayerPrefs.GetString("G4") != "")
        {
            string G4 = PlayerPrefs.GetString("G4");
            DateTime G4Time = DateTime.Parse(PlayerPrefs.GetString("G4Time"));
            param.Add("G4", G4);
            param.Add("G4Time", G4Time);
        }
        else
        {
            DateTime G4Time = DateTime.Parse(PlayerPrefs.GetString("G4Time"));
            param.Add("G4", "");
            param.Add("G4Time", G4Time);
        }
        if (PlayerPrefs.GetString("Tree") != "")
        {
            string Tree = PlayerPrefs.GetString("Tree");
            param.Add("Tree", Tree);
        }
        else
        {
            param.Add("Tree", "");
        }


        //유저 현재 row 검색
        var bro = Backend.GameData.Get("USER_GARDEN", new Where());
        string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

        //해당 row의 값을 update
        var bro2 = Backend.GameData.UpdateV2("USER_GARDEN", rowIndate, Backend.UserInDate, param);

        if (bro2.IsSuccess())
        {
            Debug.Log("SaveUserGarden 성공. USER_GARDEN 업데이트 되었습니다.");
        }
        else
        {
            Debug.Log("SaveUserGarden 실패.");
        }
    }

    void PopSellPanel(int get_coin)
    {
        crops_sell_panel.SetActive(true);
        GameObject parent = crops_sell_panel.transform.Find("Asset_StorePopup").gameObject;
        GameObject coin = parent.transform.Find("Coin").gameObject;
        Text coin_txt = coin.GetComponent<Text>();
        coin_txt.text = get_coin.ToString();
    }
}
