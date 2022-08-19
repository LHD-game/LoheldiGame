using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HousingElevator : MonoBehaviour
{
    public int NowF = 2;

    public GameObject Player;
    public GameObject Camera;
    public GameObject UpButton;
    public GameObject DownButton;
    public void GoUp()
    {
        NowF++;
        if (NowF == 3)
        {
            Camera.transform.position = new Vector3(-20f, 6f, -169f);
            Player.transform.position = new Vector3(-22.5f, -1.8f, -95.2f);
        }
        else if (NowF == 4)
        {
            Camera.transform.position = new Vector3(-20f, 6.5f, -243.5f);
            Player.transform.position = new Vector3(-22.5f, -1.8f, -165f);
        }
        else if (NowF == 5)
        {
            Camera.transform.position = new Vector3(-20f, 6.5f, -307f);
            Player.transform.position = new Vector3(-22.5f, -1.8f, -300f);
        }
        CheckF(NowF);
    }
    public void GoDown()
    {
        NowF--;
        if (NowF == 2)
        {
            Camera.transform.position = new Vector3(-18.5f, 8.5f, -70f);
            Player.transform.position = new Vector3(-22.5f, -1.8f, -67.5f);
        }
        else if (NowF == 3)
        {
            Camera.transform.position = new Vector3(-20f, 6f, -169f);
            Player.transform.position = new Vector3(-22.5f, -1.8f, -95.2f);
        }
        else if (NowF == 4)
        {
            Camera.transform.position = new Vector3(-20f, 6.5f, -243.5f);
            Player.transform.position = new Vector3(-22.5f, -1.8f, -165f);
        }
        CheckF(NowF);
    }
    public void CheckF(int NowF)
    {
        int a = PlayerPrefs.GetInt("HouseLv");
        if (a == NowF)
            UpButton.SetActive(false);
        else
            UpButton.SetActive(true);
        if (NowF == 2 || NowF == 1)
            UpButton.SetActive(false);
        else
            UpButton.SetActive(true);
    }
}
