using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Super_CategorySelect : MonoBehaviour
{
    public GameObject SeedPanel;
    public GameObject TreePanel;


    void Start()
    {
        initPanel();
    }

    void initPanel()
    {
        SeedPanel.SetActive(true);
        TreePanel.SetActive(false);
    }

    public void PopSeed()
    {
        SeedPanel.SetActive(true);
        TreePanel.SetActive(false);
    }

    public void PopTree()
    {
        SeedPanel.SetActive(false);
        TreePanel.SetActive(true);
    }
}
