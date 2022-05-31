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

    public GameObject SoundManager;

    public bool housing=false;
    private void Start()
    {
        SoundManager.GetComponent<SoundEffect>().Sound("BGMHouse");
    }
    void Update()
    {
        if(housing)
        {
            if (Input.GetMouseButtonUp(0))
            {
                Ray ray = getCamera.ScreenPointToRay(Input.mousePosition);         //���콺 ��ġ�� RRayCast����ϱ�
                if (Physics.Raycast(ray, out hit))                                      //���� ���𰡸� �����ٸ�
                {
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
                            Bloker.SetActive(true);
                            ButtonToggle = true;
                        }
                    }
                }
            }
        }
        SoundManager.GetComponent<SoundEffect>().Sound("GameFail");
    }

    public void Reset()
    {
        Buttons.SetActive(false);
        Bloker.SetActive(false);//��ư ġ���
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
