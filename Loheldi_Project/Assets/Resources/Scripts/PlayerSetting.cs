using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSetting : MonoBehaviour
{
    public GameObject SettingContent;
    public static float BGMValueforSetting;
    public static float SEValueforSetting;

    public int BGMValueforString;
    public int SEValueforString;

    void Update()
    {
        BGMValueforSetting = SettingContent.transform.Find("BGMSlider").gameObject.GetComponent<Slider>().value;
        BGMValueforString = (int)Math.Round(BGMValueforSetting * 100);
        SettingContent.transform.Find("BGMSlider").Find("BGMValue").gameObject.GetComponent<Text>().text = BGMValueforString.ToString();

        SEValueforSetting = SettingContent.transform.Find("SESlider").gameObject.GetComponent<Slider>().value;
        SEValueforString = (int)Math.Round(SEValueforSetting * 100);
        SettingContent.transform.Find("SESlider").Find("SEValue").gameObject.GetComponent<Text>().text = SEValueforString.ToString();
    }
}
