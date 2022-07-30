using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CropsSell : MonoBehaviour
{
    public GameObject getCamera;
    private int TempInt;
    private RaycastHit hit;

    public void Start()
    {
        StartCoroutine("harvest");
    }
    IEnumerator harvest()//void Update()
    {
        while (true)
        {
            //Debug.Log("�� ������Ʈ ���ư��� ��");
            if (Input.GetMouseButtonUp(0))
            {
                getCamera = GameObject.Find("FarmCamera");
                Ray ray = getCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);         //���콺 ��ġ�� RayCast����ϱ�
                if (Physics.Raycast(ray, out hit))                                      //���� ���𰡸� �����ٸ�
                {
                    if (hit.collider.gameObject == this.gameObject)                          //���� �����Ŷ��
                    {
                        GardenControl GardenControl = GameObject.Find("InventoryManager").GetComponent<GardenControl>();
                        GardenControl.HarvestCrops(int.Parse(transform.parent.GetComponent<Text>().text));
                        TempInt = Random.Range(0, 2);
                        if (TempInt == 0)
                            PlayInfoManager.GetCoin(3);
                        if (TempInt == 1)
                            PlayInfoManager.GetCoin(10);
                        if (TempInt == 2)
                            PlayInfoManager.GetCoin(18);
                        MainGameManager.SingletonInstance.UpdateField();
                        Destroy(this.gameObject);
                    }
                }
            }
            yield return null;
        }
    }
}
