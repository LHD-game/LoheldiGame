using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card_TimeSlider : MonoBehaviour
{
    private static Card_TimeSlider _instance;
    public static Card_TimeSlider instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Card_TimeSlider>();
            }
            return _instance;
        }
    }

    [SerializeField]
    Text timeTxt;

    Image sliderImg;
    public static float totalT = 200;
    public static float addTime = 10;
    public static float nowTime;


    void Start()
    {
        sliderImg = GetComponent<Image>();
        TimeInit();
    }

    void Update()
    {
        
    }

    public void TimeInit() //시간 초기화 - 200초
    {
        nowTime = totalT;
        TimeLook();
    }

    public void TimeDel()  //시간이 감소
    {
        nowTime -= Time.deltaTime;
        TimeLook();
    }

    public void TimeAdd()  // 시간이 증가 +10초
    {
        nowTime += addTime;
        if(nowTime > 200)
        {
            nowTime = 200;
        }
        TimeLook();
    }

    void TimeLook() //시간을 나타낸다
    {
        timeTxt.text = string.Format("시간: {0:N0}", nowTime);
        TimeSlider();
    }

    void TimeSlider()   //슬라이더의 이동
    {
        sliderImg.fillAmount = nowTime / totalT;
    }
}
