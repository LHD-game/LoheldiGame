using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSet : PlayerCustom
{
    // Player �� �Ʒ� �پ player�� Ŀ������ �� ���� �� ������� ��!
    void Start()
    {
        nowCustom();
        ResetCustom();
        PlayerLook();
    }
}
