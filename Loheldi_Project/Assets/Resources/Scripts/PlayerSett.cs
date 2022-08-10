using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSett : MonoBehaviour
{
    public static float BGMValue;
    public static float SEValue;
    void Start()
    {
        GetSound(); //로컬 설정을 불러오는 함수
    }

    //로컬에서 사운드 설정을 불러옵니다.
    void GetSound()
    {
        if (PlayerPrefs.HasKey("BGMValue"))
        {
            BGMValue = PlayerPrefs.GetFloat("BGMValue");  //value 값이 float형이라고 생각하여 이렇게 정해두었으나 아닐경우 수정 바랍니다.
        }
        else
        {
            BGMValue = 0.5f;
        }

        if (PlayerPrefs.HasKey("SEValue"))
        {
            SEValue = PlayerPrefs.GetFloat("SEValue");
        }
        else
        {
            SEValue = 0.5f;
        }
    }


    //로컬에 사운드 설정을 저장합니다.
    void SetSound(float bgm, float se)
    {
        PlayerPrefs.SetFloat("BGMValue", bgm);
        PlayerPrefs.SetFloat("SEValue", se);
    }
}
