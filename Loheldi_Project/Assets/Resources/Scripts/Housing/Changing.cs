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
    {                                                           //�ش� ���ǹ� ����غ���. (���� ���� ������Ʈ�� �̸��� ���ؼ� ħ������ �ƴ��� �����ϴ� ���ǹ���)    P.S.������ ��ǰ��(���忡 ��¦�̳�, ���� ��)�� �߰��ϸ� �ʹ� ���������Ƿ� �ݸ��� �����ϰų� ��Ȱ��ȭ��Ű�� ��ü�� �ݸ��� �����ϱ�
        if (FurnitureChangeClick.CurrentFurniture == "Bed" || FurnitureChangeClick.CurrentFurniture == "Bed_01_Single(Clone)" || FurnitureChangeClick.CurrentFurniture == "Bed_03_Double(Clone)" || FurnitureChangeClick.CurrentFurniture == "Sunbed_01(Clone)")
        {
            if (EventSystem.current.currentSelectedGameObject.name == "Option1")            //ù��° �ɼ��� �����ߴٸ�
            {
                Destroy(BedPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefebs/Furniture/Beds/Bed_01_Single"), BedPosition.transform);  //�ش� �ּҿ� ������Ʈ�� �����ؼ� ������.
                TempObject.transform.SetParent(BedPosition.transform);                                                                //������Ʈ�� BedPosition�� Child�� ����.
                BedNum = 1;
            }
            else if (EventSystem.current.currentSelectedGameObject.name == "Option2")       //�ι�° �ɼ��� �����ߴٸ�
            {
                Destroy(BedPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefebs/Furniture/Beds/Bed_03_Double"), BedPosition.transform);  //�ش� �ּҿ� ������Ʈ�� �����ؼ� ������.
                TempObject.transform.SetParent(BedPosition.transform);                                                                //������Ʈ�� BedPosition�� Child�� ����.
                BedNum = 2;
            }
            else if (EventSystem.current.currentSelectedGameObject.name == "Option3")       //����° �ɼ��� �����ߴٸ�
            {
                Destroy(BedPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefebs/Furniture/Beds/Sunbed_01"), BedPosition.transform);      //�ش� �ּҿ� ������Ʈ�� �����ؼ� ������.
                TempObject.transform.SetParent(BedPosition.transform);                                                                //������Ʈ�� BedPosition�� Child�� ����.
                BedNum = 3;
            }
        }
                                                                //����
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