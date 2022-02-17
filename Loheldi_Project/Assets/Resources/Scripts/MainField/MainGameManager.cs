using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameManager : MonoBehaviour
{
    public int money;
    public int level;
    public int conditionLevel;
    public static int exp;
    float expPersent;
    float Maxexp;

    public Text moneyText;
    public Text levelText; 
    public Text conditionLevelText;
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
        conditionLevelText.text = level.ToString();

        slider.maxValue = Maxexp;
        conditionSlider.maxValue = Maxexp;
        expPersent = exp / Maxexp;
        slider.value = expPersent * 100;
        conditionSlider.value = expPersent * 100;

        if (slider.value >= Maxexp)
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
