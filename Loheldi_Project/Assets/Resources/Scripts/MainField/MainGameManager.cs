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
    public Text expBarleftText;                    //필요한 경험치량
    public Text expBarrightText;                  //현재 경험치량
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
        expBarleftText.text = exp.ToString("F0");              //설명설명
        expBarrightText.text = Maxexp.ToString("F0");                  //설명설명설명
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
