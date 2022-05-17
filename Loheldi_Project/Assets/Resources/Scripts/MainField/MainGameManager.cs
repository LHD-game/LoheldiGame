using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameManager : MonoBehaviour
{
    public int money;
    public static int level;
    public static float exp;
    float Maxexp;

    public Text moneyText;
    public Text levelText;
    public Text expBarleftText;                    //�ʿ��� ����ġ��
    public Text expBarrightText;                  //���� ����ġ��
    public Slider slider;
    public Slider conditionSlider;

    public GameObject SoundManager;

    void Start()
    {
        level = 1;
        Maxexp = 100;
        exp = 0;
    }

    void Update()
    {
        moneyText.text = money.ToString();
        expBarleftText.text = exp.ToString("F0");              //������
        expBarrightText.text = Maxexp.ToString("F0");                  //��������
        levelText.text = level.ToString();


        slider.maxValue = Maxexp;
        conditionSlider.maxValue = Maxexp;
        slider.value = exp;
        conditionSlider.value = exp;

        if (exp >= Maxexp)
        {
            SoundManager.GetComponent<SoundEffect>().Sound("LevelUp");
            LevelUp();
        }
    }


    void LevelUp()
    {
        conditionSlider.value = 0;
        slider.value = 0;
        exp = exp - Maxexp;
        Maxexp = Maxexp * 1.2f;
        level++;
    }
}
