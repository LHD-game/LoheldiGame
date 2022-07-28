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
    GameObject p_Lower_L;
    [SerializeField]
    GameObject p_Lower_R;
    [SerializeField]
    GameObject p_Shoes_L;
    [SerializeField]
    GameObject p_Shoes_R;

    // Player 모델 아래 붙어서 player의 커스텀을 씬 시작 시 적용시켜 줌!
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

        pc2.p_Lower_L = p_Lower_L;
        pc2.p_Lower_R = p_Lower_R;
        pc2.p_Shoes_L = p_Shoes_L;
        pc2.p_Shoes_R = p_Shoes_R;
        pc2.nowClothes();
        pc2.ResetClothes();
        pc2.PlayerLook();
    }
}
