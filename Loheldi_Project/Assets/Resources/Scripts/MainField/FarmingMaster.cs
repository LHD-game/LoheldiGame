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
                        ChosenFarm = hit.collider.gameObject;                      //�ش� �� �����ϱ�
                    }
                }
            }
        }
    }
}
