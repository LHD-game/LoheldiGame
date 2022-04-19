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
            Ray ray = getCamera.ScreenPointToRay(Input.mousePosition);         //마우스 위치에 RRayCast사용하기
            if (Physics.Raycast(ray, out hit))                                      //만약 무언가를 눌랐다면
            {
                if (CurrentFurniture != "Button")                                       //버튼을 누른게 아니라면
                {
                    Buttons.SetActive(false);                                               //버튼 치우기
                    ButtonToggle = false;
                    CurrentFurniture = "None";
                }

                if (hit.collider.gameObject.tag == "ChangeableFurniture")           //가구를 누른거라면
                {
                    if (CurrentFurniture == "None")                                     //그때 선택된 가구가 없다면
                    {
                        Debug.Log(hit.collider.gameObject.name);
                        CurrentFurniture = hit.collider.gameObject.name;                      //해당 가구 선택하기
                    }
                    if (!ButtonToggle)                                                  //이때 버튼이 없다면
                    {
                        Buttons.transform.position = Input.mousePosition;                   //버튼을 클릭한 위치에 생성하기
                        Buttons.SetActive(true);
                        ButtonToggle = true;
                    }
                }
            }
        }
    }
}
