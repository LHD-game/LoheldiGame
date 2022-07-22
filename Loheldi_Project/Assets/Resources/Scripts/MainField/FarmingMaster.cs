using BackEnd;
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmingMaster : MonoBehaviour
{
    public GameObject Interaction;

    public GameObject FarmContents;
    public Camera getCamera;
    private RaycastHit hit;
    public GameObject ChosenFarm = null;
    public GameObject Farms;
    public static string FarmnumtoBackend;

    [SerializeField]
    private GameObject Crops;

    JsonData myGarden_rows = new JsonData();
    public List<GameObject> Crops_list = new List<GameObject>();

    GameObject child;
    public class GardenData
    {
        public string G1;
        public DateTime G1Time;
        public string G2;
        public DateTime G2Time;
        public string G3;
        public DateTime G3Time;
        public string G4;
        public DateTime G4Time;
    }

    void Start()
    {
        var myGarden = Backend.GameData.GetMyData("USER_GARDEN", new Where(), 100);
        myGarden_rows = myGarden.GetReturnValuetoJSON()["rows"];
        ParsingJSON pj = new ParsingJSON();
        GardenData data = pj.ParseBackendData<GardenData>(myGarden_rows);

        if (data.G1 != null || data.G2 != null || data.G3 != null || data.G4 != null)
        {
            this.GetComponent<SeedSelect>().SelectedAuto(data.G1, data.G2, data.G3, data.G4, Farms);
        }
    }

    public void SeedList()
    {
        //�������� ������ FarmUI�� ����
        Debug.Log("���� ������Ʈ");
        this.GetComponent<InvenCategory>().superItem.Clear();
        this.GetComponent<InvenCategory>().gaguItem.Clear();
        this.GetComponent<InvenCategory>().cropsItem.Clear();
        this.GetComponent<InvenCategory>().GetChartContents("55031");
        this.GetComponent<InvenCategory>().MakeCategoryforFarming(FarmContents, this.GetComponent<InvenCategory>().superItem, this.GetComponent<InvenCategory>().super_list);
    }

    void Update()
    {
        if (Interaction.GetComponent<Interaction>().Farm)
        {
            if (Input.GetMouseButtonUp(0))
            {
                Ray ray = getCamera.ScreenPointToRay(Input.mousePosition);         //���콺 ��ġ�� RayCast����ϱ�
                if (Physics.Raycast(ray, out hit))                                      //���� ���𰡸� �����ٸ�
                {
                    if (hit.collider.gameObject.tag == "Farm")                          //���� �����Ŷ��
                    {
                        if (ChosenFarm == Farms.transform.GetChild(0))
                            FarmnumtoBackend = "G1";
                        else if (ChosenFarm == Farms.transform.GetChild(1))
                            FarmnumtoBackend = "G2";
                        else if (ChosenFarm == Farms.transform.GetChild(2))
                            FarmnumtoBackend = "G3";
                        else if (ChosenFarm == Farms.transform.GetChild(3))
                            FarmnumtoBackend = "G4";
                        ChosenFarm = hit.collider.gameObject;                      //�ش� �� �����ϱ�
                    }
                }
            }
        }
    }
}
