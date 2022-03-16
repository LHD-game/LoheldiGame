using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject player;
    public Material[] material;

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
    }

    private void SA()
    {
        player.GetComponent<MeshRenderer>().materials[0].color = Color.cyan;
    }
    private void SB()
    {
        player.GetComponent<MeshRenderer>().materials[0].color = Color.red;
    }
    private void SC()
    {
        player.GetComponent<MeshRenderer>().materials[0].color = Color.green;
    }
    private void SD()
    {
        player.GetComponent<MeshRenderer>().materials[0].color = Color.yellow;
    }
    private void SE()
    {
        player.GetComponent<MeshRenderer>().materials[0].color = Color.grey;
    }
    private void SF()
    {
        player.GetComponent<MeshRenderer>().materials[0].color = Color.black;
    }

    private void EA()
    {
        player.GetComponent<MeshRenderer>().materials[4].color = Color.cyan;
    }
    private void EB()
    {
        player.GetComponent<MeshRenderer>().materials[4].color = Color.red;
    }
    private void EC()
    {
        player.GetComponent<MeshRenderer>().materials[4].color = Color.green;
    }
    private void ED()
    {
        player.GetComponent<MeshRenderer>().materials[4].color = Color.yellow;
    }
    private void EE()
    {
        player.GetComponent<MeshRenderer>().materials[4].color = Color.grey;
    }
    private void EF()
    {
        player.GetComponent<MeshRenderer>().materials[4].color = Color.black;
    }

    private void MA()
    {
        player.GetComponent<MeshRenderer>().materials[5].color = Color.cyan;
    }
    private void MB()
    {
        player.GetComponent<MeshRenderer>().materials[5].color = Color.red;
    }
    private void MC()
    {
        player.GetComponent<MeshRenderer>().materials[5].color = Color.green;
    }
    private void MD()
    {
        player.GetComponent<MeshRenderer>().materials[5].color = Color.yellow;
    }
    private void ME()
    {
        player.GetComponent<MeshRenderer>().materials[5].color = Color.grey;
    }
    private void MF()
    {
        player.GetComponent<MeshRenderer>().materials[5].color = Color.black;
    }
}