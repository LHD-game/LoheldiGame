using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Changing : MonoBehaviour
{
    public GameObject TempObject;
    public GameObject BedPosition;

    private int BedNum = 1;
    private int ClosetNum = 1;

    public void ButtonClick()
    {                                                           //해당 조건문 고민해볼것. (지금 누른 오브젝트의 이름을 비교해서 침대인지 아닌지 구분하는 조건문임)
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
    }
}