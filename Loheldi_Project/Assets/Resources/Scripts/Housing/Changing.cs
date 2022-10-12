using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Changing : MonoBehaviour
{
    public int NowFloor;

    public GameObject F1;
    public GameObject F11;
    public GameObject F2;
    public GameObject F3;
    public GameObject F31;
    public GameObject F4;

    public GameObject TempObject;
    public GameObject BedPosition;
    public GameObject ClosetPosition;
    public GameObject BookshelfPosition;
    public GameObject WallshelfPosition;
    public GameObject DeskPosition;
    public GameObject TablePosition;
    public GameObject CoffeeTablePosition;
    public GameObject SidetablePosition;
    public GameObject ChairPosition;
    public GameObject ChairPosition2;
    public GameObject ChairPosition3;
    public GameObject SunbedPosition;
    public GameObject SunbedPosition2;
    public GameObject KitchenPosition;
    public GameObject StandSinkPosition;
    public GameObject FridgePosition;
    public GameObject SofaPosition;

    public Text TempItemCode;

    private bool Starting;
    public int temp = 0;   //chair1,2  ,Desk1,2,3 �� �ߺ� ���� ��ȯ �����

    public void Start()
    {
        F1 = GameObject.Find("1F");
        F2 = GameObject.Find("2F");
        F3 = GameObject.Find("3F");
        F4 = GameObject.Find("4F");

        NowFloor = PlayerPrefs.GetInt("HouseLv");

        if (NowFloor >= 1)
        {
            BedPosition = F1.transform.Find("BedPosition").gameObject;
            TablePosition = F1.transform.Find("TablePosition").gameObject;
            ChairPosition = F1.transform.Find("ChairPosition").gameObject;
            ClosetPosition = F1.transform.Find("ClosetPosition").gameObject;
        }
        if (NowFloor >= 2)
        {
            ChairPosition2 = F2.transform.Find("ChairPosition2").gameObject;
            SidetablePosition = F2.transform.Find("SidetablePosition").gameObject;
            KitchenPosition = F2.transform.Find("KitchenPosition").gameObject;
            FridgePosition = F2.transform.Find("FridgePosition").gameObject;
            StandSinkPosition = F2.transform.Find("StandSinkPosition").gameObject;
            WallshelfPosition = F2.transform.Find("WallshelfPosition").gameObject;
        }
        if (NowFloor >= 3 && GameObject.Find("HousingSystem").GetComponent<HousingElevator>().upstair)
        {
            ChairPosition3 = F3.transform.Find("ChairPosition3").gameObject;
            DeskPosition = F3.transform.Find("DeskPosition").gameObject;
            BookshelfPosition = F3.transform.Find("BookshelfPosition").gameObject;
            SofaPosition = F3.transform.Find("SofaPosition").gameObject;
        }
        if (NowFloor >= 4 && GameObject.Find("HousingSystem").GetComponent<HousingElevator>().upstair)
        {
            CoffeeTablePosition = F4.transform.Find("CoffeetablePosition").gameObject;
            SunbedPosition = F4.transform.Find("SunbedPosition").gameObject;
            SunbedPosition2 = F4.transform.Find("SunbedPosition2").gameObject;
        }
    }
    public void FirstSetting()
    {
        Starting = true;
        Save_Basic.LoadUserHousing();
        Save_Basic.LoadUserHousing2();

        TempItemCode = GameObject.Find("TempItemCodeforLoad").GetComponent<Text>();

        TempItemCode.text = PlayerPrefs.GetString("bed");
        if (TempItemCode.text != "")
            ButtonClick(TempItemCode);
        TempItemCode.text = PlayerPrefs.GetString("closet");
        if (TempItemCode.text != "")
            ButtonClick(TempItemCode);
        TempItemCode.text = PlayerPrefs.GetString("bookshelf");
        if (TempItemCode.text != "")
            ButtonClick(TempItemCode);
        TempItemCode.text = PlayerPrefs.GetString("wallshelf");
        if (TempItemCode.text != "")
            ButtonClick(TempItemCode);
        TempItemCode.text = PlayerPrefs.GetString("desk");
        if (TempItemCode.text != "")
            ButtonClick(TempItemCode);
        TempItemCode.text = PlayerPrefs.GetString("table");
        if (TempItemCode.text != "")
            ButtonClick(TempItemCode);
        TempItemCode.text = PlayerPrefs.GetString("coffeetable");
        if (TempItemCode.text != "")
            ButtonClick(TempItemCode);
        TempItemCode.text = PlayerPrefs.GetString("sidetable");
        if (TempItemCode.text != "")
            ButtonClick(TempItemCode);
        TempItemCode.text = PlayerPrefs.GetString("chair");
        if (TempItemCode.text != "")
        {
            ButtonClick(TempItemCode);
        }
        temp = 1;
        TempItemCode.text = PlayerPrefs.GetString("chair2");
        if (TempItemCode.text != "")
        {
            ButtonClick(TempItemCode);
        }
        temp = 2;
        TempItemCode.text = PlayerPrefs.GetString("chair3");
        if (TempItemCode.text != "")
        {
            ButtonClick(TempItemCode);
        }
        temp = 3;
        TempItemCode.text = PlayerPrefs.GetString("sunbed");
        if (TempItemCode.text != "")
        {
            ButtonClick(TempItemCode);
        }
        temp = 4;
        TempItemCode.text = PlayerPrefs.GetString("sunbed2");
        if (TempItemCode.text != "")
        {
            ButtonClick(TempItemCode);
        }
        temp = 0;
        TempItemCode.text = PlayerPrefs.GetString("kitchen");
        if (TempItemCode.text != "")
            ButtonClick(TempItemCode);
        TempItemCode.text = PlayerPrefs.GetString("fridge");
        if (TempItemCode.text != "")
            ButtonClick(TempItemCode);
        TempItemCode.text = PlayerPrefs.GetString("standingsink");
        if (TempItemCode.text != "")
            ButtonClick(TempItemCode);
        TempItemCode.text = PlayerPrefs.GetString("sofa");
        if (TempItemCode.text != "")
            ButtonClick(TempItemCode);
        //2~4�� ����
        Starting = false;
        Debug.Log("ó�� ���� ���� �Ϸ�");
    }

    public void ButtonClick(Text ItemCodeObject)
    {
        string ItemCode = ItemCodeObject.text;

        Where where = new Where();
        if (FurnitureChangeClick.CurrentFurniture.transform.Find("ItemCode"))
        {
            string ICode = FurnitureChangeClick.CurrentFurniture.transform.Find("ItemCode").GetComponent<Text>().text;
            where.Equal("ICode", ICode);
            var broA = Backend.GameData.GetMyData("INVENTORY", where);
            string rowIndateA = broA.FlattenRows()[0]["inDate"].ToString();

            Param paramA = new Param();
            paramA.Add("ICode", ICode);

            var update_bro = Backend.GameData.UpdateV2("INVENTORY", rowIndateA, Backend.UserInDate, paramA);
        }
        else { }
        
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
        if (FurnitureChangeClick.CurrentFurniture.GetComponent<Text>().text == "table" || Starting)
        {
            if (ItemCode == "2010202")            //ù��° �ɼ��� �����ߴٸ�
            {
                Destroy(TablePosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Table/Table4_W"), TablePosition.transform);
                TempObject.transform.SetParent(TablePosition.transform);
            }
            else if (ItemCode == "2020202")       //�ι�° �ɼ��� �����ߴٸ�
            {
                Destroy(TablePosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Table/Table_4"), TablePosition.transform);
                TempObject.transform.SetParent(TablePosition.transform);
            }
            else if (ItemCode == "2030202")       //����° �ɼ��� �����ߴٸ�
            {
                Destroy(TablePosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Table/Table4_K"), TablePosition.transform);
                TempObject.transform.SetParent(TablePosition.transform);
            }
            else if (ItemCode == "2040201")       //�׹�° �ɼ��� �����ߴٸ�
            {
                Destroy(TablePosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Table/Table_A"), TablePosition.transform);
                TempObject.transform.SetParent(TablePosition.transform);
            }
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
        if (FurnitureChangeClick.CurrentFurniture.GetComponent<Text>().text == "coffeetable" || Starting)
        {
            if (ItemCode == "2010301")            //ù��° �ɼ��� �����ߴٸ�
            {
                Destroy(CoffeeTablePosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Coffeetable/table_W"), CoffeeTablePosition.transform);
                TempObject.transform.SetParent(CoffeeTablePosition.transform);
            }
            else if (ItemCode == "2020301")       //�ι�° �ɼ��� �����ߴٸ�
            {
                Destroy(CoffeeTablePosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Coffeetable/table"), CoffeeTablePosition.transform);
                TempObject.transform.SetParent(CoffeeTablePosition.transform);
            }
            else if (ItemCode == "8020302")       //����° �ɼ��� �����ߴٸ�
            {
                Destroy(CoffeeTablePosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Coffeetable/table_k"), CoffeeTablePosition.transform);
                TempObject.transform.SetParent(CoffeeTablePosition.transform);
            }
            else if (ItemCode == "8020403")       //�׹�° �ɼ��� �����ߴٸ�
            {
                Destroy(CoffeeTablePosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Coffeetable/Table_A"), CoffeeTablePosition.transform);
                TempObject.transform.SetParent(CoffeeTablePosition.transform);
            }
            if (!Starting)
            {
                Param param = new Param();
                param.Add("coffeetable", ItemCode);

                //���� ���� row �˻�
                var bro = Backend.GameData.Get("USER_HOUSE2F", new Where());
                string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

                //�ش� row�� ���� update
                var bro2 = Backend.GameData.UpdateV2("USER_HOUSE2F", rowIndate, Backend.UserInDate, param);

                if (bro2.IsSuccess())
                {
                    Debug.Log(ItemCode);
                    Debug.Log("SaveUserHousing ����. USER_HOUSE2F ������Ʈ �Ǿ����ϴ�.");
                }
                else
                {
                    Debug.Log("SaveUserHousing ����.");
                }
            }
        }
        if (FurnitureChangeClick.CurrentFurniture.GetComponent<Text>().text == "sidetable" || Starting)
        {
            if (ItemCode == "2010104")            //ù��° �ɼ��� �����ߴٸ�
            {
                Destroy(SidetablePosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Sidetable/sidetable_W"), SidetablePosition.transform);
                TempObject.transform.SetParent(SidetablePosition.transform);
            }
            else if (ItemCode == "2020104")       //�ι�° �ɼ��� �����ߴٸ�
            {
                Destroy(SidetablePosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Sidetable/Table_3"), SidetablePosition.transform);
                TempObject.transform.SetParent(SidetablePosition.transform);
            }
            else if (ItemCode == "2030104")       //����° �ɼ��� �����ߴٸ�
            {
                Destroy(SidetablePosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Sidetable/table0_K"), SidetablePosition.transform);
                TempObject.transform.SetParent(SidetablePosition.transform);
            }
            else if (ItemCode == "2040103")       //�׹�° �ɼ��� �����ߴٸ�
            {
                Destroy(SidetablePosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Sidetable/table_A"), SidetablePosition.transform);
                TempObject.transform.SetParent(SidetablePosition.transform);
            }
            if (!Starting)
            {
                Param param = new Param();
                param.Add("sidetable", ItemCode);

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
        if (FurnitureChangeClick.CurrentFurniture.GetComponent<Text>().text == "fridge" || Starting)
        {
            if (ItemCode == "2010201")            //ù��° �ɼ��� �����ߴٸ�
            {
                Destroy(FridgePosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Fridge/Fridge_01"), FridgePosition.transform);
                TempObject.transform.SetParent(FridgePosition.transform);
            }
            else if (ItemCode == "2020201")       //�ι�° �ɼ��� �����ߴٸ�
            {
                Destroy(FridgePosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Fridge/fridge_bw"), FridgePosition.transform);
                TempObject.transform.SetParent(FridgePosition.transform);
            }
            else if (ItemCode == "2030201")       //����° �ɼ��� �����ߴٸ�
            {
                Destroy(FridgePosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Fridge/Fridge_K"), FridgePosition.transform);
                TempObject.transform.SetParent(FridgePosition.transform);
            }
            else if (ItemCode == "2040106")       //�׹�° �ɼ��� �����ߴٸ�
            {
                Destroy(FridgePosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Fridge/Fridge_A"), FridgePosition.transform);
                TempObject.transform.SetParent(FridgePosition.transform);
            }
            if (!Starting)
            {
                Param param = new Param();
                param.Add("fridge", ItemCode);

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
        if (FurnitureChangeClick.CurrentFurniture.GetComponent<Text>().text == "wallshelf" || Starting)
        {
            if (ItemCode == "2010106")            //ù��° �ɼ��� �����ߴٸ�
            {
                Destroy(WallshelfPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Wallshelf/Wallshelf_05"), WallshelfPosition.transform);
                TempObject.transform.SetParent(WallshelfPosition.transform);
            }
            else if (ItemCode == "2020106")       //�ι�° �ɼ��� �����ߴٸ�
            {
                Destroy(WallshelfPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Wallshelf/WallShelf_BW"), WallshelfPosition.transform);
                TempObject.transform.SetParent(WallshelfPosition.transform);
            }
            else if (ItemCode == "2030106")       //����° �ɼ��� �����ߴٸ�
            {
                Destroy(WallshelfPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Wallshelf/WallShelf_K"), WallshelfPosition.transform);
                TempObject.transform.SetParent(WallshelfPosition.transform);
            }
            else if (ItemCode == "2040105")       //�׹�° �ɼ��� �����ߴٸ�
            {
                Destroy(WallshelfPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Wallshelf/WallShelf_A"), WallshelfPosition.transform);
                TempObject.transform.SetParent(WallshelfPosition.transform);
            }
            if (!Starting)
            {
                Param param = new Param();
                param.Add("wallshelf", ItemCode);

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
        if (FurnitureChangeClick.CurrentFurniture.GetComponent<Text>().text == "bookshelf" || Starting)
        {
            if (ItemCode == "2010203")            //ù��° �ɼ��� �����ߴٸ�
            {
                Destroy(BookshelfPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Bookshelf/bookshelf_W"), BookshelfPosition.transform);
                TempObject.transform.SetParent(BookshelfPosition.transform);
            }
            else if (ItemCode == "2020203")       //�ι�° �ɼ��� �����ߴٸ�
            {
                Destroy(BookshelfPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Bookshelf/bookshelf"), BookshelfPosition.transform);
                TempObject.transform.SetParent(BookshelfPosition.transform);
            }
            else if (ItemCode == "2030203")       //����° �ɼ��� �����ߴٸ�
            {
                Destroy(BookshelfPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Bookshelf/bookshelf_K"), BookshelfPosition.transform);
                TempObject.transform.SetParent(BookshelfPosition.transform);
            }
            else if (ItemCode == "2040202")       //�׹�° �ɼ��� �����ߴٸ�
            {
                Destroy(BookshelfPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Bookshelf/Bookcase_A"), BookshelfPosition.transform);
                TempObject.transform.SetParent(BookshelfPosition.transform);
            }
            if (!Starting)
            {
                Param param = new Param();
                param.Add("bookshelf", ItemCode);

                //���� ���� row �˻�
                var bro = Backend.GameData.Get("USER_HOUSE2F", new Where());
                string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

                //�ش� row�� ���� update
                var bro2 = Backend.GameData.UpdateV2("USER_HOUSE2F", rowIndate, Backend.UserInDate, param);

                if (bro2.IsSuccess())
                {
                    Debug.Log(ItemCode);
                    Debug.Log("SaveUserHousing ����. USER_HOUSE2F ������Ʈ �Ǿ����ϴ�.");
                }
                else
                {
                    Debug.Log("SaveUserHousing ����.");
                }
            }
        }
        if (FurnitureChangeClick.CurrentFurniture.GetComponent<Text>().text == "standingsink" || Starting)
        {
            if (ItemCode == "2010103")            //ù��° �ɼ��� �����ߴٸ�
            {
                Destroy(StandSinkPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Standingsink/washstand_W"), StandSinkPosition.transform);
                TempObject.transform.SetParent(StandSinkPosition.transform);
            }
            else if (ItemCode == "2020103")       //�ι�° �ɼ��� �����ߴٸ�
            {
                Destroy(StandSinkPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Standingsink/washstand"), StandSinkPosition.transform);
                TempObject.transform.SetParent(StandSinkPosition.transform);
            }
            else if (ItemCode == "2030103")       //����° �ɼ��� �����ߴٸ�
            {
                Destroy(StandSinkPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Standingsink/washstand_K"), StandSinkPosition.transform);
                TempObject.transform.SetParent(StandSinkPosition.transform);
            }
            else if (ItemCode == "2040102")       //�׹�° �ɼ��� �����ߴٸ�
            {
                Destroy(StandSinkPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Standingsink/washstand_A"), StandSinkPosition.transform);
                TempObject.transform.SetParent(StandSinkPosition.transform);
            }
            if (!Starting)
            {
                Param param = new Param();
                param.Add("standingsink", ItemCode);

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
        if (FurnitureChangeClick.CurrentFurniture.GetComponent<Text>().text == "desk" || Starting)
        {
            Debug.Log(ItemCode);
            if (ItemCode == "2010204")            //ù��° �ɼ��� �����ߴٸ�
            {
                Destroy(DeskPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Desk/Desk2_W"), DeskPosition.transform);
                TempObject.transform.SetParent(DeskPosition.transform);
            }
            else if (ItemCode == "2020204")       //�ι�° �ɼ��� �����ߴٸ�
            {
                Destroy(DeskPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Desk/desk_2"), DeskPosition.transform);
                TempObject.transform.SetParent(DeskPosition.transform);
            }
            else if (ItemCode == "2030204")       //����° �ɼ��� �����ߴٸ�
            {
                Destroy(DeskPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Desk/Desk2_K"), DeskPosition.transform);
                TempObject.transform.SetParent(DeskPosition.transform);
            }
            else if (ItemCode == "2040203")       //�׹�° �ɼ��� �����ߴٸ�
            {
                Destroy(DeskPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Desk/Desk4_A"), DeskPosition.transform);
                TempObject.transform.SetParent(DeskPosition.transform);
            }
            if (!Starting)
            {
                Param param = new Param();
                param.Add("desk", ItemCode);

                //���� ���� row �˻�
                var bro = Backend.GameData.Get("USER_HOUSE2F", new Where());
                string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

                //�ش� row�� ���� update
                var bro2 = Backend.GameData.UpdateV2("USER_HOUSE2F", rowIndate, Backend.UserInDate, param);

                if (bro2.IsSuccess())
                {
                    Debug.Log(ItemCode);
                    Debug.Log("SaveUserHousing ����. USER_HOUSE2F ������Ʈ �Ǿ����ϴ�.");
                }
                else
                {
                    Debug.Log("SaveUserHousing ����.");
                }
            }
        }
        if (FurnitureChangeClick.CurrentFurniture.GetComponent<Text>().text == "chair" || (Starting && temp == 0))
        {
            if (ItemCode == "2010205")            //ù��° �ɼ��� �����ߴٸ�
            {
                Destroy(ChairPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Chair/Chair2_W"), ChairPosition.transform);
                TempObject.transform.SetParent(ChairPosition.transform);
            }
            else if (ItemCode == "2020205")       //�ι�° �ɼ��� �����ߴٸ�
            {
                Destroy(ChairPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Chair/chair_2"), ChairPosition.transform);
                TempObject.transform.SetParent(ChairPosition.transform);
            }
            else if (ItemCode == "2030205")       //����° �ɼ��� �����ߴٸ�
            {
                Destroy(ChairPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Chair/Chair_K"), ChairPosition.transform);
                TempObject.transform.SetParent(ChairPosition.transform);
            }
            else if (ItemCode == "2040204")       //�׹�° �ɼ��� �����ߴٸ�
            {
                Destroy(ChairPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Chair/Chair_A"), ChairPosition.transform);
                TempObject.transform.SetParent(ChairPosition.transform);
            }
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
        if (FurnitureChangeClick.CurrentFurniture.GetComponent<Text>().text == "chair2" || (Starting && temp == 1))
        {
            if (ItemCode == "2010205")            //ù��° �ɼ��� �����ߴٸ�
            {
                Destroy(ChairPosition2.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Chair/Chair2_W"), ChairPosition2.transform);
                TempObject.transform.SetParent(ChairPosition2.transform);
            }
            else if (ItemCode == "2020205")       //�ι�° �ɼ��� �����ߴٸ�
            {
                Destroy(ChairPosition2.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Chair/chair_2"), ChairPosition2.transform);
                TempObject.transform.SetParent(ChairPosition2.transform);
            }
            else if (ItemCode == "2030205")       //����° �ɼ��� �����ߴٸ�
            {
                Destroy(ChairPosition2.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Chair/Chair_K"), ChairPosition2.transform);
                TempObject.transform.SetParent(ChairPosition2.transform);
            }
            else if (ItemCode == "2040204")       //�׹�° �ɼ��� �����ߴٸ�
            {
                Destroy(ChairPosition2.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Chair/Chair_A"), ChairPosition2.transform);
                TempObject.transform.SetParent(ChairPosition2.transform);
            }
            TempObject.GetComponent<Text>().text = "chair2";
            if (!Starting)
            {
                Param param = new Param();
                param.Add("chair2", ItemCode);

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
        if (FurnitureChangeClick.CurrentFurniture.GetComponent<Text>().text == "chair3" || (Starting && temp == 2))
        {
            if (ItemCode == "2010205")            //ù��° �ɼ��� �����ߴٸ�
            {
                Destroy(ChairPosition3.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Chair/Chair2_W"), ChairPosition3.transform);
                TempObject.transform.SetParent(ChairPosition3.transform);
            }
            else if (ItemCode == "2020205")       //�ι�° �ɼ��� �����ߴٸ�
            {
                Destroy(ChairPosition3.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Chair/chair_2"), ChairPosition3.transform);
                TempObject.transform.SetParent(ChairPosition3.transform);
            }
            else if (ItemCode == "2030205")       //����° �ɼ��� �����ߴٸ�
            {
                Destroy(ChairPosition3.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Chair/Chair_K"), ChairPosition3.transform);
                TempObject.transform.SetParent(ChairPosition3.transform);
            }
            else if (ItemCode == "2040204")       //�׹�° �ɼ��� �����ߴٸ�
            {
                Destroy(ChairPosition3.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Chair/Chair_A"), ChairPosition3.transform);
                TempObject.transform.SetParent(ChairPosition3.transform);
            }
            TempObject.GetComponent<Text>().text = "chair3";
            if (!Starting)
            {
                Param param = new Param();
                param.Add("chair3", ItemCode);

                //���� ���� row �˻�
                var bro = Backend.GameData.Get("USER_HOUSE2F", new Where());
                string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

                //�ش� row�� ���� update
                var bro2 = Backend.GameData.UpdateV2("USER_HOUSE2F", rowIndate, Backend.UserInDate, param);

                if (bro2.IsSuccess())
                {
                    Debug.Log(ItemCode);
                    Debug.Log("SaveUserHousing ����. USER_HOUSE2F ������Ʈ �Ǿ����ϴ�.");
                }
                else
                {
                    Debug.Log("SaveUserHousing ����.");
                }
            }
        }
        if (FurnitureChangeClick.CurrentFurniture.GetComponent<Text>().text == "sunbed" || (Starting && temp == 3))
        {
            if (ItemCode == "2010302")            //ù��° �ɼ��� �����ߴٸ�
            {
                Destroy(SunbedPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Sunbed/sunbed_W"), SunbedPosition.transform);
                TempObject.transform.SetParent(SunbedPosition.transform);
            }
            else if (ItemCode == "2020302")       //�ι�° �ɼ��� �����ߴٸ�
            {
                Destroy(SunbedPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Sunbed/sunbed"), SunbedPosition.transform);
                TempObject.transform.SetParent(SunbedPosition.transform);
            }
            else if (ItemCode == "2030206")       //����° �ɼ��� �����ߴٸ�
            {
                Destroy(SunbedPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Sunbed/sunbed_K"), SunbedPosition.transform);
                TempObject.transform.SetParent(SunbedPosition.transform);
            }
            else if (ItemCode == "2040205")       //�׹�° �ɼ��� �����ߴٸ�
            {
                Destroy(SunbedPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Sunbed/Sunbed_A"), SunbedPosition.transform);
                TempObject.transform.SetParent(SunbedPosition.transform);
            }
            if (!Starting)
            {
                Param param = new Param();
                param.Add("sunbed", ItemCode);

                //���� ���� row �˻�
                var bro = Backend.GameData.Get("USER_HOUSE2F", new Where());
                string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

                //�ش� row�� ���� update
                var bro2 = Backend.GameData.UpdateV2("USER_HOUSE2F", rowIndate, Backend.UserInDate, param);

                if (bro2.IsSuccess())
                {
                    Debug.Log(ItemCode);
                    Debug.Log("SaveUserHousing ����. USER_HOUSE2F ������Ʈ �Ǿ����ϴ�.");
                }
                else
                {
                    Debug.Log("SaveUserHousing ����.");
                }
            }
        }
        if (FurnitureChangeClick.CurrentFurniture.GetComponent<Text>().text == "sunbed2" || (Starting && temp == 4))
        {
            if (ItemCode == "2010302")            //ù��° �ɼ��� �����ߴٸ�
            {
                Destroy(SunbedPosition2.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Sunbed/sunbed_W"), SunbedPosition2.transform);
                TempObject.transform.SetParent(SunbedPosition2.transform);
            }
            else if (ItemCode == "2020302")       //�ι�° �ɼ��� �����ߴٸ�
            {
                Destroy(SunbedPosition2.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Sunbed/sunbed"), SunbedPosition2.transform);
                TempObject.transform.SetParent(SunbedPosition2.transform);
            }
            else if (ItemCode == "2030206")       //����° �ɼ��� �����ߴٸ�
            {
                Destroy(SunbedPosition2.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Sunbed/sunbed_K"), SunbedPosition2.transform);
                TempObject.transform.SetParent(SunbedPosition2.transform);
            }
            else if (ItemCode == "2040205")       //�׹�° �ɼ��� �����ߴٸ�
            {
                Destroy(SunbedPosition2.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Sunbed/Sunbed_A"), SunbedPosition2.transform);
                TempObject.transform.SetParent(SunbedPosition2.transform);
            }
            TempObject.GetComponent<Text>().text = "sunbed2";
            if (!Starting)
            {
                Param param = new Param();
                param.Add("sunbed2", ItemCode);

                //���� ���� row �˻�
                var bro = Backend.GameData.Get("USER_HOUSE2F", new Where());
                string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

                //�ش� row�� ���� update
                var bro2 = Backend.GameData.UpdateV2("USER_HOUSE2F", rowIndate, Backend.UserInDate, param);

                if (bro2.IsSuccess())
                {
                    Debug.Log(ItemCode);
                    Debug.Log("SaveUserHousing ����. USER_HOUSE2F ������Ʈ �Ǿ����ϴ�.");
                }
                else
                {
                    Debug.Log("SaveUserHousing ����.");
                }
            }
        }
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
        if (FurnitureChangeClick.CurrentFurniture.GetComponent<Text>().text == "sofa" || Starting)
        {
            if (ItemCode == "2010206")            //ù��° �ɼ��� �����ߴٸ�
            {
                Destroy(SofaPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Sofa/sofa_W"), SofaPosition.transform);
                TempObject.transform.SetParent(SofaPosition.transform);
            }
            else if (ItemCode == "2020206")       //�ι�° �ɼ��� �����ߴٸ�
            {
                Destroy(SofaPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Sofa/sofa"), SofaPosition.transform);
                TempObject.transform.SetParent(SofaPosition.transform);
            }
            else if (ItemCode == "8020301")       //����° �ɼ��� �����ߴٸ�
            {
                Destroy(SofaPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Sofa/sofa_K"), SofaPosition.transform);
                TempObject.transform.SetParent(SofaPosition.transform);
            }
            else if (ItemCode == "8020402")       //�׹�° �ɼ��� �����ߴٸ�
            {
                Destroy(SofaPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Sofa/Couch_A"), SofaPosition.transform);
                TempObject.transform.SetParent(SofaPosition.transform);
            }
            if (!Starting)
            {
                Param param = new Param();
                param.Add("sofa", ItemCode);

                //���� ���� row �˻�
                var bro = Backend.GameData.Get("USER_HOUSE2F", new Where());
                string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

                //�ش� row�� ���� update
                var bro2 = Backend.GameData.UpdateV2("USER_HOUSE2F", rowIndate, Backend.UserInDate, param);

                if (bro2.IsSuccess())
                {
                    Debug.Log(ItemCode);
                    Debug.Log("SaveUserHousing ����. USER_HOUSE2F ������Ʈ �Ǿ����ϴ�.");
                }
                else
                {
                    Debug.Log("SaveUserHousing ����.");
                }
            }
        }
    }
}