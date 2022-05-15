using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSet : PlayerCustom
{
    // Player 모델 아래 붙어서 player의 커스텀을 씬 시작 시 적용시켜 줌!
    void Start()
    {
        nowCustom();
        ResetCustom();
        PlayerLook();
    }
}
