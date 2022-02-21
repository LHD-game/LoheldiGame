using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public Slider conditionSlider;
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
