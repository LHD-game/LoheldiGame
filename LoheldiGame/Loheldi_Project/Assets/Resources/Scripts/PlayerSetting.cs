using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSetting : MonoBehaviour
{
    public GameObject SettingContent;
    public float BGMValueforSetting;
    public float SEValueforSetting;

    public int BGMValueforString;
    public int SEValueforString;
    void Start()
    {
        SettingContent.transform.Find("BGMSlider").gameObject.GetComponent<Slider>().value = PlayerSett.BGMValue;
        SettingContent.transform.Find("SESlider").gameObject.GetComponent<Slider>().value = PlayerSett.SEValue;
    }

    void Update()
    {
        BGMValueforSetting = SettingContent.transform.Find("BGMSlider").gameObject.GetComponent<Slider>().value;
        BGMValueforString = (int)Math.Round(BGMValueforSetting * 100);
        SettingContent.transform.Find("BGMSlider").Find("BGMValue").gameObject.GetComponent<Text>().text = BGMValueforString.ToString();
        PlayerSett.BGMValue = BGMValueforSetting;
        SoundManager.audioSource.volume = 0.8f * PlayerSett.BGMValue;

        SEValueforSetting = SettingContent.transform.Find("SESlider").gameObject.GetComponent<Slider>().value;
        SEValueforString = (int)Math.Round(SEValueforSetting * 100);
        SettingContent.transform.Find("SESlider").Find("SEValue").gameObject.GetComponent<Text>().text = SEValueforString.ToString();
        PlayerSett.SEValue = SEValueforSetting;

        //���ÿ� ����
        PlayerPrefs.SetFloat("BGMValue", BGMValueforSetting);
        PlayerPrefs.SetFloat("SEValue", SEValueforSetting);
    }
}
