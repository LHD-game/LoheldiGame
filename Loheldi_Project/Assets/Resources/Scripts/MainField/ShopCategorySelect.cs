using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCategorySelect : MonoBehaviour
{
    public GameObject Buttons;
    private Text ButtonTxt;


    public static GameObject Buttons1;
    private GameObject Buttons2;
    private GameObject Buttons3;
    private GameObject Buttons4;
    private GameObject Buttons5;
    private GameObject Buttons6;
    private Text PriceTxt;

    public static int Category;
    public static int Page;
    private int Price;
    public static int buttonnum;

    void Start()
    {
        Price = 0;
        Category = 1;
        Page = 1;
        Buttons1 = Buttons.transform.Find("Button").gameObject;
        Buttons2 = Buttons.transform.Find("Button (1)").gameObject;
        Buttons3 = Buttons.transform.Find("Button (2)").gameObject;
        Buttons4 = Buttons.transform.Find("Button (3)").gameObject;
        Buttons5 = Buttons.transform.Find("Button (4)").gameObject;
        Buttons6 = Buttons.transform.Find("Button (5)").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Category == 1)
        {
            if (Page == 1)
            {
                ButtonTxt = Buttons1.transform.Find("Text").GetComponent<Text>();
                
                ButtonTxt = Buttons2.transform.Find("Text").GetComponent<Text>();
                
                ButtonTxt = Buttons3.transform.Find("Text").GetComponent<Text>();
                
                ButtonTxt = Buttons4.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "D";
                ButtonTxt = Buttons5.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "E";
                ButtonTxt = Buttons6.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "F";
            }
            if (Page == 2)
            {
                ButtonTxt = Buttons1.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "G";
                ButtonTxt = Buttons2.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "H";
                ButtonTxt = Buttons3.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "I";
                ButtonTxt = Buttons4.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "J";
                ButtonTxt = Buttons5.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "K";
                ButtonTxt = Buttons6.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "L";
            }
        }
        else if (Category == 2)
        {
            if (Page == 1)
            {
                ButtonTxt = Buttons1.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "���� A";
                ButtonTxt = Buttons2.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "���� B";
                ButtonTxt = Buttons3.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "���� C";
                ButtonTxt = Buttons4.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "���� D";
                ButtonTxt = Buttons5.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "���� E";
                ButtonTxt = Buttons6.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "���� F";
            }
            if (Page == 2)
            {
                ButtonTxt = Buttons1.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "���� G";
                ButtonTxt = Buttons2.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "���� H";
                ButtonTxt = Buttons3.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "���� I";
                ButtonTxt = Buttons4.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "���� J";
                ButtonTxt = Buttons5.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "���� K";
                ButtonTxt = Buttons6.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "���� L";
            }
        }
        else if (Category == 3)
        {
            if (Page == 1)
            {
                ButtonTxt = Buttons1.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "��� A";
                ButtonTxt = Buttons2.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "��� B";
                ButtonTxt = Buttons3.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "��� C";
                ButtonTxt = Buttons4.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "��� D";
                ButtonTxt = Buttons5.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "��� E";
                ButtonTxt = Buttons6.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "��� F";
            }
            if (Page == 2)
            {
                ButtonTxt = Buttons1.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "��� G";
                ButtonTxt = Buttons2.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "��� H";
                ButtonTxt = Buttons3.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "��� I";
                ButtonTxt = Buttons4.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "��� J";
                ButtonTxt = Buttons5.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "��� K";
                ButtonTxt = Buttons6.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "��� L";
            }
        }
        else if (Category == 4)
        {
            if (Page == 1)
            {
                ButtonTxt = Buttons1.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "Űġ A";
                ButtonTxt = Buttons2.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "Űġ B";
                ButtonTxt = Buttons3.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "Űġ C";
                ButtonTxt = Buttons4.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "Űġ D";
                ButtonTxt = Buttons5.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "Űġ E";
                ButtonTxt = Buttons6.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "Űġ F";
            }
            if (Page == 2)
            {
                ButtonTxt = Buttons1.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "Űġ G";
                ButtonTxt = Buttons2.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "Űġ H";
                ButtonTxt = Buttons3.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "Űġ I";
                ButtonTxt = Buttons4.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "Űġ J";
                ButtonTxt = Buttons5.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "Űġ K";
                ButtonTxt = Buttons6.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "Űġ L";
            }
        }
        /*else if (Category == 5)
        {
            if (Page == 1)
            {
                ButtonTxt = Buttons1.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "Ŭ���� A";
                ButtonTxt = Buttons2.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "Ŭ���� B";
                ButtonTxt = Buttons3.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "Ŭ���� C";
                ButtonTxt = Buttons4.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "Ŭ���� D";
                ButtonTxt = Buttons5.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "Ŭ���� E";
                ButtonTxt = Buttons6.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "Ŭ���� F";
            }
            if (Page == 2)
            {
                ButtonTxt = Buttons1.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "Ŭ���� G";
                ButtonTxt = Buttons2.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "Ŭ���� H";
                ButtonTxt = Buttons3.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "Ŭ���� I";
                ButtonTxt = Buttons4.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "Ŭ���� J";
                ButtonTxt = Buttons5.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "Ŭ���� K";
                ButtonTxt = Buttons6.transform.Find("Text").GetComponent<Text>();
                ButtonTxt.text = "Ŭ���� L";
            }
        }*/
    }

    public void Cate1()
    {
        Category = 1;
        Page = 1;
    }
    public void Cate2()
    {
        Category = 2;
        Page = 1;
    }
    public void Cate3()
    {
        Category = 3;
        Page = 1;
    }
    public void Cate4()
    {
        Category = 4;
        Page = 1;
    }
    public void Cate5()
    {
        Category = 5;
        Page = 1;
    }

    public void PageL()
    {
        Page = 1;
    }
    public void PageR()
    {
        Page = 2;
    }
    public void Button1()
    {
        buttonnum = 0;
    }
    public void Button2()
    {
        buttonnum = 1;
    }
    public void Button3()
    {
        buttonnum = 2;
    }
    public void Button4()
    {
        buttonnum = 3;
    }
    public void Button5()
    {
        buttonnum = 4;
    }
    public void Button6()
    {
        buttonnum = 5;
    }
}
