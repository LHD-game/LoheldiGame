using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropsSell : MonoBehaviour
{
    public GameObject getCamera;
    private int TempInt;
    private RaycastHit hit;

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            getCamera = GameObject.Find("FarmCamera");
            Ray ray = getCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);         //���콺 ��ġ�� RayCast����ϱ�
            if (Physics.Raycast(ray, out hit))                                      //���� ���𰡸� �����ٸ�
            {
                if (hit.collider.gameObject == this.gameObject)                          //���� �����Ŷ��
                {
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
    }
}
