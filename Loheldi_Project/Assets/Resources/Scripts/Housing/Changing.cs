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
    public GameObject BedPosition2;
    public GameObject ClosetPosition;
    public GameObject BookshelfPosition;
    public GameObject DeskPosition;
    public GameObject DeskPosition2;
    public GameObject DeskPosition3;
    public GameObject DeskPosition4;
    public GameObject TablePosition;
    public GameObject TablePosition2;
    public GameObject SidetablePosition;
    public GameObject ChairPosition;
    public GameObject ChairPosition2;
    public GameObject CchairPosition;
    public GameObject SunbedPosition;
    public GameObject SunbedPosition2;
    public GameObject SunbedPosition3;
    public GameObject SunbedPosition4;
    public GameObject KitchenPosition;

    public Text TempItemCode;

    private bool Starting;
    public int temp;   //chair1,2  ,Desk1,2,3 등 중복 가구 변환 제어용

    public void Start()
    {
        Camera = GameObject.Find("housingCamera");
        F1 = GameObject.Find("1F");
        F2 = GameObject.Find("2F");
        F3 = GameObject.Find("3F");
        F4 = GameObject.Find("4F");
        F5 = GameObject.Find("5F");

        Debug.Log(PlayerPrefs.GetInt("HouseLv"));
        NowFloor = PlayerPrefs.GetInt("HouseLv");

        if (NowFloor == 1)
        {
            Camera.transform.position = new Vector3(-21f, 5.5f, -4f);
            BedPosition = F1.transform.Find("BedPosition").gameObject;
            DeskPosition = F1.transform.Find("DeskPosition").gameObject;
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
            DeskPosition = F2.transform.Find("DeskPosition").gameObject;
            ChairPosition = F2.transform.Find("ChairPosition").gameObject;
            ChairPosition2 = F2.transform.Find("ChairPosition2").gameObject;
            ClosetPosition = F2.transform.Find("ClosetPosition").gameObject;
            KitchenPosition = F2.transform.Find("KitchenPosition").gameObject;
            if (this.name == "HousingSystem")
            {
                Player.transform.position = new Vector3(-22.5f, -1.8f, -67.5f);
            }
        }
        if (NowFloor >= 3)
        {
            Camera.transform.position = new Vector3(-20f, 6f, -169f);
            DeskPosition2 = F3.transform.Find("DeskPosition2").gameObject;
            DeskPosition3 = F3.transform.Find("DeskPosition3").gameObject;
            TablePosition = F3.transform.Find("TablePosition").gameObject;
            SunbedPosition = F3.transform.Find("SunbedPosition").gameObject;
            SunbedPosition2 = F3.transform.Find("SunbedPosition2").gameObject;
            if (this.name == "HousingSystem")
            {
                Player.transform.position = new Vector3(-22.5f, -1.8f, -95.2f);
            }
        }
        if (NowFloor >= 4)
        {
            Camera.transform.position = new Vector3(-20f, 6.5f, -243.5f);
            BedPosition2 = F4.transform.Find("BedPosition2").gameObject;
            SidetablePosition = F4.transform.Find("SidetablePosition").gameObject;
            DeskPosition4 = F4.transform.Find("DeskPosition4").gameObject;
            CchairPosition = F4.transform.Find("CChairPosition").gameObject;
            BookshelfPosition = F4.transform.Find("BookshelfPosition").gameObject;
            if (this.name == "HousingSystem")
            {
                Player.transform.position = new Vector3(-22.5f, -1.8f, -165f);
            }
        }
        if (NowFloor >= 5)
        {
            Camera.transform.position = new Vector3(-20f, 6.5f, -307f);
            SunbedPosition3 = F5.transform.Find("SunbedPosition3").gameObject;
            SunbedPosition4 = F5.transform.Find("SunbedPosition4").gameObject;
            ChairPosition = F2.transform.Find("ChairPosition").gameObject;
            if (this.name == "HousingSystem")
            {
                Player.transform.position = new Vector3(-22.5f, -1.8f, -300f);
            }
        }
    }
    public void FirstSetting()
    {
        Starting = true;
        Save_Basic.LoadUserHousing();

        TempItemCode = GameObject.Find("TempItemCodeforLoad").GetComponent<Text>();

        TempItemCode.text = PlayerPrefs.GetString("bed");
        if (TempItemCode.text != "")
        {
            ButtonClick(TempItemCode);
        }
        temp = 1;
        TempItemCode.text = PlayerPrefs.GetString("bed2");
        if (TempItemCode.text != "")
        {
            ButtonClick(TempItemCode);
        }
        temp = 0;
        TempItemCode.text = PlayerPrefs.GetString("closet");
        if (TempItemCode.text != "")
            ButtonClick(TempItemCode);
        TempItemCode.text = PlayerPrefs.GetString("bookshelf");
        if (TempItemCode.text != "")
            ButtonClick(TempItemCode);
        TempItemCode.text = PlayerPrefs.GetString("desk");
        if (TempItemCode.text != "")
        {
            ButtonClick(TempItemCode);
        }
        temp = 1;
        TempItemCode.text = PlayerPrefs.GetString("desk2");
        if (TempItemCode.text != "")
        {
            ButtonClick(TempItemCode);
        }
        temp = 2;
        TempItemCode.text = PlayerPrefs.GetString("desk3");
        if (TempItemCode.text != "")
        {
            ButtonClick(TempItemCode);
        }
        temp = 3;
        TempItemCode.text = PlayerPrefs.GetString("desk4");
        if (TempItemCode.text != "")
        {
            ButtonClick(TempItemCode);
        }
        temp = 0;
        TempItemCode.text = PlayerPrefs.GetString("table");
        if (TempItemCode.text != "")
        {
            ButtonClick(TempItemCode);
        }
        temp = 1;
        TempItemCode.text = PlayerPrefs.GetString("table2");
        if (TempItemCode.text != "")
        {
            ButtonClick(TempItemCode);
        }
        temp = 0;
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
        temp = 0;
        TempItemCode.text = PlayerPrefs.GetString("cchair");
        if (TempItemCode.text != "")
            ButtonClick(TempItemCode);
        TempItemCode.text = PlayerPrefs.GetString("sunbed");
        if (TempItemCode.text != "")
        {
            ButtonClick(TempItemCode);
        }
        temp = 1;
        TempItemCode.text = PlayerPrefs.GetString("sunbed2");
        if (TempItemCode.text != "")
        {
            ButtonClick(TempItemCode);
        }
        temp = 2;
        TempItemCode.text = PlayerPrefs.GetString("sunbed3");
        if (TempItemCode.text != "")
        {
            ButtonClick(TempItemCode);
        }
        temp = 3;
        TempItemCode.text = PlayerPrefs.GetString("sunbed4");
        if (TempItemCode.text != "")
        {
            ButtonClick(TempItemCode);
        }
        temp = 0;
        TempItemCode.text = PlayerPrefs.GetString("kitchen");
        if (TempItemCode.text != "")
            ButtonClick(TempItemCode);
        //2~4층 가구
        Starting = false;
        Debug.Log("처음 가구 셋팅 완료");
    }

    public void ButtonClick(Text ItemCodeObject)
    {
        string ItemCode = ItemCodeObject.text;
        if (FurnitureChangeClick.CurrentFurniture.GetComponent<Text>().text == "bed" || (Starting && temp == 0))
        {
            if (ItemCode == "2010101")            //첫번째 옵션을 선택했다면
            {
                Destroy(BedPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Beds/Bed_W"), BedPosition.transform);  //해당 주소에 오브젝트를 생성해서 물러옴.
                TempObject.transform.SetParent(BedPosition.transform);                                                                //오브젝트를 BedPosition에 Child로 저장.
            }
            else if (ItemCode == "2020101")       //두번째 옵션을 선택했다면
            {
                Destroy(BedPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Beds/Bed_Single"), BedPosition.transform);  //해당 주소에 오브젝트를 생성해서 물러옴.
                TempObject.transform.SetParent(BedPosition.transform);                                                                //오브젝트를 BedPosition에 Child로 저장.
            }
            else if (ItemCode == "2030101")       //세번째 옵션을 선택했다면
            {
                Destroy(BedPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Beds/Bed_K"), BedPosition.transform);      //해당 주소에 오브젝트를 생성해서 물러옴.
                TempObject.transform.SetParent(BedPosition.transform);                                                                //오브젝트를 BedPosition에 Child로 저장.
            }
            else if (ItemCode == "8020401")       //네번째 옵션을 선택했다면
            {
                Destroy(BedPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Beds/Bed_A"), BedPosition.transform);      //해당 주소에 오브젝트를 생성해서 물러옴.
                TempObject.transform.SetParent(BedPosition.transform);                                                                //오브젝트를 BedPosition에 Child로 저장
            }
            Debug.Log("가구 변경 완료");
            if (!Starting)
            {
                Param param = new Param();
                param.Add("bed", ItemCode);

                //유저 현재 row 검색
                var bro = Backend.GameData.Get("USER_HOUSE", new Where());
                string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

                //해당 row의 값을 update
                var bro2 = Backend.GameData.UpdateV2("USER_HOUSE", rowIndate, Backend.UserInDate, param);

                if (bro2.IsSuccess())
                {
                    Debug.Log(ItemCode);
                    Debug.Log("SaveUserHousing 성공. USER_HOUSE 업데이트 되었습니다.");
                }
                else
                {
                    Debug.Log("SaveUserHousing 실패.");
                }
            }
        }
        if (FurnitureChangeClick.CurrentFurniture.GetComponent<Text>().text == "bed2" || (Starting && temp == 1))
        {
            if (ItemCode == "2010101")            //첫번째 옵션을 선택했다면
            {
                Destroy(BedPosition2.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Beds/Bed_W"), BedPosition2.transform);  //해당 주소에 오브젝트를 생성해서 물러옴.
                TempObject.transform.SetParent(BedPosition2.transform);                                                                //오브젝트를 BedPosition에 Child로 저장.
            }
            else if (ItemCode == "2020101")       //두번째 옵션을 선택했다면
            {
                Destroy(BedPosition2.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Beds/Bed_Single"), BedPosition2.transform);  //해당 주소에 오브젝트를 생성해서 물러옴.
                TempObject.transform.SetParent(BedPosition2.transform);                                                                //오브젝트를 BedPosition에 Child로 저장.
            }
            else if (ItemCode == "2030101")       //세번째 옵션을 선택했다면
            {
                Destroy(BedPosition2.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Beds/Bed_K"), BedPosition2.transform);      //해당 주소에 오브젝트를 생성해서 물러옴.
                TempObject.transform.SetParent(BedPosition2.transform);                                                                //오브젝트를 BedPosition에 Child로 저장.
            }
            else if (ItemCode == "8020401")       //네번째 옵션을 선택했다면
            {
                Destroy(BedPosition2.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Beds/Bed_A"), BedPosition2.transform);      //해당 주소에 오브젝트를 생성해서 물러옴.
                TempObject.transform.SetParent(BedPosition2.transform);                                                                //오브젝트를 BedPosition에 Child로 저장
            }
            TempObject.GetComponent<Text>().text = "bed2";
            Debug.Log("가구 변경 완료");
            if (!Starting)
            {
                Param param = new Param();
                param.Add("bed2", ItemCode);

                //유저 현재 row 검색
                var bro = Backend.GameData.Get("USER_HOUSE", new Where());
                string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

                //해당 row의 값을 update
                var bro2 = Backend.GameData.UpdateV2("USER_HOUSE", rowIndate, Backend.UserInDate, param);

                if (bro2.IsSuccess())
                {
                    Debug.Log(ItemCode);
                    Debug.Log("SaveUserHousing 성공. USER_HOUSE 업데이트 되었습니다.");
                }
                else
                {
                    Debug.Log("SaveUserHousing 실패.");
                }
            }
        }
        if (FurnitureChangeClick.CurrentFurniture.GetComponent<Text>().text == "closet" || Starting)
        {
            if (ItemCode == "2010102")            //첫번째 옵션을 선택했다면
            {
                Destroy(ClosetPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/FurnitureSets/Wardrobe_W"), ClosetPosition.transform);
                TempObject.transform.SetParent(ClosetPosition.transform);
            }
            else if (ItemCode == "2020102")       //두번째 옵션을 선택했다면
            {
                Destroy(ClosetPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/FurnitureSets/wardrobe"), ClosetPosition.transform);
                TempObject.transform.SetParent(ClosetPosition.transform);
            }
            else if (ItemCode == "2030102")       //세번째 옵션을 선택했다면
            {
                Destroy(ClosetPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/FurnitureSets/Wardrobe_K"), ClosetPosition.transform);
                TempObject.transform.SetParent(ClosetPosition.transform);
            }
            else if (ItemCode == "2040101")       //네번째 옵션을 선택했다면
            {
                Destroy(ClosetPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/FurnitureSets/Wardrobe_A"), ClosetPosition.transform);
                TempObject.transform.SetParent(ClosetPosition.transform);
            }
            Debug.Log("가구 변경 완료");
            if (!Starting)
            {
                Param param = new Param();
                param.Add("closet", ItemCode);

                //유저 현재 row 검색
                var bro = Backend.GameData.Get("USER_HOUSE", new Where());
                string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

                //해당 row의 값을 update
                var bro2 = Backend.GameData.UpdateV2("USER_HOUSE", rowIndate, Backend.UserInDate, param);

                if (bro2.IsSuccess())
                {
                    Debug.Log(ItemCode);
                    Debug.Log("SaveUserHousing 성공. USER_HOUSE 업데이트 되었습니다.");
                }
                else
                {
                    Debug.Log("SaveUserHousing 실패.");
                }
            }
        }
        if (FurnitureChangeClick.CurrentFurniture.GetComponent<Text>().text == "bookshelf" || Starting)
        {
            if (ItemCode == "2010203")            //첫번째 옵션을 선택했다면
            {
                Destroy(BookshelfPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Bookshelf/bookshelf_W"), BookshelfPosition.transform);
                TempObject.transform.SetParent(BookshelfPosition.transform);
            }
            else if (ItemCode == "2020203")       //두번째 옵션을 선택했다면
            {
                Destroy(BookshelfPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Bookshelf/bookshelf"), BookshelfPosition.transform);
                TempObject.transform.SetParent(BookshelfPosition.transform);
            }
            else if (ItemCode == "2030203")       //세번째 옵션을 선택했다면
            {
                Destroy(BookshelfPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Bookshelf/bookshelf_K"), BookshelfPosition.transform);
                TempObject.transform.SetParent(BookshelfPosition.transform);
            }
            else if (ItemCode == "2040202")       //네번째 옵션을 선택했다면
            {
                Destroy(BookshelfPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Bookshelf/Bookshelf_A"), BookshelfPosition.transform);
                TempObject.transform.SetParent(BookshelfPosition.transform);
            }
            Debug.Log("가구 변경 완료");
            if (!Starting)
            {
                Param param = new Param();
                param.Add("bookshelf", ItemCode);

                //유저 현재 row 검색
                var bro = Backend.GameData.Get("USER_HOUSE", new Where());
                string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

                //해당 row의 값을 update
                var bro2 = Backend.GameData.UpdateV2("USER_HOUSE", rowIndate, Backend.UserInDate, param);

                if (bro2.IsSuccess())
                {
                    Debug.Log(ItemCode);
                    Debug.Log("SaveUserHousing 성공. USER_HOUSE 업데이트 되었습니다.");
                }
                else
                {
                    Debug.Log("SaveUserHousing 실패.");
                }
            }
        }
        if (FurnitureChangeClick.CurrentFurniture.GetComponent<Text>().text == "desk" || (Starting && temp == 0))
        {
            if (ItemCode == "2010204")            //첫번째 옵션을 선택했다면
            {
                Destroy(DeskPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Desk/Desk4_W"), DeskPosition.transform);
                TempObject.transform.SetParent(DeskPosition.transform);
            }
            else if (ItemCode == "2020204")       //두번째 옵션을 선택했다면
            {
                Destroy(DeskPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Desk/Desk_4"), DeskPosition.transform);
                TempObject.transform.SetParent(DeskPosition.transform);
            }
            else if (ItemCode == "2030204")       //세번째 옵션을 선택했다면
            {
                Destroy(DeskPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Desk/Desk4_K"), DeskPosition.transform);
                TempObject.transform.SetParent(DeskPosition.transform);
            }
            else if (ItemCode == "2040203")       //네번째 옵션을 선택했다면
            {
                Destroy(DeskPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Desk/Desk_A"), DeskPosition.transform);
                TempObject.transform.SetParent(DeskPosition.transform);
            }
            Debug.Log("가구 변경 완료");
            if (!Starting)
            {
                Param param = new Param();
                param.Add("desk", ItemCode);

                //유저 현재 row 검색
                var bro = Backend.GameData.Get("USER_HOUSE", new Where());
                string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

                //해당 row의 값을 update
                var bro2 = Backend.GameData.UpdateV2("USER_HOUSE", rowIndate, Backend.UserInDate, param);

                if (bro2.IsSuccess())
                {
                    Debug.Log(ItemCode);
                    Debug.Log("SaveUserHousing 성공. USER_HOUSE 업데이트 되었습니다.");
                }
                else
                {
                    Debug.Log("SaveUserHousing 실패.");
                }
            }
        }
        if (FurnitureChangeClick.CurrentFurniture.GetComponent<Text>().text == "desk2" || (Starting && temp == 1))
        {
            if (ItemCode == "2010204")            //첫번째 옵션을 선택했다면
            {
                Destroy(DeskPosition2.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Desk/Desk4_W"), DeskPosition2.transform);
                TempObject.transform.SetParent(DeskPosition2.transform);
            }
            else if (ItemCode == "2020204")       //두번째 옵션을 선택했다면
            {
                Destroy(DeskPosition2.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Desk/Desk_4"), DeskPosition2.transform);
                TempObject.transform.SetParent(DeskPosition2.transform);
            }
            else if (ItemCode == "2030204")       //세번째 옵션을 선택했다면
            {
                Destroy(DeskPosition2.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Desk/Desk4_K"), DeskPosition2.transform);
                TempObject.transform.SetParent(DeskPosition2.transform);
            }
            else if (ItemCode == "2040203")       //네번째 옵션을 선택했다면
            {
                Destroy(DeskPosition2.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Desk/Desk_A"), DeskPosition2.transform);
                TempObject.transform.SetParent(DeskPosition2.transform);
            }
            TempObject.GetComponent<Text>().text = "desk2";
            Debug.Log("가구 변경 완료");
            if (!Starting)
            {
                Param param = new Param();
                param.Add("desk2", ItemCode);

                //유저 현재 row 검색
                var bro = Backend.GameData.Get("USER_HOUSE", new Where());
                string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

                //해당 row의 값을 update
                var bro2 = Backend.GameData.UpdateV2("USER_HOUSE", rowIndate, Backend.UserInDate, param);

                if (bro2.IsSuccess())
                {
                    Debug.Log(ItemCode);
                    Debug.Log("SaveUserHousing 성공. USER_HOUSE 업데이트 되었습니다.");
                }
                else
                {
                    Debug.Log("SaveUserHousing 실패.");
                }
            }
        }
        if (FurnitureChangeClick.CurrentFurniture.GetComponent<Text>().text == "desk3" || (Starting && temp == 2))
        {
            if (ItemCode == "2010204")            //첫번째 옵션을 선택했다면
            {
                Destroy(DeskPosition3.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Desk/Desk4_W"), DeskPosition3.transform);
                TempObject.transform.SetParent(DeskPosition3.transform);
            }
            else if (ItemCode == "2020204")       //두번째 옵션을 선택했다면
            {
                Destroy(DeskPosition3.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Desk/Desk_4"), DeskPosition3.transform);
                TempObject.transform.SetParent(DeskPosition3.transform);
            }
            else if (ItemCode == "2030204")       //세번째 옵션을 선택했다면
            {
                Destroy(DeskPosition3.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Desk/Desk4_K"), DeskPosition3.transform);
                TempObject.transform.SetParent(DeskPosition3.transform);
            }
            else if (ItemCode == "2040203")       //네번째 옵션을 선택했다면
            {
                Destroy(DeskPosition3.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Desk/Desk_A"), DeskPosition3.transform);
                TempObject.transform.SetParent(DeskPosition3.transform);
            }
            TempObject.GetComponent<Text>().text = "desk3";
            Debug.Log("가구 변경 완료");
            if (!Starting)
            {
                Param param = new Param();
                param.Add("desk3", ItemCode);

                //유저 현재 row 검색
                var bro = Backend.GameData.Get("USER_HOUSE", new Where());
                string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

                //해당 row의 값을 update
                var bro2 = Backend.GameData.UpdateV2("USER_HOUSE", rowIndate, Backend.UserInDate, param);

                if (bro2.IsSuccess())
                {
                    Debug.Log(ItemCode);
                    Debug.Log("SaveUserHousing 성공. USER_HOUSE 업데이트 되었습니다.");
                }
                else
                {
                    Debug.Log("SaveUserHousing 실패.");
                }
            }
        }
        if (FurnitureChangeClick.CurrentFurniture.GetComponent<Text>().text == "desk4" || (Starting && temp == 3))
        {
            if (ItemCode == "2010204")            //첫번째 옵션을 선택했다면
            {
                Destroy(DeskPosition4.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Desk/Desk4_W"), DeskPosition4.transform);
                TempObject.transform.SetParent(DeskPosition4.transform);
            }
            else if (ItemCode == "2020204")       //두번째 옵션을 선택했다면
            {
                Destroy(DeskPosition4.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Desk/Desk_4"), DeskPosition4.transform);
                TempObject.transform.SetParent(DeskPosition4.transform);
            }
            else if (ItemCode == "2030204")       //세번째 옵션을 선택했다면
            {
                Destroy(DeskPosition4.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Desk/Desk4_K"), DeskPosition4.transform);
                TempObject.transform.SetParent(DeskPosition4.transform);
            }
            else if (ItemCode == "2040203")       //네번째 옵션을 선택했다면
            {
                Destroy(DeskPosition4.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Desk/Desk_A"), DeskPosition4.transform);
                TempObject.transform.SetParent(DeskPosition4.transform);
            }
            TempObject.GetComponent<Text>().text = "desk4";
            Debug.Log("가구 변경 완료");
            if (!Starting)
            {
                Param param = new Param();
                param.Add("desk4", ItemCode);

                //유저 현재 row 검색
                var bro = Backend.GameData.Get("USER_HOUSE", new Where());
                string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

                //해당 row의 값을 update
                var bro2 = Backend.GameData.UpdateV2("USER_HOUSE", rowIndate, Backend.UserInDate, param);

                if (bro2.IsSuccess())
                {
                    Debug.Log(ItemCode);
                    Debug.Log("SaveUserHousing 성공. USER_HOUSE 업데이트 되었습니다.");
                }
                else
                {
                    Debug.Log("SaveUserHousing 실패.");
                }
            }
        }
        if (FurnitureChangeClick.CurrentFurniture.GetComponent<Text>().text == "table" || (Starting && temp == 0))
        {
            if (ItemCode == "2010301")            //첫번째 옵션을 선택했다면
            {
                Destroy(TablePosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Table/table_W"), TablePosition.transform);
                TempObject.transform.SetParent(TablePosition.transform);
            }
            else if (ItemCode == "2020301")       //두번째 옵션을 선택했다면
            {
                Destroy(TablePosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Table/table"), TablePosition.transform);
                TempObject.transform.SetParent(TablePosition.transform);
            }
            else if (ItemCode == "8020302")       //세번째 옵션을 선택했다면
            {
                Destroy(TablePosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Table/table_K"), TablePosition.transform);
                TempObject.transform.SetParent(TablePosition.transform);
            }
            else if (ItemCode == "8020403")       //네번째 옵션을 선택했다면
            {
                Destroy(TablePosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Table/Table_A"), TablePosition.transform);
                TempObject.transform.SetParent(TablePosition.transform);
            }
            Debug.Log("가구 변경 완료");
            if (!Starting)
            {
                Param param = new Param();
                param.Add("table", ItemCode);

                //유저 현재 row 검색
                var bro = Backend.GameData.Get("USER_HOUSE", new Where());
                string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

                //해당 row의 값을 update
                var bro2 = Backend.GameData.UpdateV2("USER_HOUSE", rowIndate, Backend.UserInDate, param);

                if (bro2.IsSuccess())
                {
                    Debug.Log(ItemCode);
                    Debug.Log("SaveUserHousing 성공. USER_HOUSE 업데이트 되었습니다.");
                }
                else
                {
                    Debug.Log("SaveUserHousing 실패.");
                }
            }
        }
        if (FurnitureChangeClick.CurrentFurniture.GetComponent<Text>().text == "table2" || (Starting && temp == 1))
        {
            if (ItemCode == "2010301")            //첫번째 옵션을 선택했다면
            {
                Destroy(TablePosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Table/table_W"), TablePosition.transform);
                TempObject.transform.SetParent(TablePosition.transform);
            }
            else if (ItemCode == "2020301")       //두번째 옵션을 선택했다면
            {
                Destroy(TablePosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Table/table"), TablePosition.transform);
                TempObject.transform.SetParent(TablePosition.transform);
            }
            else if (ItemCode == "8020302")       //세번째 옵션을 선택했다면
            {
                Destroy(TablePosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Table/table_K"), TablePosition.transform);
                TempObject.transform.SetParent(TablePosition.transform);
            }
            else if (ItemCode == "8020403")       //네번째 옵션을 선택했다면
            {
                Destroy(TablePosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Table/Table_A"), TablePosition.transform);
                TempObject.transform.SetParent(TablePosition.transform);
            }
            TempObject.GetComponent<Text>().text = "table2";
            Debug.Log("가구 변경 완료");
            if (!Starting)
            {
                Param param = new Param();
                param.Add("table2", ItemCode);

                //유저 현재 row 검색
                var bro = Backend.GameData.Get("USER_HOUSE", new Where());
                string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

                //해당 row의 값을 update
                var bro2 = Backend.GameData.UpdateV2("USER_HOUSE", rowIndate, Backend.UserInDate, param);

                if (bro2.IsSuccess())
                {
                    Debug.Log(ItemCode);
                    Debug.Log("SaveUserHousing 성공. USER_HOUSE 업데이트 되었습니다.");
                }
                else
                {
                    Debug.Log("SaveUserHousing 실패.");
                }
            }
        }
        if (FurnitureChangeClick.CurrentFurniture.GetComponent<Text>().text == "sidetable" || (Starting))
        {
            if (ItemCode == "2010104")            //첫번째 옵션을 선택했다면
            {
                Destroy(SidetablePosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Sidetable/sidetable_W"), SidetablePosition.transform);
                TempObject.transform.SetParent(SidetablePosition.transform);
            }
            else if (ItemCode == "2020104")       //두번째 옵션을 선택했다면
            {
                Destroy(SidetablePosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Sidetable/Table_3"), SidetablePosition.transform);
                TempObject.transform.SetParent(SidetablePosition.transform);
            }
            else if (ItemCode == "2030104")       //세번째 옵션을 선택했다면
            {
                Destroy(SidetablePosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Sidetable/table0_K"), SidetablePosition.transform);
                TempObject.transform.SetParent(SidetablePosition.transform);
            }
            else if (ItemCode == "2040103")       //네번째 옵션을 선택했다면
            {
                Destroy(SidetablePosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Sidetable/table_A"), SidetablePosition.transform);
                TempObject.transform.SetParent(SidetablePosition.transform);
            }
            Debug.Log("가구 변경 완료");
            if (!Starting)
            {
                Param param = new Param();
                param.Add("sidetable", ItemCode);

                //유저 현재 row 검색
                var bro = Backend.GameData.Get("USER_HOUSE", new Where());
                string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

                //해당 row의 값을 update
                var bro2 = Backend.GameData.UpdateV2("USER_HOUSE", rowIndate, Backend.UserInDate, param);

                if (bro2.IsSuccess())
                {
                    Debug.Log(ItemCode);
                    Debug.Log("SaveUserHousing 성공. USER_HOUSE 업데이트 되었습니다.");
                }
                else
                {
                    Debug.Log("SaveUserHousing 실패.");
                }
            }
        }
        if (FurnitureChangeClick.CurrentFurniture.GetComponent<Text>().text == "chair" || (Starting && temp == 0))
        {
            if (ItemCode == "2010205")            //첫번째 옵션을 선택했다면
            {
                Destroy(ChairPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Chair/Chair2_W"), ChairPosition.transform);
                TempObject.transform.SetParent(ChairPosition.transform);
            }
            else if (ItemCode == "2020205")       //두번째 옵션을 선택했다면
            {
                Destroy(ChairPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Chair/chair_2"), ChairPosition.transform);
                TempObject.transform.SetParent(ChairPosition.transform);
            }
            else if (ItemCode == "2030205")       //세번째 옵션을 선택했다면
            {
                Destroy(ChairPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Chair/Chair_K"), ChairPosition.transform);
                TempObject.transform.SetParent(ChairPosition.transform);
            }
            else if (ItemCode == "2040204")       //네번째 옵션을 선택했다면
            {
                Destroy(ChairPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Chair/Chair_A"), ChairPosition.transform);
                TempObject.transform.SetParent(ChairPosition.transform);
            }
            Debug.Log("가구 변경 완료");
            if (!Starting)
            {
                Param param = new Param();
                param.Add("chair", ItemCode);

                //유저 현재 row 검색
                var bro = Backend.GameData.Get("USER_HOUSE", new Where());
                string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

                //해당 row의 값을 update
                var bro2 = Backend.GameData.UpdateV2("USER_HOUSE", rowIndate, Backend.UserInDate, param);

                if (bro2.IsSuccess())
                {
                    Debug.Log(ItemCode);
                    Debug.Log("SaveUserHousing 성공. USER_HOUSE 업데이트 되었습니다.");
                }
                else
                {
                    Debug.Log("SaveUserHousing 실패.");
                }
            }
        }
        if (FurnitureChangeClick.CurrentFurniture.GetComponent<Text>().text == "chair2" || (Starting && temp == 1))
        {
            if (ItemCode == "2010205")            //첫번째 옵션을 선택했다면
            {
                Destroy(ChairPosition2.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Chair/Chair2_W"), ChairPosition2.transform);
                TempObject.transform.SetParent(ChairPosition2.transform);
            }
            else if (ItemCode == "2020205")       //두번째 옵션을 선택했다면
            {
                Destroy(ChairPosition2.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Chair/chair_2"), ChairPosition2.transform);
                TempObject.transform.SetParent(ChairPosition2.transform);
            }
            else if (ItemCode == "2030205")       //세번째 옵션을 선택했다면
            {
                Destroy(ChairPosition2.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Chair/Chair_K"), ChairPosition2.transform);
                TempObject.transform.SetParent(ChairPosition2.transform);
            }
            else if (ItemCode == "2040204")       //네번째 옵션을 선택했다면
            {
                Destroy(ChairPosition2.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Chair/Chair_A"), ChairPosition2.transform);
                TempObject.transform.SetParent(ChairPosition2.transform);
            }
            TempObject.GetComponent<Text>().text = "chair2";
            Debug.Log("가구 변경 완료");
            if (!Starting)
            {
                Param param = new Param();
                param.Add("chair2", ItemCode);

                //유저 현재 row 검색
                var bro = Backend.GameData.Get("USER_HOUSE", new Where());
                string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

                //해당 row의 값을 update
                var bro2 = Backend.GameData.UpdateV2("USER_HOUSE", rowIndate, Backend.UserInDate, param);

                if (bro2.IsSuccess())
                {
                    Debug.Log(ItemCode);
                    Debug.Log("SaveUserHousing 성공. USER_HOUSE 업데이트 되었습니다.");
                }
                else
                {
                    Debug.Log("SaveUserHousing 실패.");
                }
            }
        }
        /*if (FurnitureChangeClick.CurrentFurniture.GetComponent<Text>().text == "cchair" || (Starting))
        {
            if (ItemCode == "2010206")            //첫번째 옵션을 선택했다면
            {
                Destroy(CchairPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Desk/"), CchairPosition.transform);
                TempObject.transform.SetParent(CchairPosition.transform);
            }
            else if (ItemCode == "2020206")       //두번째 옵션을 선택했다면
            {
                Destroy(CchairPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Desk/"), CchairPosition.transform);
                TempObject.transform.SetParent(CchairPosition.transform);
            }
            else if (ItemCode == "8020301")       //세번째 옵션을 선택했다면
            {
                Destroy(CchairPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Desk/"), CchairPosition.transform);
                TempObject.transform.SetParent(CchairPosition.transform);
            }
            else if (ItemCode == "8020402")       //네번째 옵션을 선택했다면
            {
                Destroy(CchairPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Desk/"), CchairPosition.transform);
                TempObject.transform.SetParent(CchairPosition.transform);
            }
            TempObject.GetComponent<Text>().text = "cchair";
            Debug.Log("가구 변경 완료");
            if (!Starting)
            {
                Param param = new Param();
                param.Add("cchair", ItemCode);

                //유저 현재 row 검색
                var bro = Backend.GameData.Get("USER_HOUSE", new Where());
                string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

                //해당 row의 값을 update
                var bro2 = Backend.GameData.UpdateV2("USER_HOUSE", rowIndate, Backend.UserInDate, param);

                if (bro2.IsSuccess())
                {
                    Debug.Log(ItemCode);
                    Debug.Log("SaveUserHousing 성공. USER_HOUSE 업데이트 되었습니다.");
                }
                else
                {
                    Debug.Log("SaveUserHousing 실패.");
                }
            }
        }*/
        if (FurnitureChangeClick.CurrentFurniture.GetComponent<Text>().text == "sunbed" || (Starting && temp == 0))
        {
            if (ItemCode == "2010302")            //첫번째 옵션을 선택했다면
            {
                Destroy(SunbedPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Sunbed/sunbed_W"), SunbedPosition.transform);
                TempObject.transform.SetParent(SunbedPosition.transform);
            }
            else if (ItemCode == "2020302")       //두번째 옵션을 선택했다면
            {
                Destroy(SunbedPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Sunbed/sunbed"), SunbedPosition.transform);
                TempObject.transform.SetParent(SunbedPosition.transform);
            }
            else if (ItemCode == "2030206")       //세번째 옵션을 선택했다면
            {
                Destroy(SunbedPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Sunbed/sunbed_K"), SunbedPosition.transform);
                TempObject.transform.SetParent(SunbedPosition.transform);
            }
            else if (ItemCode == "2040205")       //네번째 옵션을 선택했다면
            {
                Destroy(SunbedPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Sunbed/Sunbed_A"), SunbedPosition.transform);
                TempObject.transform.SetParent(SunbedPosition.transform);
            }
            Debug.Log("가구 변경 완료");
            if (!Starting)
            {
                Param param = new Param();
                param.Add("sunbed", ItemCode);

                //유저 현재 row 검색
                var bro = Backend.GameData.Get("USER_HOUSE", new Where());
                string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

                //해당 row의 값을 update
                var bro2 = Backend.GameData.UpdateV2("USER_HOUSE", rowIndate, Backend.UserInDate, param);

                if (bro2.IsSuccess())
                {
                    Debug.Log(ItemCode);
                    Debug.Log("SaveUserHousing 성공. USER_HOUSE 업데이트 되었습니다.");
                }
                else
                {
                    Debug.Log("SaveUserHousing 실패.");
                }
            }
        }
        if (FurnitureChangeClick.CurrentFurniture.GetComponent<Text>().text == "sunbed2" || (Starting && temp == 1))
        {
            if (ItemCode == "2010302")            //첫번째 옵션을 선택했다면
            {
                Destroy(SunbedPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Sunbed/sunbed_W"), SunbedPosition.transform);
                TempObject.transform.SetParent(SunbedPosition.transform);
            }
            else if (ItemCode == "2020302")       //두번째 옵션을 선택했다면
            {
                Destroy(SunbedPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Sunbed/sunbed"), SunbedPosition.transform);
                TempObject.transform.SetParent(SunbedPosition.transform);
            }
            else if (ItemCode == "2030206")       //세번째 옵션을 선택했다면
            {
                Destroy(SunbedPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Sunbed/sunbed_K"), SunbedPosition.transform);
                TempObject.transform.SetParent(SunbedPosition.transform);
            }
            else if (ItemCode == "2040205")       //네번째 옵션을 선택했다면
            {
                Destroy(SunbedPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Sunbed/Sunbed_A"), SunbedPosition.transform);
                TempObject.transform.SetParent(SunbedPosition.transform);
            }
            TempObject.GetComponent<Text>().text = "sunbed2";
            Debug.Log("가구 변경 완료");
            if (!Starting)
            {
                Param param = new Param();
                param.Add("sunbed2", ItemCode);

                //유저 현재 row 검색
                var bro = Backend.GameData.Get("USER_HOUSE", new Where());
                string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

                //해당 row의 값을 update
                var bro2 = Backend.GameData.UpdateV2("USER_HOUSE", rowIndate, Backend.UserInDate, param);

                if (bro2.IsSuccess())
                {
                    Debug.Log(ItemCode);
                    Debug.Log("SaveUserHousing 성공. USER_HOUSE 업데이트 되었습니다.");
                }
                else
                {
                    Debug.Log("SaveUserHousing 실패.");
                }
            }
        }
        if (FurnitureChangeClick.CurrentFurniture.GetComponent<Text>().text == "sunbed3" || (Starting && temp == 2))
        {
            if (ItemCode == "2010302")            //첫번째 옵션을 선택했다면
            {
                Destroy(SunbedPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Sunbed/sunbed_W"), SunbedPosition.transform);
                TempObject.transform.SetParent(SunbedPosition.transform);
            }
            else if (ItemCode == "2020302")       //두번째 옵션을 선택했다면
            {
                Destroy(SunbedPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Sunbed/sunbed"), SunbedPosition.transform);
                TempObject.transform.SetParent(SunbedPosition.transform);
            }
            else if (ItemCode == "2030206")       //세번째 옵션을 선택했다면
            {
                Destroy(SunbedPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Sunbed/sunbed_K"), SunbedPosition.transform);
                TempObject.transform.SetParent(SunbedPosition.transform);
            }
            else if (ItemCode == "2040205")       //네번째 옵션을 선택했다면
            {
                Destroy(SunbedPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Sunbed/Sunbed_A"), SunbedPosition.transform);
                TempObject.transform.SetParent(SunbedPosition.transform);
            }
            TempObject.GetComponent<Text>().text = "sunbed3";
            Debug.Log("가구 변경 완료");
            if (!Starting)
            {
                Param param = new Param();
                param.Add("sunbed3", ItemCode);

                //유저 현재 row 검색
                var bro = Backend.GameData.Get("USER_HOUSE", new Where());
                string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

                //해당 row의 값을 update
                var bro2 = Backend.GameData.UpdateV2("USER_HOUSE", rowIndate, Backend.UserInDate, param);

                if (bro2.IsSuccess())
                {
                    Debug.Log(ItemCode);
                    Debug.Log("SaveUserHousing 성공. USER_HOUSE 업데이트 되었습니다.");
                }
                else
                {
                    Debug.Log("SaveUserHousing 실패.");
                }
            }
        }
        if (FurnitureChangeClick.CurrentFurniture.GetComponent<Text>().text == "sunbed4" || (Starting && temp == 3))
        {
            if (ItemCode == "2010302")            //첫번째 옵션을 선택했다면
            {
                Destroy(SunbedPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Sunbed/sunbed_W"), SunbedPosition.transform);
                TempObject.transform.SetParent(SunbedPosition.transform);
            }
            else if (ItemCode == "2020302")       //두번째 옵션을 선택했다면
            {
                Destroy(SunbedPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Sunbed/sunbed"), SunbedPosition.transform);
                TempObject.transform.SetParent(SunbedPosition.transform);
            }
            else if (ItemCode == "2030206")       //세번째 옵션을 선택했다면
            {
                Destroy(SunbedPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Sunbed/sunbed_K"), SunbedPosition.transform);
                TempObject.transform.SetParent(SunbedPosition.transform);
            }
            else if (ItemCode == "2040205")       //네번째 옵션을 선택했다면
            {
                Destroy(SunbedPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Sunbed/Sunbed_A"), SunbedPosition.transform);
                TempObject.transform.SetParent(SunbedPosition.transform);
            }
            TempObject.GetComponent<Text>().text = "sunbed4";
            Debug.Log("가구 변경 완료");
            if (!Starting)
            {
                Param param = new Param();
                param.Add("sunbed4", ItemCode);

                //유저 현재 row 검색
                var bro = Backend.GameData.Get("USER_HOUSE", new Where());
                string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

                //해당 row의 값을 update
                var bro2 = Backend.GameData.UpdateV2("USER_HOUSE", rowIndate, Backend.UserInDate, param);

                if (bro2.IsSuccess())
                {
                    Debug.Log(ItemCode);
                    Debug.Log("SaveUserHousing 성공. USER_HOUSE 업데이트 되었습니다.");
                }
                else
                {
                    Debug.Log("SaveUserHousing 실패.");
                }
            }
        }
        if (FurnitureChangeClick.CurrentFurniture.GetComponent<Text>().text == "kitchen" || Starting)
        {
            if (ItemCode == "2010105")            //첫번째 옵션을 선택했다면
            {
                Destroy(KitchenPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Kitchen/Kitchen_W"), KitchenPosition.transform);
                TempObject.transform.SetParent(KitchenPosition.transform);
            }
            else if (ItemCode == "2020105")       //두번째 옵션을 선택했다면
            {
                Destroy(KitchenPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Kitchen/Kitchen_WB"), KitchenPosition.transform);
                TempObject.transform.SetParent(KitchenPosition.transform);
            }
            else if (ItemCode == "2030105")       //세번째 옵션을 선택했다면
            {
                Destroy(KitchenPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Kitchen/Kitchen_K"), KitchenPosition.transform);
                TempObject.transform.SetParent(KitchenPosition.transform);
            }
            else if (ItemCode == "2040104")       //네번째 옵션을 선택했다면
            {
                Destroy(KitchenPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/FurnitureSets/Kitchen_A"), KitchenPosition.transform);
                TempObject.transform.SetParent(KitchenPosition.transform);
            }
            Debug.Log("가구 변경 완료");
            if (!Starting)
            {
                Param param = new Param();
                param.Add("kitchen", ItemCode);

                //유저 현재 row 검색
                var bro = Backend.GameData.Get("USER_HOUSE", new Where());
                string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

                //해당 row의 값을 update
                var bro2 = Backend.GameData.UpdateV2("USER_HOUSE", rowIndate, Backend.UserInDate, param);

                if (bro2.IsSuccess())
                {
                    Debug.Log(ItemCode);
                    Debug.Log("SaveUserHousing 성공. USER_HOUSE 업데이트 되었습니다.");
                }
                else
                {
                    Debug.Log("SaveUserHousing 실패.");
                }
            }
        }
    }
}