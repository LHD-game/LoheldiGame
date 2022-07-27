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
                Ray ray = getCamera.ScreenPointToRay(Input.mousePosition);         //���콺 ��ġ�� RayCast����ϱ�
                if (Physics.Raycast(ray, out hit))                                      //���� ���𰡸� �����ٸ�
                {
                    Debug.Log(hit.collider.gameObject.name);
                    if (hit.collider.gameObject.tag == "ChangeableFurniture")           //������ �����Ŷ��
                    {
                        Debug.Log(hit.collider.gameObject.name);
                        CurrentFurniture = hit.collider.gameObject;                      //�ش� ���� �����ϱ�
                        if (CurrentFurniture.GetComponent<Text>().text == "bed")
                        {
                            ItemType = "bed";
                        }
                        if (CurrentFurniture.GetComponent<Text>().text == "closet")
                        {
                            ItemType = "closet";
                        }
                        if (CurrentFurniture.GetComponent<Text>().text == "table")
                        {
                            ItemType = "desk";
                        }
                        if (CurrentFurniture.GetComponent<Text>().text == "chair")
                        {
                            ItemType = "chair";
                        }
                        if (!ButtonToggle)                                                  //�̶� ��ư�� ���ٸ�
                        {

                            this.GetComponent<HousingCategory>().gaguItem.Clear();
                            this.GetComponent<HousingCategory>().GetChartContents("55031");
                            this.GetComponent<HousingCategory>().MakeCategory(Contents, this.GetComponent<HousingCategory>().gaguItem, this.GetComponent<HousingCategory>().gagu_list, ItemType);
                            Buttons.transform.position = Input.mousePosition;                   //��ư�� Ŭ���� ��ġ�� �����ϱ�
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
        Bloker.SetActive(false);//��ư ġ���
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
