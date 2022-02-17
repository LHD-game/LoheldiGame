using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButton : MonoBehaviour
{
    public static bool OnLand = false;    //Player가 바닥에 있는지 확인
    public GameObject Player;             //Player선언
    public GameObject Map;                //Map선언
    public Rigidbody Playerrb;            //Player의 Rigidbody선언

    public GameObject ShopMok;             // 목공방
    bool map;                              //지도가 열려있는지 확인

    void Start()
    {
        map = false;
    }

    public void JumpButton()                //점프버튼
    {
        if (Player.GetComponent<Interaction>().NearNPC)     //NPC주변에 있다면
        {
            ShopMok.SetActive(true);
        }
        else                                                //NPC주변에 있지 않다면
        {
            if (OnLand)                                         //Player가 바닥에 있다면
            {
                Playerrb.AddForce(transform.up * 15000);
                OnLand = false;
            }
        }
    }

    public void MapButton()                 //지도버튼
    {
        if (map)                                            //지도가 열려있다면
        {
            Map.SetActive(false);
            map = false;
        }
        else                                                //지도가 닫혀있다면
        {
            Map.SetActive(true);
            map = true;
        }
    }
}
