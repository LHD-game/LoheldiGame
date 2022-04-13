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
            Ray ray = getCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (CurrentFurniture != "Button")
                {
                    Buttons.SetActive(false);
                    ButtonToggle = false;
                    CurrentFurniture = "None";
                }

                if (hit.collider.gameObject.tag == "ChangeableFurniture")
                {
                    if (CurrentFurniture == "None")
                    {
                        Debug.Log(hit.collider.gameObject.name);
                        CurrentFurniture = hit.collider.gameObject.name;
                    }
                    if (!ButtonToggle)
                    {
                        Buttons.transform.position = Input.mousePosition;
                        Buttons.SetActive(true);
                        ButtonToggle = true;
                    }
                }
            }
        }
    }
}
