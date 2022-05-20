using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangColor : MonoBehaviour
{
    public static bool color;
    public static int k;

    public static GameObject[] badge;
    //public static Sprite[] badgeList;
    //static  Image spriteR;

    void start()
    {
        color = false;
    }
    // Update is called once per frame

    public static void PopUp()
    {
        //spriteR = badge[k].GetComponent<Image>();
        if (color)
        {
            badge[k].SetActive(false);
            //spriteR.sprite = badgeList[0] ;
            color = false;
        }
    }
}
