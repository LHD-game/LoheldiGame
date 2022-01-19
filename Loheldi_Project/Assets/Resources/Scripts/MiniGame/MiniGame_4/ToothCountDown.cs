using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToothCountDown : MonoBehaviour
{
    public GameObject DifficultyButton1;
    public GameObject DifficultyButton2;
    public GameObject DifficultyButton3;
    public GameObject Num_A;
    public GameObject Num_B;
    public GameObject Num_C;
    public static float Timer = 0;
    public static bool CountStart = false;
    public static bool CountEnd = false;

    void Start()
    {
        Num_A.SetActive(false);
        Num_B.SetActive(false);
        Num_C.SetActive(false);
    }
    void Update()
    {

        if (CountStart)
        {
            DifficultyButton1.gameObject.SetActive(false);
            DifficultyButton2.gameObject.SetActive(false);
            DifficultyButton3.gameObject.SetActive(false);

            Timer += Time.deltaTime;

            if (Timer < 1)
            {
                Num_C.SetActive(true);
            }
            else if (Timer < 2 && Timer >= 1)
            {
                Num_C.SetActive(false);
                Num_B.SetActive(true);
            }
            else if (Timer < 3 && Timer >= 2)
            {
                Num_B.SetActive(false);
                Num_A.SetActive(true);
            }
            else
            {
                Num_A.SetActive(false);
                CountEnd = true;
            }
        }
    }
}