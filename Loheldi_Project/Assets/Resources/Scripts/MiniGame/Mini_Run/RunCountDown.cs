using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RunCountDown : MonoBehaviour
{
    public GameObject DifficultyPanel;
    public GameObject RunBtnPanel;

    public GameObject Num;
    public GameObject Num_1;
    public GameObject Num_2;
    public GameObject Num_3;
    private int timer = 3;
    public static bool CountEnd = false;

    public GameObject SoundManager;

    void Start()
    {
        ResetTimer();
    }
    
    public void CountNum()
    {
        DifficultyPanel.gameObject.SetActive(false);
        RunBtnPanel.SetActive(true);

        InvokeRepeating("NumAppear", 0f, 1f);
    }

    private void NumAppear()
    {
        if(timer == 3)
        {
            Num.SetActive(true);
        }
        else if(timer == 2)
        {
            Num_3.SetActive(true);
            SoundManager.GetComponent<SoundEffect>().Sound("CountFinish");
        }
        else if (timer == 1)
        {
            Num_2.SetActive(true);
            SoundManager.GetComponent<SoundEffect>().Sound("CountFinish");
        }
        else if (timer == 0)
        {
            Num_1.SetActive(true);
            SoundManager.GetComponent<SoundEffect>().Sound("CountFinish");
        }
        else if(timer == -1)
        {
            Num.SetActive(false);
            Num_1.SetActive(false);
            Num_2.SetActive(false);
            Num_3.SetActive(false);
            CountEnd = true;
            SoundManager.GetComponent<SoundEffect>().Sound("Count");
            CancelInvoke("NumAppear");
        }
        timer--;

    }

    public void ResetTimer()
    {
        timer = 3;
        CountEnd = false;
        Num.SetActive(false);
        Num_1.SetActive(false);
        Num_2.SetActive(false);
        Num_3.SetActive(false);
        CancelInvoke("NumAppear");
        
    }

}