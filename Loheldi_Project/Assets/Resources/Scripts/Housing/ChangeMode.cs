using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMode : MonoBehaviour
{
    public GameObject ExitButton;
    public GameObject Canvas;
    public GameObject Player;
    public Camera HousingCamera;
    //public Camera followCamera;

    private FurnitureChangeClick HousingMode;

    public void Housing()
    {
        HousingMode = GameObject.Find("HousingSystem").GetComponent<FurnitureChangeClick>();
        ExitButton.SetActive(true);
        Canvas.SetActive(false);
        Player.SetActive(false);
        HousingMode.housing = true;
        HousingCamera.enabled = true;
        //followCamera.enabled = false;
    }
    public void ExitHousing()
    {
        HousingMode = GameObject.Find("HousingSystem").GetComponent<FurnitureChangeClick>();
        ExitButton.SetActive(false);
        Canvas.SetActive(true);
        Player.SetActive(true);
        HousingMode.housing = false;
        HousingCamera.enabled = false;
        //followCamera.enabled = true;
    }
}
