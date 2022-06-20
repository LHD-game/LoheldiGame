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
        sliderImg = this.GetComponent<Image>();
        TimeInit();
    }

    public void TimeInit() //�ð� �ʱ�ȭ - 200��
    {
        nowTime = totalT;
        TimeLook();
    }

    public void TimeDel()  //�ð��� ����
    {
        nowTime -= Time.deltaTime;
        TimeLook();
    }

    public void TimeAdd()  // �ð��� ���� +10��
    {
        nowTime += addTime;
        if(nowTime > 200)
        {
            nowTime = 200;
        }
        TimeLook();
    }

    void TimeLook() //�ð��� ��Ÿ����
    {
        timeTxt.text = string.Format("�ð�: {0:N0}", nowTime);
        TimeSlider();
    }

    void TimeSlider()   //�����̴��� �̵�
    {
        sliderImg.fillAmount = nowTime / totalT;
    }
}
