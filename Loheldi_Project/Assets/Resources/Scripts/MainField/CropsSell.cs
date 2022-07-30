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
            //Debug.Log("밭 업데이트 돌아가는 중");
            if (Input.GetMouseButtonUp(0))
            {
                getCamera = GameObject.Find("FarmCamera");
                Ray ray = getCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);         //마우스 위치에 RayCast사용하기
                if (Physics.Raycast(ray, out hit))                                      //만약 무언가를 눌랐다면
                {
                    if (hit.collider.gameObject == this.gameObject)                          //밭을 누른거라면
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
