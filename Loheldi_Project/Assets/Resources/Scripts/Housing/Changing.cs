using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Changing : MonoBehaviour
{
    public GameObject TempObject;
    public GameObject BedPosition;
    public GameObject TablePosition;
    public GameObject ChairPosition;
    public GameObject ClosetPosition;
    public Text ItemCode;

    private int BedNum = 1;
    private int ClosetNum = 1;
    private int TableNum = 1;
    private int ChairNum = 1;

    public void Start()
    {
        BedPosition = GameObject.Find("BedPosition");
        TablePosition = GameObject.Find("TablePosition");
        ChairPosition = GameObject.Find("ChairPosition");
        ClosetPosition = GameObject.Find("ClosetPosition");
    }
    public void ButtonClick()
    {
        if (FurnitureChangeClick.CurrentFurniture == "Bed_01_Single" || FurnitureChangeClick.CurrentFurniture == "Bed_01_Single(Clone)" || FurnitureChangeClick.CurrentFurniture == "Bed_Single(Clone)" || FurnitureChangeClick.CurrentFurniture == "Bed_W(Clone)" || FurnitureChangeClick.CurrentFurniture == "Bed_K(Clone)")
        {
            if (ItemCode.text == "2010101")            //첫번째 옵션을 선택했다면
            {
                Destroy(BedPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Beds/Bed_W"), BedPosition.transform);  //해당 주소에 오브젝트를 생성해서 물러옴.
                TempObject.transform.SetParent(BedPosition.transform);                                                                //오브젝트를 BedPosition에 Child로 저장.
                BedNum = 1;
            }
            else if (ItemCode.text == "2020101")       //두번째 옵션을 선택했다면
            {
                Destroy(BedPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Beds/Bed_Single"), BedPosition.transform);  //해당 주소에 오브젝트를 생성해서 물러옴.
                TempObject.transform.SetParent(BedPosition.transform);                                                                //오브젝트를 BedPosition에 Child로 저장.
                BedNum = 2;
            }
            else if (ItemCode.text == "2030101")       //세번째 옵션을 선택했다면
            {
                Destroy(BedPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Beds/Bed_K"), BedPosition.transform);      //해당 주소에 오브젝트를 생성해서 물러옴.
                TempObject.transform.SetParent(BedPosition.transform);                                                                //오브젝트를 BedPosition에 Child로 저장.
                BedNum = 3;
            }
            else if (ItemCode.text == "2040101")       //네번째 옵션을 선택했다면
            {
                /*Destroy(BedPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Beds/"), BedPosition.transform);      //해당 주소에 오브젝트를 생성해서 물러옴.
                TempObject.transform.SetParent(BedPosition.transform);                                                                //오브젝트를 BedPosition에 Child로 저장.
                BedNum = 4;*/
            }
        }
                                                                //옷장
        if (FurnitureChangeClick.CurrentFurniture == "FurnitureSet_10_v_02" || FurnitureChangeClick.CurrentFurniture == "wardrobe(Clone)" || FurnitureChangeClick.CurrentFurniture == "Wardrobe_W(Clone)" || FurnitureChangeClick.CurrentFurniture == "FurnitureSet_10_v_02(Clone)" || FurnitureChangeClick.CurrentFurniture == "Wardrobe_K(Clone)")
        {
            if (ItemCode.text == "2010102")            //첫번째 옵션을 선택했다면
            {
                Destroy(ClosetPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/FurnitureSets/Wardrobe_W"), ClosetPosition.transform);
                TempObject.transform.SetParent(ClosetPosition.transform);
                ClosetNum = 1;
            }
            else if (ItemCode.text == "2020102")       //두번째 옵션을 선택했다면
            {
                Destroy(ClosetPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/FurnitureSets/wardrobe"), ClosetPosition.transform);
                TempObject.transform.SetParent(ClosetPosition.transform);
                ClosetNum = 2;
            }
            else if (ItemCode.text == "2030102")       //세번째 옵션을 선택했다면
            {
                Destroy(ClosetPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/FurnitureSets/Wardrobe_K"), ClosetPosition.transform);
                TempObject.transform.SetParent(ClosetPosition.transform);
                ClosetNum = 3;
            }
            else if (ItemCode.text == "2040102")       //네번째 옵션을 선택했다면
            {
                /*Destroy(ClosetPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/FurnitureSets/"), ClosetPosition.transform);
                TempObject.transform.SetParent(ClosetPosition.transform);
                ClosetNum = 4;*/
            }
        }
                                                                //책상
        if (FurnitureChangeClick.CurrentFurniture == "Table_01" || FurnitureChangeClick.CurrentFurniture == "Table_01(Clone)" || FurnitureChangeClick.CurrentFurniture == "table_2(Clone)" || FurnitureChangeClick.CurrentFurniture == "Table4_W(Clone)" || FurnitureChangeClick.CurrentFurniture == "table2_K(Clone)")
        {
            if (ItemCode.text == "2010204")            //첫번째 옵션을 선택했다면
            {
                Destroy(TablePosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Table/Table4_W"), TablePosition.transform);
                TempObject.transform.SetParent(TablePosition.transform);
                TableNum = 1;
            }
            else if (ItemCode.text == "2020204")       //두번째 옵션을 선택했다면
            {
                Destroy(TablePosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Table/Table_2"), TablePosition.transform);
                TempObject.transform.SetParent(TablePosition.transform);
                TableNum = 2;
            }
            else if (ItemCode.text == "2030204")       //세번째 옵션을 선택했다면
            {
                Destroy(TablePosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Table/table2_K"), TablePosition.transform);
                TempObject.transform.SetParent(TablePosition.transform);
                TableNum = 3;
            }
            else if (ItemCode.text == "2040203")       //네번째 옵션을 선택했다면
            {
                /*Destroy(TablePosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Table/"), TablePosition.transform);
                TempObject.transform.SetParent(TablePosition.transform);
                TableNum = 4;*/
            }
        }
                                                                //의자
        if (FurnitureChangeClick.CurrentFurniture == "Chair_01" || FurnitureChangeClick.CurrentFurniture == "Chair_01(Clone)" || FurnitureChangeClick.CurrentFurniture == "chair_2(Clone)" || FurnitureChangeClick.CurrentFurniture == "Chair2_W(Clone)" || FurnitureChangeClick.CurrentFurniture == "Chair_K(Clone)")
        {
            if (ItemCode.text == "2010205")            //첫번째 옵션을 선택했다면
            {
                Destroy(ChairPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Chair/Chair2_W"), ChairPosition.transform);
                TempObject.transform.SetParent(ChairPosition.transform);
                ChairNum = 1;
            }
            else if (ItemCode.text == "2020205")       //두번째 옵션을 선택했다면
            {
                Destroy(ChairPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Chair/chair_2"), ChairPosition.transform);
                TempObject.transform.SetParent(ChairPosition.transform);
                ChairNum = 2;
            }
            else if (ItemCode.text == "2030205")       //세번째 옵션을 선택했다면
            {
                Destroy(ChairPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Chair/Chair_K"), ChairPosition.transform);
                TempObject.transform.SetParent(ChairPosition.transform);
                ChairNum = 3;
            }
            else if (ItemCode.text == "2040204")       //네번째 옵션을 선택했다면
            {
                /*Destroy(ChairPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>(""Prefabs/Furniture/Chair/"), ChairPosition.transform);
                TempObject.transform.SetParent(ChairPosition.transform);
                ChairNum = 4;*/
            }
        }
    }

}