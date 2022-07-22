using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CropsGrow : MonoBehaviour
{
    public Text text;
    private float time_start;
    private float time_current;
    private float time_Max = 3600f;
    private bool isEnded;


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
        if (time_current < time_Max)
        {
            text.text = $"{time_current:N}";
        }
        else if (!isEnded)
        {
            End_Timer();
        }
    }

    private void End_Timer()
    {
        time_current = time_Max;
        text.text = $"{time_current:N}";
        isEnded = true;
    }
}
