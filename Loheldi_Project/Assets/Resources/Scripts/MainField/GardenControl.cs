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
    private GameObject c_seed;          //���� ī�װ�
    [SerializeField]
    private GameObject[] garden_ground = new GameObject[4]; //�Թ� ������Ʈ. �Թ��� ��: 4
    static GameObject[] garden_crops = new GameObject[4]; //�ɰ��� �۹� ������Ʈ ��ü.
    string[] g_seed = new string[4];    //�ɰ��� ������ ICode
    DateTime[] g_timer = new DateTime[4];    //�ɰ��� ������ ����ð�
    public static bool[] empty_ground = new bool[4];        //�� �Թ��� ��

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
        if (Interaction.GetComponent<Interaction>().Farm)   //jump ��ư���� farm�� ���� �� 
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

    void UpdateFieldGarden()     //�Թ��� ������ ������Ʈ�Ͽ� �ʵ忡 �ݿ�
    {
        //1. ���ÿ� �����ص� �Թ��� ������ �����´�.
        if (PlayerPrefs.GetString("G1") != "")
        {
            g_seed[0] = PlayerPrefs.GetString("G1");   //����ִٸ�, null�� �ƴ� ������ ����ֽ��ϴ�. ("" != null)
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

        for (int i = 0; i < g_seed.Length; i++)
        {
            //������ �ִ� �۹� ��ü�� �ִٸ� �����Ѵ�.
            Destroy(garden_crops[i]);
            //2. �� �Թ��� ��ġ�� ����Ѵ�. > ������ �ɾ��� �Թ��� ��ȣ�� �˱� ����
            if (g_seed[i].Equals(""))
            {
                empty_ground[i] = true;
            }
            else    //3. ������� ���� �Թ翡�� �ش� �ڵ��� �̸��� ���� �۹� �������� ��ü�� �����Ѵ�. "ICode_crops"
            {
                empty_ground[i] = false;
                garden_crops[i] = Instantiate(Resources.Load<GameObject>("Prefabs/Crops/Crops_GreenPlants")); //��ü ����: ����
                garden_crops[i].transform.SetParent(garden_ground[i].transform);   //�̸� �Թ� ������Ʈ�� ������ �ִ´�
                garden_crops[i].transform.GetChild(0).GetComponent<Text>().text = g_seed[i];    //icode�� �Է��صд�
                garden_crops[i].transform.GetChild(1).GetComponent<Text>().text = g_timer[i].ToString();    //����ð��� �Է��صд�
                garden_crops[i].transform.localPosition = new Vector3(0, 0, 0); //�۹� ��ü ��ġ �缳��
            }
        }
    }

    //todo: �ɱ� ���� ���� ��ư�� Ŭ���ϸ�, ���ÿ� ���Ѱ� ��¥�� �����Ѵ�. ���� UpdateFieldGarden() ���� �� todo3 �޼ҵ� ����
    public void GroundIsUpdated()
    {
        UpdateFieldGarden();
        SaveUserGarden();
    }

    //todo: ��Ȯ�ϱ� ��ư�� �����. �̸� Ŭ���ϸ� 1�� �Թ���� ���ʷ� �۹� �ɰ��� ���θ� üũ ��, ��Ȯ�Ѵ�. ���� UpdateFieldGarden() ���� �� todo3 �޼ҵ� ����
    public void HarvestCrops(int i)
    {
        empty_ground[i] = true;
        switch (i)
        {
            case 0:
                PlayerPrefs.SetString("G1", "");
                PlayerPrefs.SetString("G1Time", "");
                break;
            case 1:
                PlayerPrefs.SetString("G2", "");
                PlayerPrefs.SetString("G2Time", "");
                break;
            case 2:
                PlayerPrefs.SetString("G3", "");
                PlayerPrefs.SetString("G3Time", "");
                break;
            case 3:
                PlayerPrefs.SetString("G4", "");
                PlayerPrefs.SetString("G4Time", "");
                break;
        }
        UpdateFieldGarden();
        SaveUserGarden();
    }

    //todo: 3. ������ update �޼ҵ�
    //���� �� play_info�� prefs�����ϴ� �޼ҵ�
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
            param.Add("G1", "");
            param.Add("G1Time", "");
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
            param.Add("G2", "");
            param.Add("G2Time", "");
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
            param.Add("G3", "");
            param.Add("G3Time", "");
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
            param.Add("G4", "");
            param.Add("G4Time", "");
        }


        //���� ���� row �˻�
        var bro = Backend.GameData.Get("USER_GARDEN", new Where());
        string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

        //�ش� row�� ���� update
        var bro2 = Backend.GameData.UpdateV2("USER_GARDEN", rowIndate, Backend.UserInDate, param);

        if (bro2.IsSuccess())
        {
            Debug.Log("SaveUserGarden ����. USER_GARDEN ������Ʈ �Ǿ����ϴ�.");
        }
        else
        {
            Debug.Log("SaveUserGarden ����.");
        }
    }
}
