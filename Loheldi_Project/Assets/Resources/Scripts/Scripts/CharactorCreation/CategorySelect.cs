using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategorySelect : MonoBehaviour
{
    public GameObject Skin;
    public GameObject Eye;
    public GameObject Mouth;
    public GameObject Hair;
    public GameObject H_Color;
    public GameObject Accessory;

    private GameObject Buttons1;
    private GameObject Buttons2;
    private GameObject Buttons3;
    private GameObject Buttons4;
    private GameObject Buttons5;
    private GameObject Buttons6;

    private int Category;

    void Start()
    {
        Buttons1 = Skin.transform.FindChild("Buttons").gameObject;
        Buttons2 = Eye.transform.FindChild("Buttons").gameObject;
        Buttons3 = Mouth.transform.FindChild("Buttons").gameObject;
        Buttons4 = Hair.transform.FindChild("Buttons").gameObject;
        Buttons5 = H_Color.transform.FindChild("Buttons").gameObject;
        Buttons6 = Accessory.transform.FindChild("Buttons").gameObject;
        PositionReset();

        Category = 6;
    }

    void Update()
    {
        if (Category <= 1)
        {
            Eye.transform.localPosition = new Vector3(162, 33, 0);
        }
        if (Category <= 2)
        {
            Mouth.transform.localPosition = new Vector3(238, 33, 0);
        }
        if (Category <= 3)
        {
            Hair.transform.localPosition = new Vector3(314, 33, 0);
        }
        if (Category <= 4)
        {
            H_Color.transform.localPosition = new Vector3(390, 33, 0);
        }
        if (Category <= 5)
        {
            Accessory.transform.localPosition = new Vector3(466, 33, 0);
        }
    }

    public void skin()
    {
        PositionReset();
        Category = 1;
        Buttons1.SetActive(true);
    }
    public void eye()
    {
        PositionReset();
        Category = 2;
        Buttons2.SetActive(true);
    }
    public void mouth()
    {
        PositionReset();
        Category = 3;
        Buttons3.SetActive(true);
    }
    public void hair()
    {
        PositionReset();
        Category = 4;
        Buttons4.SetActive(true);
    }
    public void hair_color()
    {
        PositionReset();
        Category = 5;
        Buttons5.SetActive(true);
    }
    public void accessory()
    {
        PositionReset();
        Category = 6;
        Buttons6.SetActive(true);
    }

    void PositionReset()
    {
        Eye.transform.localPosition = new Vector3(-390, 33, 0);
        Mouth.transform.localPosition = new Vector3(-314, 33, 0);
        Hair.transform.localPosition = new Vector3(-238, 33, 0);
        H_Color.transform.localPosition = new Vector3(-162, 33, 0);
        Accessory.transform.localPosition = new Vector3(-86, 33, 0);

        Buttons1.SetActive(false);
        Buttons2.SetActive(false);
        Buttons3.SetActive(false);
        Buttons4.SetActive(false);
        Buttons5.SetActive(false);
        Buttons6.SetActive(false);
    }
}
