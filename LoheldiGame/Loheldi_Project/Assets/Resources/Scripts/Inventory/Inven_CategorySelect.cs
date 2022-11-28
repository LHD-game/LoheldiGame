using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inven_CategorySelect : MonoBehaviour
{
    private static Inven_CategorySelect _instance;
    public static Inven_CategorySelect instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Inven_CategorySelect>();
            }
            return _instance;
        }
    }

    public GameObject SuperPanel;
    public GameObject GaguPanel;
    public GameObject CropsPanel;


    public void initInven()
    {
        initPanel();
    }

    void initPanel()
    {
        SuperPanel.SetActive(true);
        GaguPanel.SetActive(false);
        CropsPanel.SetActive(false);

    }

    public void PopSuper()
    {
        SuperPanel.SetActive(true);
        GaguPanel.SetActive(false);
        CropsPanel.SetActive(false);
    }

    public void PopGagu()
    {
        SuperPanel.SetActive(false);
        GaguPanel.SetActive(true);
        CropsPanel.SetActive(false);
    }

    public void PopCrops()
    {
        SuperPanel.SetActive(false);
        GaguPanel.SetActive(false);
        CropsPanel.SetActive(true);
    }
}
