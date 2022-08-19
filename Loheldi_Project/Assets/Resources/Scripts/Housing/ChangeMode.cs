using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMode : MonoBehaviour
{
    public GameObject ExitButton;
    public GameObject Canvas1;
    public GameObject Canvas2;
    public GameObject Button;
    public GameObject Player;

    private FurnitureChangeClick HousingMode;

    public void Housing()
    {
        HousingMode = GameObject.Find("HousingSystem").GetComponent<FurnitureChangeClick>();
        ExitButton.SetActive(true);
        Canvas1.SetActive(true);
        Canvas2.SetActive(false);
        Button.SetActive(true);
        Player.SetActive(false);
        HousingMode.housing = true;
    }
    public void ExitHousing()
    {
        Button.transform.position = new Vector3(5000, 5000);
        HousingMode = GameObject.Find("HousingSystem").GetComponent<FurnitureChangeClick>();
        ExitButton.SetActive(false);
        Canvas1.SetActive(false);
        Canvas2.SetActive(true);
        Button.SetActive(false);
        Player.SetActive(true);
        HousingMode.housing = false;
    }
}
