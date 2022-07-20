using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmingMaster : MonoBehaviour
{
    public GameObject Interaction;

    public GameObject FarmContents;
    public Camera getCamera;
    private RaycastHit hit;
    public GameObject ChosenFarm;

    public string ItemType = "seed";

    public void SeedList()
    {
        //서버에서 가져와 FarmUI에 띄우기
        Debug.Log("씨앗 업데이트");
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
                Ray ray = getCamera.ScreenPointToRay(Input.mousePosition);         //마우스 위치에 RayCast사용하기
                if (Physics.Raycast(ray, out hit))                                      //만약 무언가를 눌랐다면
                {
                    if (hit.collider.gameObject.tag == "Farm")                          //밭을 누른거라면
                    {
                        ChosenFarm = hit.collider.gameObject;                      //해당 밭 선택하기
                    }
                }
            }
        }
    }
}
