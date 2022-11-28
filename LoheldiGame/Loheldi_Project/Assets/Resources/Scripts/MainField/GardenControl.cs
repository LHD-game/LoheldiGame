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
    GameObject c_seed;          //���� ī�װ�
    [SerializeField]
    GameObject crops_sell_panel;    //�۹� �Ǹ� ���� �г�
    [SerializeField]
    GameObject[] garden_ground = new GameObject[4]; //�Թ� ������Ʈ. �Թ��� ��: 4
    public GameObject[] garden_crops = new GameObject[4]; //�ɰ��� �۹� ������Ʈ ��ü.
    string[] g_seed = new string[4];    //�ɰ��� ������ ICode
    DateTime[] g_timer = new DateTime[4];    //�ɰ��� ������ ����ð�
    public static bool[] empty_ground = new bool[4];        //�� �Թ��� ��
    public static bool[] is_grown = new bool[4];        //��Ȯ ������ �Թ����� ����
    public String Tree;

    public GameObject Interaction;
    public Camera getCamera;
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
        UpdateFieldGarden();    //1. ���ÿ��� ���� �ð��� ���� �۹��� ������ �����´�.(*4)
        StartCoroutine(GrowTimeCorutine()); //2. �ð� �˻��ϴ� �޼ҵ� 4�� ����(�� �ڷ�ƾ���� 1�и��� �ݺ�)
        TreeInstantiate();
    }

    IEnumerator GrowTimeCorutine()
    {
        while (true)
        {
            for(int i = 0; i < 4; i++)
            {
                if (!empty_ground[i])   //�� �Թ��� �ƴ϶��,
                {
                    GrowTimeChk(i); //�ð� ���� ���� üũ
                }
            }
            yield return new WaitForSecondsRealtime(30f);
        }
    }

    void GrowTimeChk(int garden_num) //3.�ð��� ������ �۹��� �𵨸��� �����ϰ�, ����ġ�� ���ش�.
    {
        if (!is_grown[garden_num])  //�̹� ��Ȯ������ �۹��� ���� �ƴ��� üũ.
        {
            DateTime now = DateTime.Now;
            DateTime end_time = g_timer[garden_num].AddHours(2);

            int compare = DateTime.Compare(now, end_time);  // t1<t2 --> -1 / t1 == t2 --> 0 / t1 > t2 --> 1

            if (compare >= 0) //end_time�� ���ų� end_time���� �����̴� (now >= end_time)
            {
                //��Ȯ�� �� �ִ�.
                is_grown[garden_num] = true;    //��Ȯ���� ����ġ true�� ����
                Destroy(garden_crops[garden_num]);  //������ �����ϴ� �۹�(����)��ü ����
                garden_crops[garden_num] = Instantiate(Resources.Load<GameObject>("Prefabs/Crops/" + g_seed[garden_num] + "_crops"));
                garden_crops[garden_num].transform.SetParent(garden_ground[garden_num].transform);
                garden_crops[garden_num].transform.localPosition = new Vector3(0, 0, 0); //�۹� ��ü ��ġ �缳��
                Debug.Log("�۹� �� �ڶ� ��ü ��ȭ");
            }
        }
        Debug.Log("GrowTimeChk() ����");
        return;
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
        if (PlayerPrefs.GetString("Tree") != "")
            Tree = PlayerPrefs.GetString("Tree");
        else
            Tree = "";

        for (int i = 0; i < g_seed.Length; i++)
        {
            //������ �ִ� �۹� ��ü�� �ִٸ� �����Ѵ�.
            //2. �� �Թ��� ��ġ�� ����Ѵ�. > ������ �ɾ��� �Թ��� ��ȣ�� �˱� ����
            if (g_seed[i].Equals(""))
            {
                Destroy(garden_crops[i]);
                empty_ground[i] = true;
                is_grown[i] = false;
            }
            else    //3. ������� ���� �Թ翡�� �ش� �ڵ��� �̸��� ���� �۹� �������� ��ü�� �����Ѵ�. "ICode_crops"
            {
                if (Farms.transform.GetChild(i).transform.childCount == 0)
                {
                    empty_ground[i] = false;
                    garden_crops[i] = Instantiate(Resources.Load<GameObject>("Prefabs/Crops/Spring")); //��ü ����: ����
                    garden_crops[i].transform.SetParent(garden_ground[i].transform);   //�̸� �Թ� ������Ʈ�� ������ �ִ´�
                    garden_crops[i].transform.GetChild(0).GetComponent<Text>().text = g_seed[i];    //icode�� �Է��صд�
                    garden_crops[i].transform.GetChild(1).GetComponent<Text>().text = g_timer[i].ToString();    //����ð��� �Է��صд�
                    garden_crops[i].transform.localPosition = new Vector3(0, 0, 0); //�۹� ��ü ��ġ �缳��
                }
            }
        }
        TreeInstantiate();
    }

    //todo: �ɱ� ���� ���� ��ư�� Ŭ���ϸ�, ���ÿ� ���Ѱ� ��¥�� �����Ѵ�. ���� UpdateFieldGarden() ���� �� todo3 �޼ҵ� ����
    public void GroundIsUpdated()
    {
        UpdateFieldGarden();
        SaveUserGarden();
    }

    //todo: ��Ȯ�ϱ� ��ư�� �����. �̸� Ŭ���ϸ� 1�� �Թ���� ���ʷ� �۹� �ɰ��� ���θ� üũ ��, ��Ȯ�Ѵ�. ���� UpdateFieldGarden() ���� �� todo3 �޼ҵ� ����
    public void HarvestCrops()  //���� �� MainGameManager�� UpdateField() ���� �ʿ�
    {
        for(int i = 0; i < 4; i++)
        {
            if (is_grown[i])    //��Ȯ�� �� �ִٸ�
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
                //todo: ��Ȯ �� ���� & ui�� ����.
                System.Random rand = new System.Random();
                int rand_num = rand.Next(2, 5) * 5;
                PlayInfoManager.GetCoin(rand_num);    // �ϴ��� 10 ~ 20���� ����
                PopSellPanel(rand_num);
                
                UpdateFieldGarden();
                SaveUserGarden();
                break;
            }
        }
    }

    //3.������ update �޼ҵ�
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

    void PopSellPanel(int get_coin)
    {
        crops_sell_panel.SetActive(true);
        GameObject parent = crops_sell_panel.transform.Find("Asset_StorePopup").gameObject;
        GameObject coin = parent.transform.Find("Coin").gameObject;
        Text coin_txt = coin.GetComponent<Text>();
        coin_txt.text = get_coin.ToString();
    }

    void TreeInstantiate()
    {
        if (Tree != "")
        {
            if(TreePlace.transform.childCount != 0)
                Destroy(TreePlace.transform.GetChild(0).gameObject);
            TreeObject = Instantiate(Resources.Load<GameObject>("Prefabs/Crops/" + Tree + "_crops"));
            TreeObject.transform.SetParent(TreePlace.transform);
            TreeObject.transform.localPosition = new Vector3(0, 0, 0);                           //��ü ��ġ �缳��
        }
    }
}
