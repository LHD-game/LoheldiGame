using BackEnd;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ClosetCategorySelect : MonoBehaviour
{
    public GameObject UpperPanel;
    public GameObject LowerPanel;
    public GameObject SocksPanel;
    public GameObject ShoesPanel;
    public GameObject HatPanel;
    public GameObject GlassesPanel;
    public GameObject BagPanel;

    void Start()
    {
        initPanel();
    }

    void initPanel()
    {
        UpperPanel.SetActive(true);
        LowerPanel.SetActive(false);
        SocksPanel.SetActive(false);
        ShoesPanel.SetActive(false);
        HatPanel.SetActive(false);
        GlassesPanel.SetActive(false);
        BagPanel.SetActive(false);

    }

    public void PopUpper()
    {
        UpperPanel.SetActive(true);
        LowerPanel.SetActive(false);
        SocksPanel.SetActive(false);
        ShoesPanel.SetActive(false);
        HatPanel.SetActive(false);
        GlassesPanel.SetActive(false);
        BagPanel.SetActive(false);
    }

    public void PopLower()
    {
        UpperPanel.SetActive(false);
        LowerPanel.SetActive(true);
        SocksPanel.SetActive(false);
        ShoesPanel.SetActive(false);
        HatPanel.SetActive(false);
        GlassesPanel.SetActive(false);
        BagPanel.SetActive(false);
    }

    public void PopSocks()
    {
        UpperPanel.SetActive(false);
        LowerPanel.SetActive(false);
        SocksPanel.SetActive(true);
        ShoesPanel.SetActive(false);
        HatPanel.SetActive(false);
        GlassesPanel.SetActive(false);
        BagPanel.SetActive(false);
    }

    public void PopShoes()
    {
        UpperPanel.SetActive(false);
        LowerPanel.SetActive(false);
        SocksPanel.SetActive(false);
        ShoesPanel.SetActive(true);
        HatPanel.SetActive(false);
        GlassesPanel.SetActive(false);
        BagPanel.SetActive(false);
    }

    public void PopHat()
    {
        UpperPanel.SetActive(false);
        LowerPanel.SetActive(false);
        SocksPanel.SetActive(false);
        ShoesPanel.SetActive(false);
        HatPanel.SetActive(true);
        GlassesPanel.SetActive(false);
        BagPanel.SetActive(false);
    }

    public void PopGlasses()
    {
        UpperPanel.SetActive(false);
        LowerPanel.SetActive(false);
        SocksPanel.SetActive(false);
        ShoesPanel.SetActive(false);
        HatPanel.SetActive(false);
        GlassesPanel.SetActive(true);
        BagPanel.SetActive(false);
    }

    public void PopBag()
    {
        UpperPanel.SetActive(false);
        LowerPanel.SetActive(false);
        SocksPanel.SetActive(false);
        ShoesPanel.SetActive(false);
        HatPanel.SetActive(false);
        GlassesPanel.SetActive(false);
        BagPanel.SetActive(true);
    }
}