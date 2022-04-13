using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Changing : MonoBehaviour
{
    public GameObject TempObject;
    public GameObject BedPosition;

    public MeshFilter bedmesh;

    private int BedNum = 1;
    private int ClosetNum = 1;

    public void ButtonClick()
    {
        if (FurnitureChangeClick.CurrentFurniture == "Bed")                                 //선택한 가구가 침대일때
        {
            if (EventSystem.current.currentSelectedGameObject.name == "Option1")            //첫번째 옵션을 선택했다면
            {
                Destroy(BedPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefebs/Furniture/Beds/Bed_01_Single"), BedPosition.transform);  //해당 주소에 오브젝트를 물러옴.
                TempObject.transform.SetParent(BedPosition.transform);                         //오브젝트에 메쉬를 추출해서 넣음.
                BedNum = 1;
            }
            else if (EventSystem.current.currentSelectedGameObject.name == "Option2")       //두번째 옵션을 선택했다면
            {
                Destroy(BedPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefebs/Furniture/Beds/Bed_03_Double"), BedPosition.transform);  //해당 주소에 오브젝트를 물러옴.
                TempObject.transform.SetParent(BedPosition.transform);                         //오브젝트에 메쉬를 추출해서 넣음.
                BedNum = 2;
            }
            else if (EventSystem.current.currentSelectedGameObject.name == "Option2")       //세번째 옵션을 선택했다면
            {
                Destroy(BedPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefebs/Furniture/Beds/Sunbed_01"), BedPosition.transform);      //해당 주소에 오브젝트를 물러옴.
                TempObject.transform.SetParent(BedPosition.transform);                         //오브젝트에 메쉬를 추출해서 넣음.
                BedNum = 3;
            }
        }
    }
}