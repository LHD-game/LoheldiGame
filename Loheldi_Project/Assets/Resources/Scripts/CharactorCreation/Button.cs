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
            HCD();
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
            HCE();
        }
    }
    public void F()
    {
        if (CategorySelect.Category == 0)
        {
            SF();
        }
        else if (CategorySelect.Category == 1)
        {
            EF();
        }
        else if (CategorySelect.Category == 2)
        {
            MF();
        }
        else if (CategorySelect.Category == 4)
        {
            HCF();
        }
    }

    private void SA()           //피부
    {
        body.GetComponent<MeshRenderer>().materials[2].color = new Color(255 / 255f, 237 / 255f, 227 / 255f);
        head.GetComponent<MeshRenderer>().materials[0].color = new Color(255 / 255f, 237 / 255f, 227 / 255f);
        head.GetComponent<MeshRenderer>().materials[1].color = new Color(255 / 255f, 210 / 255f, 179 / 255f);
    }
    private void SB()
    {
        body.GetComponent<MeshRenderer>().materials[2].color = new Color(255 / 255f, 210 / 255f, 179 / 255f);
        head.GetComponent<MeshRenderer>().materials[0].color = new Color(255 / 255f, 210 / 255f, 179 / 255f);
        head.GetComponent<MeshRenderer>().materials[1].color = new Color(231 / 255f, 145 / 255f, 134 / 255f);
    }
    private void SC()
    {
        body.GetComponent<MeshRenderer>().materials[2].color = new Color(255 / 255f, 183 / 255f, 131 / 255f);
        head.GetComponent<MeshRenderer>().materials[0].color = new Color(255 / 255f, 183 / 255f, 131 / 255f);
        head.GetComponent<MeshRenderer>().materials[1].color = new Color(207 / 255f, 80 / 255f, 89 / 255f);
    }
    private void SD()
    {
        body.GetComponent<MeshRenderer>().materials[2].color = new Color(255 / 255f, 156 / 255f, 83 / 255f);
        head.GetComponent<MeshRenderer>().materials[0].color = new Color(255 / 255f, 156 / 255f, 83 / 255f);
        head.GetComponent<MeshRenderer>().materials[1].color = new Color(183 / 255f, 15 / 255f, 44 / 255f);
    }
    private void SE()
    {
        body.GetComponent<MeshRenderer>().materials[2].color = new Color(255 / 255f, 129 / 255f, 35 / 255f);
        head.GetComponent<MeshRenderer>().materials[0].color = new Color(255 / 255f, 129 / 255f, 35 / 255f);
        head.GetComponent<MeshRenderer>().materials[1].color = new Color(159 / 255f, 0 / 255f, 0 / 255f);
    }
    private void SF()
    {
        body.GetComponent<MeshRenderer>().materials[2].color = new Color(255 / 255f, 102 / 255f, 0 / 255f);
        head.GetComponent<MeshRenderer>().materials[0].color = new Color(255 / 255f, 102 / 255f, 0 / 255f);
        head.GetComponent<MeshRenderer>().materials[1].color = new Color(135 / 255f, 0 / 255f, 0 / 255f);
    }

    private void EA()           //눈
    {
        head.GetComponent<MeshRenderer>().materials[4].color = new Color(120 / 255f, 190 / 255f, 255 / 255f);
    }
    private void EB()
    {
        head.GetComponent<MeshRenderer>().materials[4].color = Color.red;
    }
    private void EC()
    {
        head.GetComponent<MeshRenderer>().materials[4].color = new Color(42 / 255f, 138 / 255f, 52 / 255f);
    }
    private void ED()
    {
        head.GetComponent<MeshRenderer>().materials[4].color = Color.yellow;
    }
    private void EE()
    {
        head.GetComponent<MeshRenderer>().materials[4].color = Color.grey;
    }
    private void EF()
    {
        head.GetComponent<MeshRenderer>().materials[4].color = Color.black;
    }

    private void MA()           //입
    {
        head.GetComponent<MeshRenderer>().materials[6].color = Color.cyan;
    }
    private void MB()
    {
        head.GetComponent<MeshRenderer>().materials[6].color = new Color(231 / 255f, 81 / 255f, 90 / 255f);
    }
    private void MC()
    {
        head.GetComponent<MeshRenderer>().materials[6].color = Color.green;
    }
    private void MD()
    {
        head.GetComponent<MeshRenderer>().materials[6].color = Color.yellow;
    }
    private void ME()
    {
        head.GetComponent<MeshRenderer>().materials[6].color = Color.grey;
    }
    private void MF()
    {
        head.GetComponent<MeshRenderer>().materials[6].color = Color.black;
    }

    private void HA()           //머리카락(머리 모양)
    {
        hair.SetActive(true);
    }
    private void HB()
    {
        hair.SetActive(false);
    }

    private void HCA()           //머리 색
    {
        hair.GetComponent<MeshRenderer>().materials[0].color = Color.cyan;
    }
    private void HCB()
    {
        hair.GetComponent<MeshRenderer>().materials[0].color = Color.red;
    }
    private void HCC()
    {
        hair.GetComponent<MeshRenderer>().materials[0].color = Color.green;
    }
    private void HCD()
    {
        hair.GetComponent<MeshRenderer>().materials[0].color = Color.yellow;
    }
    private void HCE()
    {
        hair.GetComponent<MeshRenderer>().materials[0].color = Color.grey;
    }
    private void HCF()
    {
        hair.GetComponent<MeshRenderer>().materials[0].color = Color.black;
    }
}