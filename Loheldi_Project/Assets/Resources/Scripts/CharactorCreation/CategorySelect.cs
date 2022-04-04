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

    
    /*    public void skin()
        {
            resetColor();
            Category = 0;
            image = Skin.GetComponent<Image>();
            image.sprite = Resources.Load<Sprite>("Sprites\\fieldUI\\CustomizingSelectTab") as Sprite;
        }
        public void eye()
        {
            resetColor();
            Category = 1;
            image = Eye.GetComponent<Image>();
            image.sprite = Resources.Load<Sprite>("Sprites\\fieldUI\\CustomizingSelectTab") as Sprite;
        }
        public void mouth()
        {
            resetColor();
            Category = 2;
            image = Mouth.GetComponent<Image>();
            image.sprite = Resources.Load<Sprite>("Sprites\\fieldUI\\CustomizingSelectTab") as Sprite;
        }
        public void hair()
        {
            resetColor();
            Category = 3;
            image = Hair.GetComponent<Image>();
            image.sprite = Resources.Load<Sprite>("Sprites\\fieldUI\\CustomizingSelectTab") as Sprite;
        }
        public void hair_color()
        {
            resetColor();
            Category = 4;
            image = H_Color.GetComponent<Image>();
            image.sprite = Resources.Load<Sprite>("Sprites\\fieldUI\\CustomizingSelectTab") as Sprite;
        }*/


    /*    public void resetColor()
        {
            Skin.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites\\fieldUI\\CustomizingNSelectTab") as Sprite;
            Eye.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites\\fieldUI\\CustomizingNSelectTab") as Sprite;
            Mouth.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites\\fieldUI\\CustomizingNSelectTab") as Sprite;
            Hair.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites\\fieldUI\\CustomizingNSelectTab") as Sprite;
            H_Color.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites\\fieldUI\\CustomizingNSelectTab") as Sprite;
        }*/
}