using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMode : MonoBehaviour
{
    public GameObject ExitButton;
    public GameObject Canvas1;
    public GameObject Canvas2;
    public GameObject Player;
    public Camera Camera1;
    public Camera Camera2;

    private FurnitureChangeClick HousingMode;

    public void changeCamera()
    {
        Debug.Log("함수실행");
        if (Camera1.enabled == true)
        {
            Debug.Log("변경 두두둔");
            Camera1.enabled = false;
            Camera2.enabled = true;
        }
        else
        {

            Debug.Log("변경 두두둔2");
            Camera2.enabled = false;
            Camera1.enabled = true;
        }
    }
    public void Housing()
    {
        HousingMode = GameObject.Find("HousingSystem").GetComponent<FurnitureChangeClick>();
        ExitButton.SetActive(true);
        Canvas1.SetActive(true);
        Canvas2.SetActive(false);
        Player.SetActive(false);
        HousingMode.housing = true;
        if (Camera1.enabled == true) //부엌에 있을 때
            ;
        else
            changeCamera();
        //Camera2.enabled = false;
        //Camera1.enabled = true;
    }
    public void ExitHousing()
    {
        HousingMode = GameObject.Find("HousingSystem").GetComponent<FurnitureChangeClick>();
        ExitButton.SetActive(false);
        Canvas1.SetActive(false);
        Canvas2.SetActive(true);
        Player.SetActive(true);
        HousingMode.housing = false;
        //Camera2.enabled = true;
        //Camera1.enabled = false;
    }
}
