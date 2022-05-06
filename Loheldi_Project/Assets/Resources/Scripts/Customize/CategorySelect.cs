using BackEnd;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class CategorySelect : MonoBehaviour
{
    public GameObject SkinPanel;
    public GameObject EyesPanel;
    public GameObject MouthPanel;
    public GameObject HairPanel;

    void Start()
    {
        initPanel();
    }

    void initPanel()
    {
        SkinPanel.SetActive(true);
        EyesPanel.SetActive(false);
        MouthPanel.SetActive(false);
        HairPanel.SetActive(false);

    }

    public void PopSkin()
    {
        SkinPanel.SetActive(true);
        EyesPanel.SetActive(false);
        MouthPanel.SetActive(false);
        HairPanel.SetActive(false);
    }

    public void PopEyes()
    {
        SkinPanel.SetActive(false);
        EyesPanel.SetActive(true);
        MouthPanel.SetActive(false);
        HairPanel.SetActive(false);
    }

    public void PopMouth()
    {
        SkinPanel.SetActive(false);
        EyesPanel.SetActive(false);
        MouthPanel.SetActive(true);
        HairPanel.SetActive(false);
    }

    public void PopHair()
    {
        SkinPanel.SetActive(false);
        EyesPanel.SetActive(false);
        MouthPanel.SetActive(false);
        HairPanel.SetActive(true);
    }
}