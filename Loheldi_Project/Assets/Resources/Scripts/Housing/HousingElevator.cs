using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HousingElevator : MonoBehaviour
{
    public bool upstair = true;

    public GameObject Player;
    public GameObject Camera;
    public GameObject UpButton;
    public GameObject DownButton;
    public Camera TCam;
    public VirtualJoystick VJS;

    int a;

    public void Start()
    {
        a = PlayerPrefs.GetInt("HouseLv");
        if (a == 1)
        {
            this.GetComponent<Changing>().F1.SetActive(true);
            this.GetComponent<Changing>().F11.SetActive(true);
            this.GetComponent<Changing>().F2.SetActive(false);
            this.GetComponent<Changing>().F3.SetActive(false);
            this.GetComponent<Changing>().F31.SetActive(false);
            this.GetComponent<Changing>().F4.SetActive(false);
            Camera.transform.position = new Vector3(-21f, 5.5f, -4f);
        }
        else if (a >= 2)
        {
            this.GetComponent<Changing>().F1.SetActive(true);
            this.GetComponent<Changing>().F11.SetActive(false);
            this.GetComponent<Changing>().F2.SetActive(true);
            this.GetComponent<Changing>().F3.SetActive(false);
            this.GetComponent<Changing>().F31.SetActive(false);
            this.GetComponent<Changing>().F4.SetActive(false);
            Camera.transform.position = new Vector3(-20f, 6f, -2f);
        }
        Player.transform.position = new Vector3(-23.5f, -2f, -1f);
        if (a >= 3)
            CheckF();
        upstair = false;
    }
    public void GoUp()
    {
        Debug.Log(a);
        upstair = true;
        this.GetComponent<Changing>().F3.SetActive(true);
        if (a == 3)
        {
            this.GetComponent<Changing>().F31.SetActive(true);
            this.GetComponent<Changing>().F4.SetActive(false);
            Camera.transform.position = new Vector3(-22f, 8f, -3f);
        }
        else if (a == 4)
        {
            this.GetComponent<Changing>().F31.SetActive(false);
            this.GetComponent<Changing>().F4.SetActive(true);
            Camera.transform.position = new Vector3(-20.5f, 9f, -2f);
        }
        Player.transform.position = new Vector3(-23.5f, 3f, -1f);
        CheckF();
    }
    public void GoDown()
    {
        Debug.Log(a);
        upstair = false;
        this.GetComponent<Changing>().F3.SetActive(false);
        this.GetComponent<Changing>().F31.SetActive(false);
        this.GetComponent<Changing>().F4.SetActive(false);
        Camera.GetComponent<Camera>().enabled = true;
        VJS.TempInt = 1;
        TCam.enabled = false;
        Camera.transform.position = new Vector3(-20f, 6f, -2f);
        Player.transform.position = new Vector3(-23.5f, -2f, -1f);
        CheckF();
    }
    public void CheckF()
    {
        if (upstair) {
            UpButton.SetActive(false);
            DownButton.SetActive(true);
        }
        else {
            UpButton.SetActive(true);
            DownButton.SetActive(false);
        }
    }
}
