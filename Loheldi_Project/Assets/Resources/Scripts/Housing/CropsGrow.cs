using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CropsGrow : MonoBehaviour
{
    public Text text;
    private float time_start;
    private float time_current;
    private int hour;
    private int min;
    private int sec;
    private int temp_sec;
    private float time_Max = 3600f;
    private bool isEnded = false;


    private void Start()
    {
        time_current = 0f;
    }
    void Update()
    {
        if (isEnded)
            return;

        Check_Timer();
    }
    private void Check_Timer()
    {
        time_current = (float)System.DateTime.Now.TimeOfDay.TotalSeconds - time_start;
        hour = (int)time_current / 3600;
        temp_sec = (int)time_current % 3600;
        min = (int)temp_sec / 60;
        sec = temp_sec % 60;
        if (time_current < time_Max)
        {
            text.text = hour.ToString("0")+" : "+min.ToString("00")+" : "+sec.ToString("00");
        }
        else if (!isEnded)
        {
            End_Timer();
        }
    }

    private void End_Timer()
    {
        time_current = time_Max;
        text.text = $"{time_current:N0}";
        isEnded = true;
    }
}
