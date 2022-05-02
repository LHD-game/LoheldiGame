using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FurnitureChangeClick : MonoBehaviour
{
    public GameObject Buttons;
    public Camera getCamera;
    private bool ButtonToggle = false;
    private RaycastHit hit;
    public static string CurrentFurniture = "None";

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = getCamera.ScreenPointToRay(Input.mousePosition);         //���콺 ��ġ�� RRayCast����ϱ�
            if (Physics.Raycast(ray, out hit))                                      //���� ���𰡸� �����ٸ�
            {
                if (CurrentFurniture != "Button")                                       //��ư�� ������ �ƴ϶��
                {
                    Buttons.SetActive(false);                                               //��ư ġ���
                    ButtonToggle = false;
                    CurrentFurniture = "None";
                }

                if (hit.collider.gameObject.tag == "ChangeableFurniture")           //������ �����Ŷ��
                {
                    if (CurrentFurniture == "None")                                     //�׶� ���õ� ������ ���ٸ�
                    {
                        Debug.Log(hit.collider.gameObject.name);
                        CurrentFurniture = hit.collider.gameObject.name;                      //�ش� ���� �����ϱ�
                    }
                    if (!ButtonToggle)                                                  //�̶� ��ư�� ���ٸ�
                    {
                        Buttons.transform.position = Input.mousePosition;                   //��ư�� Ŭ���� ��ġ�� �����ϱ�
                        Buttons.SetActive(true);
                        ButtonToggle = true;
                    }
                }
            }
        }
    }
}
