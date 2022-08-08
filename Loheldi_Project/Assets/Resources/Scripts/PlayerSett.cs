using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSett : MonoBehaviour
{
    public static float BGMValue;
    public static float SEValue;
    void Start()
    {
        BGMValue = PlayerSetting.BGMValueforSetting;
        SEValue = PlayerSetting.SEValueforSetting;
    }
}
