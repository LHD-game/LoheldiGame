using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    public Camera Maincam;
    public Camera Subcam;
    void OnTriggerEnter(Collider other)
    {
        Maincam.enabled = false;
        Subcam.enabled = true;
    }
    void OnTriggerExit(Collider other)
    {
        Maincam.enabled = true;
        Subcam.enabled = false;
    }
}
