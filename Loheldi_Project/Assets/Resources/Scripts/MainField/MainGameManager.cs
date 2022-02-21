using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameManager : MonoBehaviour
{
    public int money;
    public static int level;
    public int conditionLevel;
    public static float exp;
    float Maxexp;

    public Text moneyText;
    public Text levelText; 
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
        levelText.text = level.ToString();

        slider.maxValue = Maxexp;
        conditionSlider.maxValue = Maxexp;
        slider.value = exp;
        conditionSlider.value = exp;

        if (slider.value >= Maxexp)
        {
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
        conditionLevel++;
    }
}
