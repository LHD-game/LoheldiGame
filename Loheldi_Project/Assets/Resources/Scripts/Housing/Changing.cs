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
        NowFloor = PlayerPrefs.GetInt("HouseLv");  //집 확장 단계, 나중에 서버와 연결

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
            //2층 가구
        }
        else if (NowFloor == 4)
        {
            //3층 가구
        }
        else if (NowFloor == 5)
        {
            //4층 가구
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
        //2~4층 가구
        Starting = false;
        Debug.Log("처음 가구 셋팅 완료");
    }

    public void ButtonClick(Text ItemCodeObject)
    {
        string ItemCode = ItemCodeObject.text;
        if (FurnitureChangeClick.CurrentFurniture.GetComponent<Text>().text == "bed" || Starting)
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
                                                                //옷장
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
                                                                //책상
        if (FurnitureChangeClick.CurrentFurniture.GetComponent<Text>().text == "table" || Starting)
        {
            if (ItemCode == "2010204")            //첫번째 옵션을 선택했다면
            {
                Destroy(TablePosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Table/Table4_W"), TablePosition.transform);
                TempObject.transform.SetParent(TablePosition.transform);
            }
            else if (ItemCode == "2020204")       //두번째 옵션을 선택했다면
            {
                Destroy(TablePosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Table/Table_2"), TablePosition.transform);
                TempObject.transform.SetParent(TablePosition.transform);
            }
            else if (ItemCode == "2030204")       //세번째 옵션을 선택했다면
            {
                Destroy(TablePosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Table/table2_K"), TablePosition.transform);
                TempObject.transform.SetParent(TablePosition.transform);
            }
            else if (ItemCode == "2040203")       //네번째 옵션을 선택했다면
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
                                                                //의자
        if (FurnitureChangeClick.CurrentFurniture.GetComponent<Text>().text == "chair" || Starting)
        {
            if (ItemCode == "2010205")            //첫번째 옵션을 선택했다면
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
            else if (ItemCode == "2020205")       //두번째 옵션을 선택했다면
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
            else if (ItemCode == "2030205")       //세번째 옵션을 선택했다면
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
            else if (ItemCode == "2040204")       //네번째 옵션을 선택했다면
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
                    //부엌
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