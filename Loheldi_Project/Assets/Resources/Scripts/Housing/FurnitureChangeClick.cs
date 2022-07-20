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
    public static string CurrentFurniture = "None";

    public string ItemType = "aa";

    public bool housing=false;
    private void Start()
    {
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
                    Debug.Log(hit.collider.gameObject.name);
                    if (hit.collider.gameObject.tag == "ChangeableFurniture")           //가구를 누른거라면
                    {
                        Debug.Log(hit.collider.gameObject.name);
                        CurrentFurniture = hit.collider.gameObject.name;                      //해당 가구 선택하기
                        if (CurrentFurniture == "Bed_01_Single" || CurrentFurniture == "Bed_01_Single(Clone)" || CurrentFurniture == "Bed_Single(Clone)" || CurrentFurniture == "Bed_W(Clone)" || CurrentFurniture == "Bed_K(Clone)")
                        {
                            ItemType = "bed";
                        }
                        if (CurrentFurniture == "FurnitureSet_10_v_02" || CurrentFurniture == "wardrobe(Clone)" || CurrentFurniture == "Wardrobe_W(Clone)" || CurrentFurniture == "FurnitureSet_10_v_02(Clone)" || CurrentFurniture == "Wardrobe_K(Clone)")
                        {
                            ItemType = "closet";
                        }
                        if (CurrentFurniture == "Table_01" || CurrentFurniture == "Table_01(Clone)" || CurrentFurniture == "table_2(Clone)" || CurrentFurniture == "Table4_W(Clone)" || CurrentFurniture == "table2_K(Clone)")
                        {
                            ItemType = "desk";
                        }
                        if (CurrentFurniture == "Chair_01" || CurrentFurniture == "Chair_01(Clone)" || CurrentFurniture == "chair_2(Clone)" || CurrentFurniture == "Chair2_W(Clone)" || CurrentFurniture == "Chair_K(Clone)")
                        {
                            ItemType = "chair";
                        }
                        if (!ButtonToggle)                                                  //이때 버튼이 없다면
                        {
                            this.GetComponent<InvenCategory>().superItem.Clear();
                            this.GetComponent<InvenCategory>().gaguItem.Clear();
                            this.GetComponent<InvenCategory>().cropsItem.Clear();
                            this.GetComponent<InvenCategory>().GetChartContents("55031");
                            this.GetComponent<InvenCategory>().MakeCategoryforHousing(Contents, this.GetComponent<InvenCategory>().gaguItem, this.GetComponent<InvenCategory>().gagu_list, ItemType);
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
