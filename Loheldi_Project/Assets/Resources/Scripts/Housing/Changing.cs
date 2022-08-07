using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Changing : MonoBehaviour
{
    public int NowFloor;
    public GameObject Player;
    public GameObject Camera;

    public GameObject F1;
    public GameObject F2;
    public GameObject F3;
    public GameObject F4;
    public GameObject F5;

    public GameObject TempObject;
    public GameObject BedPosition;
    public GameObject TablePosition;
    public GameObject ChairPosition;
    public GameObject ClosetPosition;
    public GameObject KitchenPosition;
    public Text TempItemCode;

    private bool Starting;

    public void Start()
    {
        Camera = GameObject.Find("housingCamera");
        F1 = GameObject.Find("1F");
        F2 = GameObject.Find("2F");
        //F3 = GameObject.Find("3F");
        //F4 = GameObject.Find("4F");
        //F5 = GameObject.Find("5F");

        Debug.Log(PlayerPrefs.GetInt("HouseLv"));
        NowFloor = PlayerPrefs.GetInt("HouseLv");  //�� Ȯ�� �ܰ�, ���߿� ������ ����

        if (NowFloor == 1)
        {
            Camera.transform.position = new Vector3(-21f, 5.5f, -4f);
            BedPosition = F1.transform.Find("BedPosition").gameObject;
            TablePosition = F1.transform.Find("TablePosition").gameObject;
            ChairPosition = F1.transform.Find("ChairPosition").gameObject;
            ClosetPosition = F1.transform.Find("ClosetPosition").gameObject;
            if (this.name == "HousingSystem")
            {
                Player.transform.position = new Vector3(-23.3f, -1.9f, -1f);
            }
        }
        else if (NowFloor == 2)
        {
            Camera.transform.position = new Vector3(-18.5f, 8.5f, -70f);
            BedPosition = F2.transform.Find("BedPosition").gameObject;
            TablePosition = F2.transform.Find("TablePosition").gameObject;
            ChairPosition = F2.transform.Find("ChairPosition").gameObject;
            ClosetPosition = F2.transform.Find("ClosetPosition").gameObject;
            KitchenPosition = F2.transform.Find("KitchenPosition").gameObject;
            if (this.name == "HousingSystem")
            {
                Player.transform.position = new Vector3(-22.5f, -1.8f, -67.5f);
            }
        }
        else if (NowFloor == 3)
        {
            //2�� ����
        }
        else if (NowFloor == 4)
        {
            //3�� ����
        }
        else if (NowFloor == 5)
        {
            //4�� ����
        }
    }
    public void FirstSetting()
    {
        Starting = true;
        Save_Basic.LoadUserHousing();

        TempItemCode = GameObject.Find("TempItemCodeforLoad").GetComponent<Text>();

        TempItemCode.text = PlayerPrefs.GetString("bed");
        if (TempItemCode.text != "")
            ButtonClick(TempItemCode);
        TempItemCode.text = PlayerPrefs.GetString("closet");
        if (TempItemCode.text != "")
            ButtonClick(TempItemCode);
        TempItemCode.text = PlayerPrefs.GetString("table");
        if (TempItemCode.text != "")
            ButtonClick(TempItemCode);
        TempItemCode.text = PlayerPrefs.GetString("chair");
        if (TempItemCode.text != "")
            ButtonClick(TempItemCode);
        TempItemCode.text = PlayerPrefs.GetString("kitchen");
        if (TempItemCode.text != "")
            ButtonClick(TempItemCode);
        //2~4�� ����
        Starting = false;
        Debug.Log("ó�� ���� ���� �Ϸ�");
    }

    public void ButtonClick(Text ItemCodeObject)
    {
        string ItemCode = ItemCodeObject.text;
        if (FurnitureChangeClick.CurrentFurniture.GetComponent<Text>().text == "bed" || Starting)
        {
            if (ItemCode == "2010101")            //ù��° �ɼ��� �����ߴٸ�
            {
                Destroy(BedPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Beds/Bed_W"), BedPosition.transform);  //�ش� �ּҿ� ������Ʈ�� �����ؼ� ������.
                TempObject.transform.SetParent(BedPosition.transform);                                                                //������Ʈ�� BedPosition�� Child�� ����.
            }
            else if (ItemCode == "2020101")       //�ι�° �ɼ��� �����ߴٸ�
            {
                Destroy(BedPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Beds/Bed_Single"), BedPosition.transform);  //�ش� �ּҿ� ������Ʈ�� �����ؼ� ������.
                TempObject.transform.SetParent(BedPosition.transform);                                                                //������Ʈ�� BedPosition�� Child�� ����.
            }
            else if (ItemCode == "2030101")       //����° �ɼ��� �����ߴٸ�
            {
                Destroy(BedPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Beds/Bed_K"), BedPosition.transform);      //�ش� �ּҿ� ������Ʈ�� �����ؼ� ������.
                TempObject.transform.SetParent(BedPosition.transform);                                                                //������Ʈ�� BedPosition�� Child�� ����.
            }
            else if (ItemCode == "8020401")       //�׹�° �ɼ��� �����ߴٸ�
            {
                Destroy(BedPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Beds/Bed_A"), BedPosition.transform);      //�ش� �ּҿ� ������Ʈ�� �����ؼ� ������.
                TempObject.transform.SetParent(BedPosition.transform);                                                                //������Ʈ�� BedPosition�� Child�� ����
            }
            Debug.Log("���� ���� �Ϸ�");
            if (!Starting)
            {
                Param param = new Param();
                param.Add("bed", ItemCode);

                //���� ���� row �˻�
                var bro = Backend.GameData.Get("USER_HOUSE", new Where());
                string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

                //�ش� row�� ���� update
                var bro2 = Backend.GameData.UpdateV2("USER_HOUSE", rowIndate, Backend.UserInDate, param);

                if (bro2.IsSuccess())
                {
                    Debug.Log(ItemCode);
                    Debug.Log("SaveUserHousing ����. USER_HOUSE ������Ʈ �Ǿ����ϴ�.");
                }
                else
                {
                    Debug.Log("SaveUserHousing ����.");
                }
            }
        }
                                                                //����
        if (FurnitureChangeClick.CurrentFurniture.GetComponent<Text>().text == "closet" || Starting)
        {
            if (ItemCode == "2010102")            //ù��° �ɼ��� �����ߴٸ�
            {
                Destroy(ClosetPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/FurnitureSets/Wardrobe_W"), ClosetPosition.transform);
                TempObject.transform.SetParent(ClosetPosition.transform);
            }
            else if (ItemCode == "2020102")       //�ι�° �ɼ��� �����ߴٸ�
            {
                Destroy(ClosetPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/FurnitureSets/wardrobe"), ClosetPosition.transform);
                TempObject.transform.SetParent(ClosetPosition.transform);
            }
            else if (ItemCode == "2030102")       //����° �ɼ��� �����ߴٸ�
            {
                Destroy(ClosetPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/FurnitureSets/Wardrobe_K"), ClosetPosition.transform);
                TempObject.transform.SetParent(ClosetPosition.transform);
            }
            else if (ItemCode == "2040101")       //�׹�° �ɼ��� �����ߴٸ�
            {
                Destroy(ClosetPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/FurnitureSets/Wardrobe_A"), ClosetPosition.transform);
                TempObject.transform.SetParent(ClosetPosition.transform);
            }
            Debug.Log("���� ���� �Ϸ�");
            if (!Starting)
            {
                Param param = new Param();
                param.Add("closet", ItemCode);

                //���� ���� row �˻�
                var bro = Backend.GameData.Get("USER_HOUSE", new Where());
                string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

                //�ش� row�� ���� update
                var bro2 = Backend.GameData.UpdateV2("USER_HOUSE", rowIndate, Backend.UserInDate, param);

                if (bro2.IsSuccess())
                {
                    Debug.Log(ItemCode);
                    Debug.Log("SaveUserHousing ����. USER_HOUSE ������Ʈ �Ǿ����ϴ�.");
                }
                else
                {
                    Debug.Log("SaveUserHousing ����.");
                }
            }
        }
                                                                //å��
        if (FurnitureChangeClick.CurrentFurniture.GetComponent<Text>().text == "table" || Starting)
        {
            if (ItemCode == "2010204")            //ù��° �ɼ��� �����ߴٸ�
            {
                Destroy(TablePosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Table/Table4_W"), TablePosition.transform);
                TempObject.transform.SetParent(TablePosition.transform);
            }
            else if (ItemCode == "2020204")       //�ι�° �ɼ��� �����ߴٸ�
            {
                Destroy(TablePosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Table/Table_2"), TablePosition.transform);
                TempObject.transform.SetParent(TablePosition.transform);
            }
            else if (ItemCode == "2030204")       //����° �ɼ��� �����ߴٸ�
            {
                Destroy(TablePosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Table/table2_K"), TablePosition.transform);
                TempObject.transform.SetParent(TablePosition.transform);
            }
            else if (ItemCode == "2040203")       //�׹�° �ɼ��� �����ߴٸ�
            {
                Destroy(TablePosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Table/Table_A"), TablePosition.transform);
                TempObject.transform.SetParent(TablePosition.transform);
            }
            Debug.Log("���� ���� �Ϸ�");
            if (!Starting)
            {
                Param param = new Param();
                param.Add("table", ItemCode);

                //���� ���� row �˻�
                var bro = Backend.GameData.Get("USER_HOUSE", new Where());
                string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

                //�ش� row�� ���� update
                var bro2 = Backend.GameData.UpdateV2("USER_HOUSE", rowIndate, Backend.UserInDate, param);

                if (bro2.IsSuccess())
                {
                    Debug.Log(ItemCode);
                    Debug.Log("SaveUserHousing ����. USER_HOUSE ������Ʈ �Ǿ����ϴ�.");
                }
                else
                {
                    Debug.Log("SaveUserHousing ����.");
                }
            }
        }
                                                                //����
        if (FurnitureChangeClick.CurrentFurniture.GetComponent<Text>().text == "chair" || Starting)
        {
            if (ItemCode == "2010205")            //ù��° �ɼ��� �����ߴٸ�
            {
                Destroy(ChairPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Chair/Chair2_W"), ChairPosition.transform);
                TempObject.transform.SetParent(ChairPosition.transform);
                if (NowFloor >= 1)
                {
                    Destroy(ChairPosition.transform.GetChild(0).gameObject);
                    TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Chair/Chair2_W"), ChairPosition.transform);
                    TempObject.transform.SetParent(ChairPosition.transform);
                    TempObject.transform.localPosition = new Vector3(0.8f, 0f, 1f);
                }

            }
            else if (ItemCode == "2020205")       //�ι�° �ɼ��� �����ߴٸ�
            {
                Destroy(ChairPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Chair/chair_2"), ChairPosition.transform);
                TempObject.transform.SetParent(ChairPosition.transform);
                if (NowFloor >= 1)
                {
                    Destroy(ChairPosition.transform.GetChild(0).gameObject);
                    TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Chair/chair_2"), ChairPosition.transform);
                    TempObject.transform.SetParent(ChairPosition.transform);
                    TempObject.transform.localPosition = new Vector3(0.8f, 0f, 1f);
                }
            }
            else if (ItemCode == "2030205")       //����° �ɼ��� �����ߴٸ�
            {
                Destroy(ChairPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Chair/Chair_K"), ChairPosition.transform);
                TempObject.transform.SetParent(ChairPosition.transform);
                if (NowFloor >= 1)
                {
                    Destroy(ChairPosition.transform.GetChild(0).gameObject);
                    TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Chair/Chair_K"), ChairPosition.transform);
                    TempObject.transform.SetParent(ChairPosition.transform);
                    TempObject.transform.localPosition = new Vector3(0.8f, 0f, 1f);
                }
            }
            else if (ItemCode == "2040204")       //�׹�° �ɼ��� �����ߴٸ�
            {
                Destroy(ChairPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Chair/Chair_A"), ChairPosition.transform);
                TempObject.transform.SetParent(ChairPosition.transform);
                if (NowFloor >= 1)
                {
                    Destroy(ChairPosition.transform.GetChild(0).gameObject);
                    TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Chair/Chair_A"), ChairPosition.transform);
                    TempObject.transform.SetParent(ChairPosition.transform);
                    TempObject.transform.localPosition = new Vector3(0.8f, 0f, 1f);
                }
            }
            Debug.Log("���� ���� �Ϸ�");
            if (!Starting)
            {
                Param param = new Param();
                param.Add("chair", ItemCode);

                //���� ���� row �˻�
                var bro = Backend.GameData.Get("USER_HOUSE", new Where());
                string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

                //�ش� row�� ���� update
                var bro2 = Backend.GameData.UpdateV2("USER_HOUSE", rowIndate, Backend.UserInDate, param);

                if (bro2.IsSuccess())
                {
                    Debug.Log(ItemCode);
                    Debug.Log("SaveUserHousing ����. USER_HOUSE ������Ʈ �Ǿ����ϴ�.");
                }
                else
                {
                    Debug.Log("SaveUserHousing ����.");
                }
            }
        }
                    //�ξ�
        if (FurnitureChangeClick.CurrentFurniture.GetComponent<Text>().text == "kitchen" || Starting)
        {
            if (ItemCode == "2010105")            //ù��° �ɼ��� �����ߴٸ�
            {
                Destroy(KitchenPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Kitchen/Kitchen_W"), KitchenPosition.transform);
                TempObject.transform.SetParent(KitchenPosition.transform);
            }
            else if (ItemCode == "2020105")       //�ι�° �ɼ��� �����ߴٸ�
            {
                Destroy(KitchenPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Kitchen/Kitchen_WB"), KitchenPosition.transform);
                TempObject.transform.SetParent(KitchenPosition.transform);
            }
            else if (ItemCode == "2030105")       //����° �ɼ��� �����ߴٸ�
            {
                Destroy(KitchenPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Kitchen/Kitchen_K"), KitchenPosition.transform);
                TempObject.transform.SetParent(KitchenPosition.transform);
            }
            else if (ItemCode == "2040104")       //�׹�° �ɼ��� �����ߴٸ�
            {
                Destroy(KitchenPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/FurnitureSets/Kitchen_A"), KitchenPosition.transform);
                TempObject.transform.SetParent(KitchenPosition.transform);
            }
            Debug.Log("���� ���� �Ϸ�");
            if (!Starting)
            {
                Param param = new Param();
                param.Add("kitchen", ItemCode);

                //���� ���� row �˻�
                var bro = Backend.GameData.Get("USER_HOUSE", new Where());
                string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

                //�ش� row�� ���� update
                var bro2 = Backend.GameData.UpdateV2("USER_HOUSE", rowIndate, Backend.UserInDate, param);

                if (bro2.IsSuccess())
                {
                    Debug.Log(ItemCode);
                    Debug.Log("SaveUserHousing ����. USER_HOUSE ������Ʈ �Ǿ����ϴ�.");
                }
                else
                {
                    Debug.Log("SaveUserHousing ����.");
                }
            }
        }
    }
}