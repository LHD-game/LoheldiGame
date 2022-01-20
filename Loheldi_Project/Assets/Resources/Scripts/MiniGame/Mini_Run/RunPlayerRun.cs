using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RunPlayerRun : MonoBehaviour
{
    public GameObject player;
    public GameObject Marker;



    public void PlayerRun()
    {
        if (RunCountDown.CountEnd == true)
        {
            player.gameObject.transform.Translate(new Vector3(0, 0, 300));
            Marker.transform.Translate(new Vector3(-50f, 0, 0));

        }
    }
}