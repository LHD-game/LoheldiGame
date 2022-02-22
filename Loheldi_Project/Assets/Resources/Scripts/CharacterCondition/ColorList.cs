using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorList : MonoBehaviour
{
    private int active;

    // Update is called once per frame
    void Update()
    {
        active = MainGameManager.level;
        if (active == 2)
        {
            walkBadge();
        }
        else if (active == 3)
        {
            runBadge();
        }
        else if(active == 4)
        {
            quest1_2();
        }
        else if(active == 5)
        {
            quest4_2();
        }
        else if(active == 6)
        {
            quest1_4();
        }
        else if(active == 7)
        {
            quest2_4();
        }
        else if(active == 8)
        {
            quest3_4();
        }
    }
    public void walkBadge()
    {
        ChangColor.color = true;
        ChangColor.k = 0;
        ChangColor.l = 0;
        ChangColor.PopUp();
    }

    public void runBadge()
    {
        ChangColor.color = true;
        ChangColor.k = 1;
        ChangColor.l = 0;
        ChangColor.PopUp();
    }

    public void quest1_2()
    {
        ChangColor.color = true;
        ChangColor.k = 2;
        ChangColor.l = 0;
        ChangColor.PopUp();
    }

    public void quest4_2()
    {
        ChangColor.color = true;
        ChangColor.k = 3;
        ChangColor.l = 0;
        ChangColor.PopUp();
    }

    public void quest1_4()
    {
        ChangColor.color = true;
        ChangColor.k = 4;
        ChangColor.l = 0;
        ChangColor.PopUp();
    }

    public void quest2_4()
    {
        ChangColor.color = true;
        ChangColor.k = 5;
        ChangColor.l = 0;
        ChangColor.PopUp();
    }

    public void quest3_4()
    {
        ChangColor.color = true;
        ChangColor.k = 6;
        ChangColor.l = 0;
        ChangColor.PopUp();
    }
}
