using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameManager : MonoBehaviour
{
    public int money;
    public int level;
    public static int exp;
    float expPersent;
    float Maxexp;

    public Text moneyText;
    public Text levelText;
    public Slider slider;

    void Start()
    {
        level = 1;
        Maxexp = 100;
        exp = 0;
    }

    void Update()
    {
        moneyText.text = money.ToString();
        levelText.text = level.ToString();

        slider.maxValue = Maxexp;
        expPersent = exp / Maxexp;
        slider.value = expPersent * 100;

        if (slider.value >= Maxexp)
        {
            LevelUp();
        }
    }


    void LevelUp()
    {
        slider.value = 0;
        exp = 0;
        Maxexp = Maxexp * 1.2f;
        level++;
    }
}
