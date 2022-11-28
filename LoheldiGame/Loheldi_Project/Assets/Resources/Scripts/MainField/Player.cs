using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    void OnTriggerStay (Collider trigger)
    {
        if (trigger.gameObject.tag == "Land")
        {
            UIButton.OnLand = true;
        }
    }
        void OnTriggerExit(Collider trigger)
    {
        if (trigger.gameObject.tag == "Land")
        {
            UIButton.OnLand = false;
        }
    }
}