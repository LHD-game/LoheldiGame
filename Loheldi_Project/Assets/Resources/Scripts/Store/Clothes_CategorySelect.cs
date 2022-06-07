using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clothes_CategorySelect : MonoBehaviour
{
    public GameObject UpperPanel;
    public GameObject LowerPanel;
    public GameObject ShoesPanel;
    public GameObject EtcPanel;


    void Start()
    {
        initPanel();
    }

    void initPanel()
    {
        UpperPanel.SetActive(true);
        LowerPanel.SetActive(false);
        ShoesPanel.SetActive(false);
        EtcPanel.SetActive(false);
    }

    public void PopUpper()
    {
        UpperPanel.SetActive(true);
        LowerPanel.SetActive(false);
        ShoesPanel.SetActive(false);
        EtcPanel.SetActive(false);
    }

    public void PopLower()
    {
        UpperPanel.SetActive(false);
        LowerPanel.SetActive(true);
        ShoesPanel.SetActive(false);
        EtcPanel.SetActive(false);
    }

    public void PopShoes()
    {
        UpperPanel.SetActive(false);
        LowerPanel.SetActive(false);
        ShoesPanel.SetActive(true);
        EtcPanel.SetActive(false);
    }

    public void PopEtc()
    {
        UpperPanel.SetActive(false);
        LowerPanel.SetActive(false);
        ShoesPanel.SetActive(false);
        EtcPanel.SetActive(true);
    }
}
