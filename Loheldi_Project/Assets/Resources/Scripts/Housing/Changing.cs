using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Changing : MonoBehaviour
{
    public GameObject TempObject;
    public GameObject BedPosition;
    public GameObject TablePosition;
    public GameObject ChairPosition;
    public GameObject ClosetPosition;
    public Text ItemCode;

    private string BedCode = "";
    private string ClosetCode = "";
    private string TableCode = "";
    private string ChairCode = "";

    private bool Starting;

    public void Start()
    {
        Starting = true;

        BedPosition = GameObject.Find("BedPosition");
        TablePosition = GameObject.Find("TablePosition");
        ChairPosition = GameObject.Find("ChairPosition");
        ClosetPosition = GameObject.Find("ClosetPosition");

        ItemCode.text = PlayerPrefs.GetString("bed");
        if (ItemCode.text != "")
            ButtonClick();
        ItemCode.text = PlayerPrefs.GetString("closet");
        if (ItemCode.text != "")
            ButtonClick();
        ItemCode.text = PlayerPrefs.GetString("table");
        if (ItemCode.text != "")
            ButtonClick();
        ItemCode.text = PlayerPrefs.GetString("chair");
        if (ItemCode.text != "")
            ButtonClick();
        Starting = false;
    }

    public void ButtonClick()
    {
        if (FurnitureChangeClick.CurrentFurniture == "Bed_01_Single" || FurnitureChangeClick.CurrentFurniture == "Bed_01_Single(Clone)" || FurnitureChangeClick.CurrentFurniture == "Bed_Single(Clone)" || FurnitureChangeClick.CurrentFurniture == "Bed_W(Clone)" || FurnitureChangeClick.CurrentFurniture == "Bed_K(Clone)")
        {
            if (ItemCode.text == "2010101")            //ù��° �ɼ��� �����ߴٸ�
            {
                Destroy(BedPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Beds/Bed_W"), BedPosition.transform);  //�ش� �ּҿ� ������Ʈ�� �����ؼ� ������.
                TempObject.transform.SetParent(BedPosition.transform);                                                                //������Ʈ�� BedPosition�� Child�� ����.
            }
            else if (ItemCode.text == "2020101")       //�ι�° �ɼ��� �����ߴٸ�
            {
                Destroy(BedPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Beds/Bed_Single"), BedPosition.transform);  //�ش� �ּҿ� ������Ʈ�� �����ؼ� ������.
                TempObject.transform.SetParent(BedPosition.transform);                                                                //������Ʈ�� BedPosition�� Child�� ����.
            }
            else if (ItemCode.text == "2030101")       //����° �ɼ��� �����ߴٸ�
            {
                Destroy(BedPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Beds/Bed_K"), BedPosition.transform);      //�ش� �ּҿ� ������Ʈ�� �����ؼ� ������.
                TempObject.transform.SetParent(BedPosition.transform);                                                                //������Ʈ�� BedPosition�� Child�� ����.
            }
            else if (ItemCode.text == "2040101")       //�׹�° �ɼ��� �����ߴٸ�
            {
                /*Destroy(BedPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Beds/"), BedPosition.transform);      //�ش� �ּҿ� ������Ʈ�� �����ؼ� ������.
                TempObject.transform.SetParent(BedPosition.transform);*/                                                                //������Ʈ�� BedPosition�� Child�� ����
            }
            BedCode = ItemCode.text;
        }
                                                                //����
        if (FurnitureChangeClick.CurrentFurniture == "FurnitureSet_10_v_02" || FurnitureChangeClick.CurrentFurniture == "wardrobe(Clone)" || FurnitureChangeClick.CurrentFurniture == "Wardrobe_W(Clone)" || FurnitureChangeClick.CurrentFurniture == "FurnitureSet_10_v_02(Clone)" || FurnitureChangeClick.CurrentFurniture == "Wardrobe_K(Clone)")
        {
            if (ItemCode.text == "2010102")            //ù��° �ɼ��� �����ߴٸ�
            {
                Destroy(ClosetPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/FurnitureSets/Wardrobe_W"), ClosetPosition.transform);
                TempObject.transform.SetParent(ClosetPosition.transform);
            }
            else if (ItemCode.text == "2020102")       //�ι�° �ɼ��� �����ߴٸ�
            {
                Destroy(ClosetPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/FurnitureSets/wardrobe"), ClosetPosition.transform);
                TempObject.transform.SetParent(ClosetPosition.transform);
            }
            else if (ItemCode.text == "2030102")       //����° �ɼ��� �����ߴٸ�
            {
                Destroy(ClosetPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/FurnitureSets/Wardrobe_K"), ClosetPosition.transform);
                TempObject.transform.SetParent(ClosetPosition.transform);
            }
            else if (ItemCode.text == "2040102")       //�׹�° �ɼ��� �����ߴٸ�
            {
                /*Destroy(ClosetPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/FurnitureSets/"), ClosetPosition.transform);
                TempObject.transform.SetParent(ClosetPosition.transform);*/
            }
            ClosetCode = ItemCode.text;
        }
                                                                //å��
        if (FurnitureChangeClick.CurrentFurniture == "Table_01" || FurnitureChangeClick.CurrentFurniture == "Table_01(Clone)" || FurnitureChangeClick.CurrentFurniture == "table_2(Clone)" || FurnitureChangeClick.CurrentFurniture == "Table4_W(Clone)" || FurnitureChangeClick.CurrentFurniture == "table2_K(Clone)")
        {
            if (ItemCode.text == "2010204")            //ù��° �ɼ��� �����ߴٸ�
            {
                Destroy(TablePosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Table/Table4_W"), TablePosition.transform);
                TempObject.transform.SetParent(TablePosition.transform);
            }
            else if (ItemCode.text == "2020204")       //�ι�° �ɼ��� �����ߴٸ�
            {
                Destroy(TablePosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Table/Table_2"), TablePosition.transform);
                TempObject.transform.SetParent(TablePosition.transform);
            }
            else if (ItemCode.text == "2030204")       //����° �ɼ��� �����ߴٸ�
            {
                Destroy(TablePosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Table/table2_K"), TablePosition.transform);
                TempObject.transform.SetParent(TablePosition.transform);
            }
            else if (ItemCode.text == "2040203")       //�׹�° �ɼ��� �����ߴٸ�
            {
                /*Destroy(TablePosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Table/"), TablePosition.transform);
                TempObject.transform.SetParent(TablePosition.transform);*/
            }
            TableCode = ItemCode.text;
        }
                                                                //����
        if (FurnitureChangeClick.CurrentFurniture == "Chair_01" || FurnitureChangeClick.CurrentFurniture == "Chair_01(Clone)" || FurnitureChangeClick.CurrentFurniture == "chair_2(Clone)" || FurnitureChangeClick.CurrentFurniture == "Chair2_W(Clone)" || FurnitureChangeClick.CurrentFurniture == "Chair_K(Clone)")
        {
            if (ItemCode.text == "2010205")            //ù��° �ɼ��� �����ߴٸ�
            {
                Destroy(ChairPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Chair/Chair2_W"), ChairPosition.transform);
                TempObject.transform.SetParent(ChairPosition.transform);
            }
            else if (ItemCode.text == "2020205")       //�ι�° �ɼ��� �����ߴٸ�
            {
                Destroy(ChairPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Chair/chair_2"), ChairPosition.transform);
                TempObject.transform.SetParent(ChairPosition.transform);
            }
            else if (ItemCode.text == "2030205")       //����° �ɼ��� �����ߴٸ�
            {
                Destroy(ChairPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>("Prefabs/Furniture/Chair/Chair_K"), ChairPosition.transform);
                TempObject.transform.SetParent(ChairPosition.transform);
            }
            else if (ItemCode.text == "2040204")       //�׹�° �ɼ��� �����ߴٸ�
            {
                /*Destroy(ChairPosition.transform.GetChild(0).gameObject);
                TempObject = Instantiate(Resources.Load<GameObject>(""Prefabs/Furniture/Chair/"), ChairPosition.transform);
                TempObject.transform.SetParent(ChairPosition.transform);*/
            }
            ChairCode = ItemCode.text;
        }
        if (!Starting)
        {
            Param param = new Param();
            param.Add("bed", ChairCode);
            param.Add("closet", ClosetCode);
            param.Add("table", TableCode);
            param.Add("chait", ChairCode);

            //���� ���� row �˻�
            var bro = Backend.GameData.Get("USER_HOUSE", new Where());
            string rowIndate = bro.FlattenRows()[0]["inDate"].ToString();

            //�ش� row�� ���� update
            var bro2 = Backend.GameData.UpdateV2("USER_HOUSE", rowIndate, Backend.UserInDate, param);

            if (bro2.IsSuccess())
            {
                Debug.Log("SaveUserHousing ����. USER_HOUSE ������Ʈ �Ǿ����ϴ�.");
            }
            else
            {
                Debug.Log("SaveUserHousing ����.");
            }
        }
    }
}