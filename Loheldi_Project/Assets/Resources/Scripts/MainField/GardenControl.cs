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
    private GameObject c_seed;          //씨앗 카테고리
    [SerializeField]
    private GameObject[] garden_ground = new GameObject[4]; //텃밭 오브젝트. 텃밭의 수: 4
    static GameObject[] garden_crops = new GameObject[4]; //심겨진 작물 오브젝트 객체.
    string[] g_seed = new string[4];    //심겨진 씨앗의 ICode
    public static bool[] empty_ground = new bool[4];        //빈 텃밭의 수

    public GameObject Interaction;

    public Camera getCamera;
    public GameObject Farms;
    public static string FarmnumtoBackend;

    public static bool is_pop_garden = false;


    private void Start()
    {
        UpdateFieldGarden();
    }

    void Update()
    {
        if (Interaction.GetComponent<Interaction>().Farm)   //jump 버튼으로 farm에 진입 시 
        {
            if (!is_pop_garden)
            {
                GardenCategory gc = new GardenCategory();
                gc.PopGarden(c_seed);
                is_pop_garden = true;
            }
        }
        else
        {
            is_pop_garden = false;
        }
    }

    void UpdateFieldGarden()     //텃밭의 정보를 업데이트하여 필드에 반영
    {
        //1. 로컬에 저장해둔 텃밭의 정보를 가져온다.
        g_seed[0] = PlayerPrefs.GetString("G1");   //비어있다면, null이 아닌 공백이 들어있습니다. ("" != null)
        g_seed[1] = PlayerPrefs.GetString("G2");
        g_seed[2] = PlayerPrefs.GetString("G3");
        g_seed[3] = PlayerPrefs.GetString("G4");
        
        for (int i = 0; i < g_seed.Length; i++)
        {
            //기존에 있던 작물 객체가 있다면 삭제한다.
            Destroy(garden_crops[i]);
            //2. 빈 텃밭의 위치를 계산한다. > 새로이 심어질 텃밭의 번호를 알기 위함
            if (g_seed[i].Equals(""))
            {
                empty_ground[i] = true;
            }
            else    //3. 비어있지 않은 텃밭에는 해당 코드의 이름을 가진 작물 프리펩을 객체로 생성한다. "ICode_crops"
            {
                empty_ground[i] = false;
                garden_crops[i] = Instantiate(Resources.Load<GameObject>("Prefabs/Crops/Crops_GreenPlants")); //객체 생성: 새싹
                garden_crops[i].transform.SetParent(garden_ground[i].transform);   //이를 텃밭 오브젝트에 하위로 넣는다
                garden_crops[i].transform.GetChild(0).GetComponent<Text>().text = g_seed[i];    //icode를 입력해둔다?
                garden_crops[i].transform.localPosition = new Vector3(0, 0, 0); //작물 객체 위치 재설정
            }
        }
    }

    //todo: 심기 위해 씨앗 버튼을 클릭하면, 로컬에 씨앗과 날짜를 저장한다. 이후 UpdateFieldGarden() 실행 및 todo3 메소드 실행
    public void GroundIsUpdated()
    {
        UpdateFieldGarden();
        SaveUserGarden();
    }

    //todo: 수확하기 버튼을 만든다. 이를 클릭하면 1번 텃밭부터 차례로 작물 심겨짐 여부를 체크 후, 수확한다. 이후 UpdateFieldGarden() 실행 및 todo3 메소드 실행
    public void HarvestCrops()
    {
        //작물 심겨짐 여부 체크
        //해당 작물 수확 가능 여부 체크
        //수확 메소드 > 로컬을 빈값으로 바꾼 뒤, 보상을 제공
        UpdateFieldGarden();
        SaveUserGarden();
    }

    //todo: 3. 서버에 update 메소드
    //서버 상 play_info에 prefs저장하는 메소드
    void SaveUserGarden()
    {
        string G1 = PlayerPrefs.GetString("G1");
        DateTime G1Time = DateTime.Parse(PlayerPrefs.GetString("G1Time"));
        string G2 = PlayerPrefs.GetString("G2");
        DateTime G2Time = DateTime.Parse(PlayerPrefs.GetString("G2Time"));
        string G3 = PlayerPrefs.GetString("G3");
        DateTime G3Time = DateTime.Parse(PlayerPrefs.GetString("G3Time"));
        string G4 = PlayerPrefs.GetString("G4");
        DateTime G4Time = DateTime.Parse(PlayerPrefs.GetString("G4Time"));

        Param param = new Param();
        param.Add("G1", G1);
        param.Add("G1Time", G1Time);
        param.Add("G2", G2);
        param.Add("G2Time", G2Time);
        param.Add("G3", G3);
        param.Add("G3Time", G3Time);
        param.Add("G4", G4);
        param.Add("G4Time", G4Time);

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

}
