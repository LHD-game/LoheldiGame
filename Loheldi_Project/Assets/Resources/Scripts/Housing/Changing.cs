using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Changing : MonoBehaviour
{
    public GameObject TempObject;
    public GameObject BedPosition;
    public GameObject ClosetPosition;

    private int BedNum = 1;
    private int ClosetNum = 1;

    public void ButtonClick()
    {                                                           //해당 조건문 고민해볼것. (지금 누른 오브젝트의 이름을 비교해서 침대인지 아닌지 구분하는 조건문임)    P.S.가구에 부품들(옷장에 문짝이나, 서랍 등)도 추가하면 너무 복잡해지므로 콜리더 제거하거나 비활성화시키고 본체에 콜리더 조절하기
        if (FurnitureChangeClick.CurrentFurniture == "Bed" || FurnitureChangeClick.CurrentFurniture == "Bed_01_Single(Clone)" || FurnitureChangeClick.CurrentFurniture == "Bed_03_Double(Clone)" || FurnitureChangeClick.CurrentFurniture == "Sunbed_01(Clone)")
        {
            if (EventSystem.current.currentSelectedGameObject.name == "Option1")            //첫번째 옵션을 선택했다면
            {
                Destroy(BedPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefebs/Furniture/Beds/Bed_01_Single"), BedPosition.transform);  //해당 주소에 오브젝트를 생성해서 물러옴.
                TempObject.transform.SetParent(BedPosition.transform);                                                                //오브젝트를 BedPosition에 Child로 저장.
                BedNum = 1;
            }
            else if (EventSystem.current.currentSelectedGameObject.name == "Option2")       //두번째 옵션을 선택했다면
            {
                Destroy(BedPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefebs/Furniture/Beds/Bed_03_Double"), BedPosition.transform);  //해당 주소에 오브젝트를 생성해서 물러옴.
                TempObject.transform.SetParent(BedPosition.transform);                                                                //오브젝트를 BedPosition에 Child로 저장.
                BedNum = 2;
            }
            else if (EventSystem.current.currentSelectedGameObject.name == "Option3")       //세번째 옵션을 선택했다면
            {
                Destroy(BedPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefebs/Furniture/Beds/Sunbed_01"), BedPosition.transform);      //해당 주소에 오브젝트를 생성해서 물러옴.
                TempObject.transform.SetParent(BedPosition.transform);                                                                //오브젝트를 BedPosition에 Child로 저장.
                BedNum = 3;
            }
        }
                                                                //옷장
        if (FurnitureChangeClick.CurrentFurniture == "Closet" || FurnitureChangeClick.CurrentFurniture == "FurnitureSet_10_v_02(Clone)" || FurnitureChangeClick.CurrentFurniture == "FurnitureSet_11_v_11(Clone)" || FurnitureChangeClick.CurrentFurniture == "FurnitureSet_08_v_05(Clone)")
        {
            if (EventSystem.current.currentSelectedGameObject.name == "Option1")
            {
                Destroy(ClosetPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefebs/Furniture/FurnitureSets/FurnitureSet_10_v_02"), ClosetPosition.transform);
                TempObject.transform.SetParent(ClosetPosition.transform);
                ClosetNum = 1;
            }
            else if (EventSystem.current.currentSelectedGameObject.name == "Option2")
            {
                Destroy(ClosetPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefebs/Furniture/FurnitureSets/FurnitureSet_11_v_11"), ClosetPosition.transform);
                TempObject.transform.SetParent(ClosetPosition.transform);
                ClosetNum = 2;
            }
            else if (EventSystem.current.currentSelectedGameObject.name == "Option3")
            {
                Destroy(ClosetPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefebs/Furniture/FurnitureSets/FurnitureSet_08_v_05"), ClosetPosition.transform);
                TempObject.transform.SetParent(ClosetPosition.transform);
                ClosetNum = 3;
            }
        }
    }
}