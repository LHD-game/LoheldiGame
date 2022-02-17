using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButton : MonoBehaviour
{
    public static bool OnLand = false;    //Player�� �ٴڿ� �ִ��� Ȯ��
    public GameObject Player;             //Player����
    public GameObject Map;                //Map����
    public Rigidbody Playerrb;            //Player�� Rigidbody����

    public GameObject ShopMok;             // �����
    bool map;                              //������ �����ִ��� Ȯ��

    void Start()
    {
        map = false;
    }

    public void JumpButton()                //������ư
    {
        if (Player.GetComponent<Interaction>().NearNPC)     //NPC�ֺ��� �ִٸ�
        {
            ShopMok.SetActive(true);
        }
        else                                                //NPC�ֺ��� ���� �ʴٸ�
        {
            if (OnLand)                                         //Player�� �ٴڿ� �ִٸ�
            {
                Playerrb.AddForce(transform.up * 15000);
                OnLand = false;
            }
        }
    }

    public void MapButton()                 //������ư
    {
        if (map)                                            //������ �����ִٸ�
        {
            Map.SetActive(false);
            map = false;
        }
        else                                                //������ �����ִٸ�
        {
            Map.SetActive(true);
            map = true;
        }
    }
}
