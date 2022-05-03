using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FurnitureChangeClick : MonoBehaviour
{
    public GameObject Buttons; 
    public GameObject Bloker;
    public Camera getCamera;
    private bool ButtonToggle = false;
    private RaycastHit hit;
    public static string CurrentFurniture = "None";

    private bool housing=false;

    void Update()
    {
        if(housing)
        {
            if (Input.GetMouseButtonUp(0))
            {
                Ray ray = getCamera.ScreenPointToRay(Input.mousePosition);         //마우스 위치에 RRayCast사용하기
                if (Physics.Raycast(ray, out hit))                                      //만약 무언가를 눌랐다면
                {
                    /* if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject())        //UI을 누른게 아니라면
                     {
                         *//*Debug.Log(hit.collider.gameObject.name);
                         Buttons.SetActive(false);                                               //버튼 치우기
                         ButtonToggle = false;
                         CurrentFurniture = "None";*//*
                     }*/
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
                            Bloker.SetActive(true);
                            ButtonToggle = true;
                        }
                    }
                }
            }
        }
    }

    public void Reset()
    {
        Buttons.SetActive(false);
        Bloker.SetActive(false);//버튼 치우기
        ButtonToggle = false;
        CurrentFurniture = "None";
    }
    
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
