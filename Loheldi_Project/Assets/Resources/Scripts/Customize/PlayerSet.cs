using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSet : MonoBehaviour
{
    [SerializeField]
    GameObject p_Eyes;
    [SerializeField]
    GameObject p_Mouth;
    [SerializeField]
    GameObject p_Hair;

    [SerializeField]
    GameObject p_Lower;

    // Player �� �Ʒ� �پ player�� Ŀ������ �� ���� �� ������� ��!
    PlayerCustom pc = new PlayerCustom();
    PlayerCloset pc2 = new PlayerCloset();
    
    void Start()
    {
        pc.p_Eyes = p_Eyes;
        pc.p_Mouth = p_Mouth;
        pc.p_Hair = p_Hair;
        pc.nowCustom();
        pc.ResetCustom();
        pc.PlayerLook();

        pc2.p_Lower = p_Lower;
        pc2.nowClothes();
        pc2.ResetClothes();
        pc2.PlayerLook();
    }
}
