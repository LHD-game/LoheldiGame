using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject player;
    public static GameObject body;
    public static GameObject head;
    public static GameObject hair;
    public Material[] material;

    public void Start()
    {
        body = player.gameObject.transform.GetChild(0).gameObject; //하이라키에 player의 자식을 각각 지정
        head = player.gameObject.transform.GetChild(1).gameObject;
        hair = player.gameObject.transform.GetChild(2).gameObject;

        SA();    //시작시 초기화  ※초기화 버튼은 Rotation Button에 있음
        EC();
        MB();
        HCA();
    }

    public void A()
    {
        if (CategorySelect.Category == 0)
        {
            SA();
        }
        else if (CategorySelect.Category == 1)
        {
            EA();
        }
        else if (CategorySelect.Category == 2)
        {
            MA();
        }
        else if (CategorySelect.Category == 3)
        {
            HA();
        }
        else if (CategorySelect.Category == 4)
        {
            HCA();
        }
    }
    public void B()
    {
        if (CategorySelect.Category == 0)
        {
            SB();
        }
        else if (CategorySelect.Category == 1)
        {
            EB();
        }
        else if (CategorySelect.Category == 2)
        {
            MB();
        }
        else if (CategorySelect.Category == 3)
        {
            HB();
        }
        else if (CategorySelect.Category == 4)
        {
            HCB();
        }
    }
    public void C()
    {
        if (CategorySelect.Category == 0)
        {
            SC();
        }
        else if (CategorySelect.Category == 1)
        {
            EC();
        }
        else if (CategorySelect.Category == 2)
        {
            MC();
        }
        else if (CategorySelect.Category == 4)
        {
            HCC();
        }
    }
    public void D()
    {
        if (CategorySelect.Category == 0)
        {
            SD();
        }
        else if (CategorySelect.Category == 1)
        {
            ED();
        }
        else if (CategorySelect.Category == 2)
        {
            MD();
        }
        else if (CategorySelect.Category == 4)
        {
            //HCD();
        }
    }
    public void E()
    {
        if (CategorySelect.Category == 0)
        {
            SE();
        }
        else if (CategorySelect.Category == 1)
        {
            EE();
        }
        else if (CategorySelect.Category == 2)
        {
            ME();
        }
        else if (CategorySelect.Category == 4)
        {
            //HCE();
        }
    }
    public void F()
    {
        if (CategorySelect.Category == 0)
        {
            //SF();
        }
        else if (CategorySelect.Category == 1)
        {
            //EF();
        }
        else if (CategorySelect.Category == 2)
        {
            MF();
        }
        else if (CategorySelect.Category == 4)
        {
            //HCF();
        }
    }

    public static void SA()           //피부
    {
        body.GetComponent<MeshRenderer>().materials[2].color = new Color(245 / 255f, 227 / 255f, 217 / 255f);
        head.GetComponent<MeshRenderer>().materials[0].color = new Color(245 / 255f, 227 / 255f, 217 / 255f);
        head.GetComponent<MeshRenderer>().materials[1].color = new Color(255 / 255f, 200 / 255f, 169 / 255f);
    }
    public static void SB()
    {
        body.GetComponent<MeshRenderer>().materials[2].color = new Color(240 / 255f, 203 / 255f, 182 / 255f);
        head.GetComponent<MeshRenderer>().materials[0].color = new Color(240 / 255f, 203 / 255f, 182 / 255f);
        head.GetComponent<MeshRenderer>().materials[1].color = new Color(231 / 255f, 145 / 255f, 134 / 255f);
    }
    public static void SC()
    {
        body.GetComponent<MeshRenderer>().materials[2].color = new Color(209 / 255f, 158 / 255f, 129 / 255f);
        head.GetComponent<MeshRenderer>().materials[0].color = new Color(209 / 255f, 158 / 255f, 129 / 255f);
        head.GetComponent<MeshRenderer>().materials[1].color = new Color(222 / 255f, 115 / 255f, 122 / 255f);
    }
    public static void SD()
    {
        body.GetComponent<MeshRenderer>().materials[2].color = new Color(163 / 255f, 97 / 255f, 51 / 255f);
        head.GetComponent<MeshRenderer>().materials[0].color = new Color(163 / 255f, 97 / 255f, 51 / 255f);
        head.GetComponent<MeshRenderer>().materials[1].color = new Color(234 / 255f, 128 / 255f, 99 / 255f);
    }
    public static void SE()
    {
        body.GetComponent<MeshRenderer>().materials[2].color = new Color(55 / 255f, 38 / 255f, 28 / 255f);
        head.GetComponent<MeshRenderer>().materials[0].color = new Color(55 / 255f, 38 / 255f, 28 / 255f);
        head.GetComponent<MeshRenderer>().materials[1].color = new Color(119 / 255f, 63 / 255f, 36 / 255f);
    }

    public static void EA()           //눈
    {
        head.GetComponent<MeshRenderer>().materials[4].color = new Color(196 / 255f, 35 / 255f, 124 / 255f);
    }
    public static void EB()
    {
        head.GetComponent<MeshRenderer>().materials[4].color = new Color(142 / 255f, 84 / 255f, 66 / 255f);
    }
    public static void EC()
    {
        head.GetComponent<MeshRenderer>().materials[4].color = new Color(42 / 255f, 138 / 255f, 52 / 255f);
    }
    public static void ED()
    {
        head.GetComponent<MeshRenderer>().materials[4].color = new Color(30 / 255f, 115 / 255f, 168 / 255f);
    }
    public static void EE()
    {
        head.GetComponent<MeshRenderer>().materials[4].color = new Color(181 / 255f, 181 / 255f, 181 / 255f);
    }

    public static void MA()           //입
    {
        head.GetComponent<MeshRenderer>().materials[6].color = Color.cyan;
    }
    public static void MB()
    {
        head.GetComponent<MeshRenderer>().materials[6].color = new Color(231 / 255f, 81 / 255f, 90 / 255f);
    }
    public static void MC()
    {
        head.GetComponent<MeshRenderer>().materials[6].color = Color.green;
    }
    public static void MD()
    {
        head.GetComponent<MeshRenderer>().materials[6].color = Color.yellow;
    }
    public static void ME()
    {
        head.GetComponent<MeshRenderer>().materials[6].color = Color.grey;
    }
    public static void MF()
    {
        head.GetComponent<MeshRenderer>().materials[6].color = Color.black;
    }

    public static void HA()           //머리카락(머리 모양)
    {
        hair.SetActive(true);
    }
    public static void HB()
    {
        hair.SetActive(false);
    }

    public static void HCA()           //머리 색
    {
        hair.GetComponent<MeshRenderer>().materials[0].color = new Color(0 / 255f, 0 / 255f, 0 / 255f);
    }
    public static void HCB()
    {
        hair.GetComponent<MeshRenderer>().materials[0].color = new Color(96 / 255f, 59 / 255f, 50 / 255f);
    }
    public static void HCC()
    {
        hair.GetComponent<MeshRenderer>().materials[0].color = new Color(181 / 255f, 181 / 255f, 181 / 255f);
    }
    public static void HCD()
    {
        hair.GetComponent<MeshRenderer>().materials[0].color = new Color(46 / 255f, 72 / 255f, 117 / 255f);
    }
    public static void HCE()
    {
        hair.GetComponent<MeshRenderer>().materials[0].color = new Color(96 / 255f, 93 / 255f, 0 / 255f);
    }
    public static void HCF()
    {
        hair.GetComponent<MeshRenderer>().materials[0].color = new Color(240 / 255f, 128 / 255f, 128 / 255f);
    }
}