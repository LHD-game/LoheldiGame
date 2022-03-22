/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class CategorySelect : MonoBehaviour
{
    public GameObject Skin;
    public GameObject Eye;
    public GameObject Mouth;
    public GameObject Hair;
    public GameObject H_Color;

    public static int Category;
    private Image image;


    public GameObject Buttons;
    private GameObject Buttons1;
    private GameObject Buttons2;
    private GameObject Buttons3;
    private GameObject Buttons4;
    private GameObject Buttons5;
    private GameObject Buttons6;
    private Text ButtonTxt;

    void Start()
    {
        Category = 0;
        Buttons1 = Buttons.transform.Find("Button").gameObject;
        Buttons2 = Buttons.transform.Find("Button (1)").gameObject;
        Buttons3 = Buttons.transform.Find("Button (2)").gameObject;
        Buttons4 = Buttons.transform.Find("Button (3)").gameObject;
        Buttons5 = Buttons.transform.Find("Button (4)").gameObject;
        Buttons6 = Buttons.transform.Find("Button (5)").gameObject;
    }

    void Update()
    {
        if (Category == 0)
        {
            ButtonTxt = Buttons1.transform.Find("Text").GetComponent<Text>();
            ButtonTxt.text = "SA";
            ButtonTxt = Buttons2.transform.Find("Text").GetComponent<Text>();
            ButtonTxt.text = "SB";
            ButtonTxt = Buttons3.transform.Find("Text").GetComponent<Text>();
            ButtonTxt.text = "SC";
            ButtonTxt = Buttons4.transform.Find("Text").GetComponent<Text>();
            ButtonTxt.text = "SD";
            ButtonTxt = Buttons5.transform.Find("Text").GetComponent<Text>();
            ButtonTxt.text = "SE";
            ButtonTxt = Buttons6.transform.Find("Text").GetComponent<Text>();
            ButtonTxt.text = "SF";
        }
        else if (Category == 1)
        {
            ButtonTxt = Buttons1.transform.Find("Text").GetComponent<Text>();
            ButtonTxt.text = "EA";
            ButtonTxt = Buttons2.transform.Find("Text").GetComponent<Text>();
            ButtonTxt.text = "EB";
            ButtonTxt = Buttons3.transform.Find("Text").GetComponent<Text>();
            ButtonTxt.text = "EC";
            ButtonTxt = Buttons4.transform.Find("Text").GetComponent<Text>();
            ButtonTxt.text = "ED";
            ButtonTxt = Buttons5.transform.Find("Text").GetComponent<Text>();
            ButtonTxt.text = "EE";
            ButtonTxt = Buttons6.transform.Find("Text").GetComponent<Text>();
            ButtonTxt.text = "EF";
        }
        else if (Category == 2)
        {
            ButtonTxt = Buttons1.transform.Find("Text").GetComponent<Text>();
            ButtonTxt.text = "MA";
            ButtonTxt = Buttons2.transform.Find("Text").GetComponent<Text>();
            ButtonTxt.text = "MB";
            ButtonTxt = Buttons3.transform.Find("Text").GetComponent<Text>();
            ButtonTxt.text = "MC";
            ButtonTxt = Buttons4.transform.Find("Text").GetComponent<Text>();
            ButtonTxt.text = "MD";
            ButtonTxt = Buttons5.transform.Find("Text").GetComponent<Text>();
            ButtonTxt.text = "ME";
            ButtonTxt = Buttons6.transform.Find("Text").GetComponent<Text>();
            ButtonTxt.text = "MF";
        }
        else if (Category == 3)
        {
            ButtonTxt = Buttons1.transform.Find("Text").GetComponent<Text>();
            ButtonTxt.text = "HA";
            ButtonTxt = Buttons2.transform.Find("Text").GetComponent<Text>();
            ButtonTxt.text = "HB";
            ButtonTxt = Buttons3.transform.Find("Text").GetComponent<Text>();
            ButtonTxt.text = "HC";
            ButtonTxt = Buttons4.transform.Find("Text").GetComponent<Text>();
            ButtonTxt.text = "HD";
            ButtonTxt = Buttons5.transform.Find("Text").GetComponent<Text>();
            ButtonTxt.text = "HE";
            ButtonTxt = Buttons6.transform.Find("Text").GetComponent<Text>();
            ButtonTxt.text = "HF";
        }
        else if (Category == 4)
        {
            ButtonTxt = Buttons1.transform.Find("Text").GetComponent<Text>();
            ButtonTxt.text = "HCA";
            ButtonTxt = Buttons2.transform.Find("Text").GetComponent<Text>();
            ButtonTxt.text = "HCB";
            ButtonTxt = Buttons3.transform.Find("Text").GetComponent<Text>();
            ButtonTxt.text = "HCC";
            ButtonTxt = Buttons4.transform.Find("Text").GetComponent<Text>();
            ButtonTxt.text = "HCD";
            ButtonTxt = Buttons5.transform.Find("Text").GetComponent<Text>();
            ButtonTxt.text = "HCE";
            ButtonTxt = Buttons6.transform.Find("Text").GetComponent<Text>();
            ButtonTxt.text = "HCF";
        }
    }

    public void skin()
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
    }


    public void resetColor()
    {
        Skin.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites\\fieldUI\\CustomizingNSelectTab") as Sprite;
        Eye.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites\\fieldUI\\CustomizingNSelectTab") as Sprite;
        Mouth.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites\\fieldUI\\CustomizingNSelectTab") as Sprite;
        Hair.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites\\fieldUI\\CustomizingNSelectTab") as Sprite;
        H_Color.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites\\fieldUI\\CustomizingNSelectTab") as Sprite;
    }
}
*/