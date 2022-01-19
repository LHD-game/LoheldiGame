using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToothGameManager : MonoBehaviour
{
    public Text timer;
    private float time;
    public static int difficulty = 3;
    public static int k = 0;

    public static GameObject Player;

    public GameObject BlackBox_1;
    public GameObject BlackBox_2;
    public GameObject BlackBox_3;
    public GameObject BlackBox_4;
    public GameObject BlackBox_5;
    public GameObject BlackBox_6;
    public GameObject BlackBox_7;
    public GameObject BlackBox_8;
    public GameObject BlackBox_9;
    public GameObject BlackBox_10;
    public GameObject BlackBox_11;
    public GameObject BlackBox_12;
    public GameObject BlackBox_13;
    public GameObject BlackBox_14;
    int a = 0;

    public int BlackCount = 0;

    public GameObject WinText;
    public GameObject falseText;
    public GameObject ReturnButton;
    void Start()
    {
        Time.timeScale = 1;
    }

    void Update()
    {
        if (ToothCountDown.CountEnd == true)
        {
            if (k == 0)
            {
                if (difficulty == 1)
                {
                    time = 30f;
                    InvokeRepeating("BoxRandom", 3.0f, 3.0f);
                }
                if (difficulty == 2)
                {
                    time = 45f;
                    InvokeRepeating("BoxRandom", 2.25f, 2.25f);
                }
                if (difficulty == 3)
                {
                    time = 60f;
                    InvokeRepeating("BoxRandom", 2.0f, 2.0f);
                }
                k = 1;
            }
            time -= Time.deltaTime;
            timer.text = string.Format("{0:N2}", time);
            if (time <= 0)
            {
                CancelInvoke("BoxRandom");
                timer.gameObject.SetActive(false);
                Time.timeScale = 0;
                WinText.SetActive(true);
                ReturnButton.SetActive(true);
            }
            if (BlackCount == 13)
            {
                CancelInvoke("BoxRandom");
                timer.gameObject.SetActive(false);
                Time.timeScale = 0;
                falseText.SetActive(true);
                ReturnButton.SetActive(true);
            }
        }
    }

    void BoxRandom()
    {
        a = Random.Range(1, 14);
        if (a == 1)
        {
            if (BlackBox_1.activeSelf == true)
            {
                BoxRandom();
            }
            else
            {
                BlackCount++;
                BlackBox_1.SetActive(true);
            }
        }
        else if (a == 2)
        {
            if (BlackBox_2.activeSelf == true)
            {
                BoxRandom();
            }
            else
            {
                BlackCount++;
                BlackBox_2.SetActive(true);
            }
        }
        else if (a == 3)
        {
            if (BlackBox_3.activeSelf == true)
            {
                BoxRandom();
            }
            else
            {
                BlackCount++;
                BlackBox_3.SetActive(true);
            }
        }
        else if (a == 4)
        {
            if (BlackBox_4.activeSelf == true)
            {
                BoxRandom();
            }
            else
            {
                BlackCount++;
                BlackBox_4.SetActive(true);
            }
        }
        else if (a == 5)
        {
            if (BlackBox_5.activeSelf == true)
            {
                BoxRandom();
            }
            else
            {
                BlackCount++;
                BlackBox_5.SetActive(true);
            }
        }
        else if (a == 6)
        {
            if (BlackBox_6.activeSelf == true)
            {
                BoxRandom();
            }
            else
            {
                BlackCount++;
                BlackBox_6.SetActive(true);
            }
        }
        else if (a == 7)
        {
            if (BlackBox_7.activeSelf == true)
            {
                BoxRandom();
            }
            else
            {
                BlackCount++;
                BlackBox_7.SetActive(true);
            }
        }
        else if (a == 8)
        {
            if (BlackBox_8.activeSelf == true)
            {
                BoxRandom();
            }
            else
            {
                BlackCount++;
                BlackBox_8.SetActive(true);
            }
        }
        else if (a == 9)
        {
            if (BlackBox_9.activeSelf == true)
            {
                BoxRandom();
            }
            else
            {
                BlackCount++;
                BlackBox_9.SetActive(true);
            }
        }
        else if (a == 10)
        {
            if (BlackBox_10.activeSelf == true)
            {
                BoxRandom();
            }
            else
            {
                BlackCount++;
                BlackBox_10.SetActive(true);
            }
        }
        else if (a == 11)
        {
            if (BlackBox_11.activeSelf == true)
            {
                BoxRandom();
            }
            else
            {
                BlackCount++;
                BlackBox_11.SetActive(true);
            }
        }
        else if (a == 12)
        {
            if (BlackBox_12.activeSelf == true)
            {
                BoxRandom();
            }
            else
            {
                BlackCount++;
                BlackBox_12.SetActive(true);
            }
        }
        else if (a == 13)
        {
            if (BlackBox_13.activeSelf == true)
            {
                BoxRandom();
            }
            else
            {
                BlackCount++;
                BlackBox_13.SetActive(true);
            }
        }
        else if (a == 14)
        {
            if (BlackBox_14.activeSelf == true)
            {
                BoxRandom();
            }
            else
            {
                BlackCount++;
                BlackBox_14.SetActive(true);
            }
        }
    }
}
