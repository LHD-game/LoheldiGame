using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Changing : MonoBehaviour
{
    public GameObject TempObject;
    public GameObject BedPosition;
    public GameObject TablePosition;
    public GameObject ChairPosition;
    public GameObject ClosetPosition;

    private int BedNum = 1;
    private int ClosetNum = 1;

    public void ButtonClick()
    {                                                           //해당 조건문 고민해볼것. (지금 누른 오브젝트의 이름을 비교해서 침대인지 아닌지 구분하는 조건문임)    P.S.가구에 부품들(옷장에 문짝이나, 서랍 등)도 추가하면 너무 복잡해지므로 콜리더 제거하거나 비활성화시키고 본체에 콜리더 조절하기
        if (FurnitureChangeClick.CurrentFurniture == "Bed_01_Single" || FurnitureChangeClick.CurrentFurniture == "Bed_01_Single(Clone)" || FurnitureChangeClick.CurrentFurniture == "Bed_Single(Clone)" || FurnitureChangeClick.CurrentFurniture == "Bed_W(Clone)")
        {
            if (EventSystem.current.currentSelectedGameObject.name == "Option1")            //첫번째 옵션을 선택했다면
            {
                Destroy(BedPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Beds/Bed_01_Single"), BedPosition.transform);  //해당 주소에 오브젝트를 생성해서 물러옴.
                TempObject.transform.SetParent(BedPosition.transform);                                                                //오브젝트를 BedPosition에 Child로 저장.
                BedNum = 1;
            }
            else if (EventSystem.current.currentSelectedGameObject.name == "Option2")       //두번째 옵션을 선택했다면
            {
                Destroy(BedPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Beds/Bed_Single"), BedPosition.transform);  //해당 주소에 오브젝트를 생성해서 물러옴.
                TempObject.transform.SetParent(BedPosition.transform);                                                                //오브젝트를 BedPosition에 Child로 저장.
                BedNum = 2;
            }
            else if (EventSystem.current.currentSelectedGameObject.name == "Option3")       //세번째 옵션을 선택했다면
            {
                Destroy(BedPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Beds/Bed_W"), BedPosition.transform);      //해당 주소에 오브젝트를 생성해서 물러옴.
                TempObject.transform.SetParent(BedPosition.transform);                                                                //오브젝트를 BedPosition에 Child로 저장.
                BedNum = 3;
            }
        }
                                                                //옷장
        if (FurnitureChangeClick.CurrentFurniture == "FurnitureSet_10_v_02" || FurnitureChangeClick.CurrentFurniture == "wardrobe(Clone)" || FurnitureChangeClick.CurrentFurniture == "Wardrobe_W(Clone)" || FurnitureChangeClick.CurrentFurniture == "FurnitureSet_10_v_02(Clone)")
        {
            if (EventSystem.current.currentSelectedGameObject.name == "Option1")
            {
                Destroy(ClosetPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/FurnitureSets/FurnitureSet_10_v_02"), ClosetPosition.transform);
                TempObject.transform.SetParent(ClosetPosition.transform);
                ClosetNum = 1;
            }
            else if (EventSystem.current.currentSelectedGameObject.name == "Option2")
            {
                Destroy(ClosetPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/FurnitureSets/wardrobe"), ClosetPosition.transform);
                TempObject.transform.SetParent(ClosetPosition.transform);
                ClosetNum = 2;
            }
            else if (EventSystem.current.currentSelectedGameObject.name == "Option3")
            {
                Destroy(ClosetPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/FurnitureSets/Wardrobe_W"), ClosetPosition.transform);
                TempObject.transform.SetParent(ClosetPosition.transform);
                ClosetNum = 3;
            }
        }
                                                                //책상
        if (FurnitureChangeClick.CurrentFurniture == "Table_01" || FurnitureChangeClick.CurrentFurniture == "Table_01(Clone)" || FurnitureChangeClick.CurrentFurniture == "table_2(Clone)" || FurnitureChangeClick.CurrentFurniture == "Table4_W(Clone)")
        {
            if (EventSystem.current.currentSelectedGameObject.name == "Option1")
            {
                Destroy(TablePosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Table/Table_01"), TablePosition.transform);
                TempObject.transform.SetParent(TablePosition.transform);
                ClosetNum = 1;
            }
            else if (EventSystem.current.currentSelectedGameObject.name == "Option2")
            {
                Destroy(TablePosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Table/Table_2"), TablePosition.transform);
                TempObject.transform.SetParent(TablePosition.transform);
                ClosetNum = 2;
            }
            else if (EventSystem.current.currentSelectedGameObject.name == "Option3")
            {
                Destroy(TablePosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Table/Table4_W"), TablePosition.transform);
                TempObject.transform.SetParent(TablePosition.transform);
                ClosetNum = 3;
            }
        }
                                                                //책상
        if (FurnitureChangeClick.CurrentFurniture == "Chair_01" || FurnitureChangeClick.CurrentFurniture == "Chair_01(Clone)" || FurnitureChangeClick.CurrentFurniture == "chair_2(Clone)" || FurnitureChangeClick.CurrentFurniture == "Chair2_W(Clone)")
        {
            if (EventSystem.current.currentSelectedGameObject.name == "Option1")
            {
                Destroy(ChairPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Chair/Chair_01"), ChairPosition.transform);
                TempObject.transform.SetParent(ChairPosition.transform);
                ClosetNum = 1;
            }
            else if (EventSystem.current.currentSelectedGameObject.name == "Option2")
            {
                Destroy(ChairPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Chair/chair_2"), ChairPosition.transform);
                TempObject.transform.SetParent(ChairPosition.transform);
                ClosetNum = 2;
            }
            else if (EventSystem.current.currentSelectedGameObject.name == "Option3")
            {
                Destroy(ChairPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Chair/Chair2_W"), ChairPosition.transform);
                TempObject.transform.SetParent(ChairPosition.transform);
                ClosetNum = 3;
            }
        }
    }
}