using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameManager : MonoBehaviour
{
    public int money;
    public int level;
    public int conditionLevel; //상태창 레벨
    public static int exp;
    float Maxexp;

    public Text moneyText;
    public Text levelText;
    public Text expBarleftText;                    //필요한 경험치량
    public Text expBarrightText;                  //현재 경험치량
    public Text conditionLevelText;            //상태창 레벨
    public Slider slider;
    public Slider conditionSlider;
    void Start()
    {
        conditionLevel = 1;
        level = 1;
        Maxexp = 100;
        exp = 0;
    }
    void Update()
    {
        moneyText.text = money.ToString();
        expBarleftText.text = exp.ToString("F0");              //설명설명
        expBarrightText.text = Maxexp.ToString("F0");                  //설명설명설명
        levelText.text = level.ToString();
        conditionLevelText.text = level.ToString();

        slider.maxValue = Maxexp;
        conditionSlider.maxValue = Maxexp;
        slider.value = exp;
        conditionSlider.value = slider.value;

        if (exp >= Maxexp)
        {
            LevelUp();
        }
    }
    void LevelUp()
    {
        conditionSlider.value = 0;
        slider.value = 0;
        exp = 0;
        Maxexp = Maxexp * 1.2f;
        level++;
        conditionLevel++;
    }
}