using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangColor : MonoBehaviour
{
    public static bool color;
    public static int k;
    public GameObject Ride;
    public static GameObject[] badge;
    //public static Sprite[] badgeList;
    //static  Image spriteR;

    void start()
    {
       
        color = true;
    }
    // Update is called once per frame

    public void PopUp()
    {
        if (color)
        {
            Debug.Log("popup½ÇÇà");
            for (int i = 0; i <= ColorList.QDD.badgeList.Count; i++)
                badge[ColorList.QDD.badgeList[i]].SetActive(false);
            color = false;
        }
    }
}
