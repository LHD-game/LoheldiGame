using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FurnitureChangeClick : MonoBehaviour
{
    public GameObject Buttons;
    public GameObject Contents;
    public GameObject Bloker;
    public Camera getCamera;
    private bool ButtonToggle = false;
    private RaycastHit hit;
    public static GameObject CurrentFurniture;

    public string ItemType = "aa";

    public bool housing=false;
    private void Start()
    {
        CurrentFurniture = GameObject.Find("TempObjectforLoad");
        this.GetComponent<Changing>().FirstSetting();
    }
    void Update()
    {
        if(housing)
        {
            if (Input.GetMouseButtonUp(0))
            {
                Ray ray = getCamera.ScreenPointToRay(Input.mousePosition);         //마우스 위치에 RayCast사용하기
                if (Physics.Raycast(ray, out hit))                                      //만약 무언가를 눌랐다면
                {
                    if (hit.collider.gameObject.tag == "ChangeableFurniture")           //가구를 누른거라면
                    {
                        CurrentFurniture = hit.collider.gameObject;                      //해당 가구 선택하기
                        Debug.Log(CurrentFurniture.name);
                        if (CurrentFurniture.GetComponent<Text>().text == "bed")
                        {
                            ItemType = "bed";
                        }
                        if (CurrentFurniture.GetComponent<Text>().text == "closet")
                        {
                            ItemType = "closet";
                        }
                        if (CurrentFurniture.GetComponent<Text>().text == "bookshelf")
                        {
                            ItemType = "bookshelf";
                        }
                        if (CurrentFurniture.GetComponent<Text>().text == "desk")
                        {
                            ItemType = "desk";
                        }
                        if (CurrentFurniture.GetComponent<Text>().text == "table")
                        {
                            ItemType = "meal table";
                        }
                        if (CurrentFurniture.GetComponent<Text>().text == "sidetable")
                        {
                            ItemType = "side table";
                        }
                        if (CurrentFurniture.GetComponent<Text>().text == "chair" || CurrentFurniture.GetComponent<Text>().text == "chair2" || CurrentFurniture.GetComponent<Text>().text == "chair3")
                        {
                            ItemType = "chair";
                        }
                        if (CurrentFurniture.GetComponent<Text>().text == "sunbed" || CurrentFurniture.GetComponent<Text>().text == "sunbed2")
                        {
                            ItemType = "sunbed";
                        }
                        if (CurrentFurniture.GetComponent<Text>().text == "kitchen")
                        {
                            ItemType = "sink";
                        }
                        if (CurrentFurniture.GetComponent<Text>().text == "wallshelf")
                        {
                            ItemType = "cupboard";
                        }
                        if (CurrentFurniture.GetComponent<Text>().text == "fridge")
                        {
                            ItemType = "refrigerator";
                        }
                        if (CurrentFurniture.GetComponent<Text>().text == "standingsink")
                        {
                            ItemType = "washstand";
                        }
                        if (CurrentFurniture.GetComponent<Text>().text == "coffeeTable")
                        {
                            ItemType = "table";
                        }
                        if (CurrentFurniture.GetComponent<Text>().text == "sofa")
                        {
                            ItemType = "sofa";
                        }
                        Debug.Log(CurrentFurniture.GetComponent<Text>().text);
                        if (!ButtonToggle)                                                  //이때 버튼이 없다면
                        {
                            this.GetComponent<HousingCategory>().gaguItem.Clear();
                            this.GetComponent<HousingCategory>().GetChartContents(ChartNum.AllItemChart);
                            Debug.Log(ItemType);
                            this.GetComponent<HousingCategory>().MakeCategory(Contents, this.GetComponent<HousingCategory>().gaguItem, this.GetComponent<HousingCategory>().gagu_list, ItemType);
                            Buttons.transform.position = Input.mousePosition;                   //버튼을 클릭한 위치에 생성하기
                            Buttons.SetActive(true);
                            Bloker.SetActive(true);
                            ButtonToggle = true;
                        }
                    }
                    else
                    {
                        foreach (Transform child in Contents.transform)
                        {
                            Destroy(child.gameObject);
                        }
                        Buttons.SetActive(false);
                        Bloker.SetActive(false);
                        ButtonToggle = false;
                        Reset();
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
        CurrentFurniture = null;
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
