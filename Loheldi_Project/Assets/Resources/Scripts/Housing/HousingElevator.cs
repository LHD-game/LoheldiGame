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
    public void Start()
    {
        if (NowF == 1)
        {
            Camera.transform.position = new Vector3(-21f, 5.5f, -4f);
            Player.transform.position = new Vector3(-23.3f, -1.9f, -1f);
        }
        else if (NowF >= 2)
        {
            Camera.transform.position = new Vector3(-19f, 6.6f, -97.23f);
            Player.transform.position = new Vector3(-22.5f, -1.8f, -95f);
        }
        CheckF(NowF);
    }
    public void GoUp()
    {
        NowF++;
        if (NowF == 3)
        {
            Camera.transform.position = new Vector3(-20f, 6f, -169f);
            Player.transform.position = new Vector3(-22.5f, -1.8f, -166.2f);
        }
        else if (NowF == 4)
        {
            Camera.transform.position = new Vector3(-20f, 6.5f, -243.5f);
            Player.transform.position = new Vector3(-22.5f, -1.8f, -241.3f);
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
            Camera.transform.position = new Vector3(-19f, 6.6f, -97.23f);
            Player.transform.position = new Vector3(-22.5f, -1.8f, -95f);
        }
        else if (NowF == 3)
        {
            Camera.transform.position = new Vector3(-20f, 6f, -169f);
            Player.transform.position = new Vector3(-22.5f, -1.8f, -166.2f);
        }
        else if (NowF == 4)
        {
            Camera.transform.position = new Vector3(-20f, 6.5f, -243.5f);
            Player.transform.position = new Vector3(-22.5f, -1.8f, -241.3f);
        }
        CheckF(NowF);
    }
    public void CheckF(int NowF)
    {
        Debug.Log(NowF);
        int a = PlayerPrefs.GetInt("HouseLv");
        Debug.Log(a);
        if (a <= NowF)
            UpButton.SetActive(false);
        else
            UpButton.SetActive(true);
        if (NowF <= 2)
            DownButton.SetActive(false);
        else
            DownButton.SetActive(true);
    }
}
