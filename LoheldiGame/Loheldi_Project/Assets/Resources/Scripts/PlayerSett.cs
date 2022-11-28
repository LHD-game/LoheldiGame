using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSett : MonoBehaviour
{
    public static float BGMValue;
    public static float SEValue;
    void Start()
    {
        GetSound(); //���� ������ �ҷ����� �Լ�
        SoundManager.audioSource.volume = 0.5f * PlayerSett.BGMValue;
    }

    //���ÿ��� ���� ������ �ҷ��ɴϴ�.
    void GetSound()
    {
        if (PlayerPrefs.HasKey("BGMValue"))
        {
            BGMValue = PlayerPrefs.GetFloat("BGMValue");  //value ���� float���̶�� �����Ͽ� �̷��� ���صξ����� �ƴҰ�� ���� �ٶ��ϴ�.
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
}
