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
    {                                                           //�ش� ���ǹ� ����غ���. (���� ���� ������Ʈ�� �̸��� ���ؼ� ħ������ �ƴ��� �����ϴ� ���ǹ���)    P.S.������ ��ǰ��(���忡 ��¦�̳�, ���� ��)�� �߰��ϸ� �ʹ� ���������Ƿ� �ݸ��� �����ϰų� ��Ȱ��ȭ��Ű�� ��ü�� �ݸ��� �����ϱ�
        if (FurnitureChangeClick.CurrentFurniture == "Bed_01_Single" || FurnitureChangeClick.CurrentFurniture == "Bed_01_Single(Clone)" || FurnitureChangeClick.CurrentFurniture == "Bed_Single(Clone)" || FurnitureChangeClick.CurrentFurniture == "Bed_W(Clone)")
        {
            if (EventSystem.current.currentSelectedGameObject.name == "Option1")            //ù��° �ɼ��� �����ߴٸ�
            {
                Destroy(BedPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Beds/Bed_01_Single"), BedPosition.transform);  //�ش� �ּҿ� ������Ʈ�� �����ؼ� ������.
                TempObject.transform.SetParent(BedPosition.transform);                                                                //������Ʈ�� BedPosition�� Child�� ����.
                BedNum = 1;
            }
            else if (EventSystem.current.currentSelectedGameObject.name == "Option2")       //�ι�° �ɼ��� �����ߴٸ�
            {
                Destroy(BedPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Beds/Bed_Single"), BedPosition.transform);  //�ش� �ּҿ� ������Ʈ�� �����ؼ� ������.
                TempObject.transform.SetParent(BedPosition.transform);                                                                //������Ʈ�� BedPosition�� Child�� ����.
                BedNum = 2;
            }
            else if (EventSystem.current.currentSelectedGameObject.name == "Option3")       //����° �ɼ��� �����ߴٸ�
            {
                Destroy(BedPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Beds/Bed_W"), BedPosition.transform);      //�ش� �ּҿ� ������Ʈ�� �����ؼ� ������.
                TempObject.transform.SetParent(BedPosition.transform);                                                                //������Ʈ�� BedPosition�� Child�� ����.
                BedNum = 3;
            }
        }
                                                                //����
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
                                                                //å��
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
                                                                //å��
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