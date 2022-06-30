using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BicycleRide : MonoBehaviour
{

    public bool Ride = false;
    public GameObject Bicycles;
    public GameObject Player;
    public void RideOn()
    {
        if (Ride)
        {
            Bicycles.GetComponent<Animator>().SetBool("BicycleMove", false);
            Player.GetComponent<Animator>().SetBool("BicycleMove", false);
            Bicycles.SetActive(false);
            Ride = false;
        }
        else
        {
            Bicycles.GetComponent<Animator>().SetBool("BicycleMove", true);
            Player.GetComponent<Animator>().SetBool("BicycleMove", true);
            Bicycles.SetActive(true);
            Ride = true;
        }
    }
}
