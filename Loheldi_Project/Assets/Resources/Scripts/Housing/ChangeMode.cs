using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMode : MonoBehaviour
{
    public GameObject ExitButton;
    public GameObject Canvas1;
    public GameObject Canvas2;
    public GameObject Player;/*
    public Camera HousingCamera;
    public Camera followCamera;*/

    private FurnitureChangeClick HousingMode;

    public void Housing()
    {
        HousingMode = GameObject.Find("HousingSystem").GetComponent<FurnitureChangeClick>();
        ExitButton.SetActive(true);
        Canvas1.SetActive(true);
        Canvas2.SetActive(false);
        Player.SetActive(false);
        HousingMode.housing = true;/*
        followCamera.enabled = false;
        HousingCamera.enabled = true;*/
    }
    public void ExitHousing()
    {
        HousingMode = GameObject.Find("HousingSystem").GetComponent<FurnitureChangeClick>();
        ExitButton.SetActive(false);
        Canvas1.SetActive(false);
        Canvas2.SetActive(true);
        Player.SetActive(true);
        HousingMode.housing = false;/*
        followCamera.enabled = true;
        HousingCamera.enabled = false;*/
    }
}
