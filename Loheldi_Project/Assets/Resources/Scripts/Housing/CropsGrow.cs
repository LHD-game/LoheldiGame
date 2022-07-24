using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CropsGrow : MonoBehaviour
{
    public Text text;

    private float time_current;
    private float time_start;
    private int hour;
    private int min;
    private int sec;
    private int temp_sec;
    private float time_Max = 10f;
    private bool isEnded = false;

    private GameObject NextCrops;

    public Text ICode;

    private void Start()
    {
        time_start = (DateTime.Now.Hour * 3600) + (DateTime.Now.Minute * 60) + DateTime.Now.Second;
    }
    void Update()
    {
        if (isEnded)
            return;

        Check_Timer();
    }
    private void Check_Timer()
    {
        time_current = (DateTime.Now.Hour * 3600) + (DateTime.Now.Minute * 60) + DateTime.Now.Second - time_start;
        hour = (int)time_current / 3600;
        temp_sec = (int)time_current % 3600;
        min = temp_sec / 60;
        sec = temp_sec % 60;
        if (time_current < time_Max)
        {
            text.text = hour.ToString("0")+" : "+min.ToString("00")+" : "+sec.ToString("00");
        }
        else if (time_current >= time_Max && !isEnded)
        {
            End_Timer();
        }
    }

    private void End_Timer()
    {
        if (ICode.text == "1010101")
        {
            NextCrops = Instantiate(Resources.Load<GameObject>("Prefabs/Crops/Crops_Wheat"));
            NextCrops.transform.SetParent(transform.parent);
            //NextCrops.transform.GetChild(0).GetComponent<Text>().text = "15";
            NextCrops.transform.localPosition = new Vector3(0, 0, 0);
            Destroy(this.gameObject);
        }
        else if (ICode.text == "1010106")
        {
            NextCrops = Instantiate(Resources.Load<GameObject>("Prefabs/Crops/Crops_Corn"));
            NextCrops.transform.SetParent(transform.parent);
            //NextCrops.transform.GetChild(0).GetComponent<Text>().text = "15";
            NextCrops.transform.localPosition = new Vector3(0, 0, 0);
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("작물 모델이 없어서 GreenPlants로 대체");
        }
    }
}
