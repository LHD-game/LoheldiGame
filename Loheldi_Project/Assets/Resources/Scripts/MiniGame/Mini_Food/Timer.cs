using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private static Timer _instance;
    public static Timer instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Timer>();
            }
            return _instance;
        }
    }

    private float time_start = 0;
    private float time_current;
    private float time_current_tmp;
    private bool isRun = false;
    public bool isPause = false;
    int gLevel = 0;

    [SerializeField]
    private Text timeTxt;

    private void Update()
    {
        if (isRun && !isPause)
        {
            CheckTimer();
        }
    }

    private void CheckTimer()
    {
        time_current = Time.time - time_start;
        timeTxt.text = $"{time_current:N2}";
        if (GameManager.instance.ReturnLife() <= 0)
        {
            EndTimer();
        }

    }
    public void EndTimer()
    {
        timeTxt.text = $"{time_current:N2}";
        isRun = false;
    }


    public void StartTimer()
    {
        gLevel = 0;
        time_start = Time.time;
        time_current = 0;
        time_current_tmp = 0;
        timeTxt.text = $"{time_current:N2}";
        isRun = true;
        isPause = false;
        Time.timeScale = 1f;
    }

    public void PauseTimer()
    {
        if (!isPause)
        {
            isPause = true;
            Time.timeScale = 0f;
        }
        else
        {
            isPause = false;
            Time.timeScale = 1f;
        }
        
    }

    
    public int GameLvUp()
    {
        float time = 10.0f;
        if(time_current >= time_current_tmp+ time)
        {
            time_current_tmp += time;
            gLevel++;
            if(gLevel > 8)
            {
                gLevel = 8;
            }
            Debug.Log("gLevel: " + gLevel);
            GameManager.instance.FoodDropsec(gLevel);
        }
        
        return gLevel;
    }
}
