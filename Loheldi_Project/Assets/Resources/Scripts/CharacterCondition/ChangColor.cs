using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangColor : MonoBehaviour
{

    public GameObject Target;
    private int active;
    private static bool color;
    private static int k;
    public static int h;

    public static int l;
    public static GameObject[] badge;
    public static Sprite[] badgeList;
    static  Image spriteR;



    void start()
    {
        h = 0;
        color = false;
    }
    // Update is called once per frame
    void Update()
    {
        active = MainGameManager.level;
        if (active >=2)
        {
            color = true;
            k = 0;
            //Active();
        }
        else if (active >= 4)
        {
            color= true;
            k = 1;
        }
    }

    public void Active()
    {
        Target.SetActive(false);
    }

    public static void myh()
    {
         badge = GameObject.FindGameObjectsWithTag("badge");
        
        while (h<2)
        {
            badgeList = Resources.LoadAll<Sprite>("Sprites/badgeList/");
            h++;
        }
        PopUp();
    }
   
    private static void PopUp()
    {
        spriteR = badge[k].GetComponent<Image>();
        if (color)
        {
            spriteR.sprite = badgeList[0];
            color = false;
        }

       
    }
}

