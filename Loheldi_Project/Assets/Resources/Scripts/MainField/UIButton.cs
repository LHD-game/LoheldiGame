using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    public static bool OnLand = false;    //Player�� �ٴڿ� �ִ��� Ȯ��
    public GameObject Player;             //Player����
    public GameObject Map;                //Map����
    public GameObject Inv;
    public GameObject ConditionWindow;                //Map����
    public Rigidbody Playerrb;            //Player�� Rigidbody����
    public Text conditionLevelText;            //����â ����

    public GameObject ShopMok;             // �����
    bool map;                              //������ �����ִ��� Ȯ��
    bool inv;
    public static bool conditionWindow;      //����â�� �����ִ��� Ȯ��

    public FlieChoice Chat;
    public Interaction Inter;
    private SceneLoader Scene;


    private void Awake()
    {
        ConditionWindow.SetActive(true); //����â ����
        ChangColor.badge = GameObject.FindGameObjectsWithTag("badge"); //���� �±� ����

        ChangColor.badgeList = Resources.LoadAll<Sprite>("Sprites/badgeList/imgList/"); //�̹��� ���

        ConditionWindow.SetActive(false);//����â �ݱ�
        conditionWindow = false;
    }

    void Start()
    {
        map = false;
        conditionWindow = false;
        Chat = GameObject.Find("chatManager").GetComponent<FlieChoice>();
    }

    public void GoHouse()
    {
        Inter = GameObject.Find("Player").GetComponent<Interaction>();
        Scene = GameObject.Find("HousingSystem").GetComponent<SceneLoader>();
    }

    public void JumpButton()                //������ư
    {
        if (Inter.NearNPC)     //NPC�ֺ��� �ִٸ�
        {
            if(Inter.NameNPC.Equals("tiger"))  //NPC�̸��� �̰Ÿ�
            {
                Chat.tiger();
            }
            else if (Inter.NameNPC.Equals("cat"))  //NPC�̸��� �̰Ÿ�
            {
                Chat.cat();
            }
            else if (Inter.NameNPC.Equals("chick"))  //NPC�̸��� �̰Ÿ�
            {
                Chat.chick();
            }
            else if (Inter.NameNPC.Equals("rabbit"))  //NPC�̸��� �̰Ÿ�
            {
                Chat.rabbit();
            }
            else if (Inter.NameNPC.Equals("squirrel"))  //NPC�̸��� �̰Ÿ�
            {
                Chat.squirrel();
            }
            else if (Inter.NameNPC.Equals("goat"))  //NPC�̸��� �̰Ÿ�
            {
                Chat.goat();
            }
            else if (Inter.NameNPC.Equals("fox2"))  //NPC�̸��� �̰Ÿ�
            {
                Chat.fox2();
            }
            else if (Inter.NameNPC.Equals("fox1"))  //NPC�̸��� �̰Ÿ�
            {
                Chat.fox1();
            }
            else if (Inter.NameNPC.Equals("dog"))  //NPC�̸��� �̰Ÿ�
            {
                Chat.dog();
            }
            //ShopMok.SetActive(true);
        }
        else if (Inter.Door)
        {
            if (Inter.NameNPC.Equals("InDoor"))
            {
                GoHouse();
                Scene.GotoHouse();
            }
                
            else if (Inter.NameNPC.Equals("ExitDoor"))
                Scene.GotoMainField();
            Inter.Door = false;
        }
        else                                                //NPC�ֺ��� ���� �ʴٸ�
        {
            if (OnLand)                                         //Player�� �ٴڿ� �ִٸ�
            {
                Playerrb.AddForce(transform.up * 15000);
                OnLand = false;
                MainGameManager.exp = MainGameManager.exp + 100;
            }
        }
    }

    /*public void MapButton()                 //������ư
    {
        Map.SetActive(true);
    }

    public void InvButton()
    {
        if (inv)
        {
            Inv.SetActive(false);
            map = false;
        }
        else
        {
            Inv.SetActive(true);
            map = true;
        }
    }

    public  void ConditionButton()                 //����â��ư
    {
        if (conditionWindow)                                            //����â�� �����ִٸ�
        {
            ConditionWindow.SetActive(false);
            conditionWindow = false;
        }
        else                                                //����â�� �����ִٸ�
        {
            ConditionWindow.SetActive(true);
            conditionWindow = true;
            conditionLevelText.text = MainGameManager.level.ToString();
        }
    }*/
}
