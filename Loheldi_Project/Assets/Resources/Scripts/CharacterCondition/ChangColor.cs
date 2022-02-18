using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangColor : MonoBehaviour
{
    public GameObject Target;
    private int active;


    // Update is called once per frame
    void Update()
    {
        //MainGameManager activeBring = GameObject.Find("EventSystem").GetComponent<MainGameManager>();
        //active = GameObject.Find("EventSystem").GetComponent<MainGameManager>().level;
        active = MainGameManager.level;
        if (active == 5)
        {
            Active();
        }
    }

    public void Active()
    {
        Target.SetActive(false);
    }

   
}
