using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    public Camera Maincam;
    public Camera Subcam;
    VirtualJoystick VJS;
    void OnTriggerEnter(Collider other)
    {
        Maincam.enabled = false;
        Subcam.enabled = true;
        VJS.TempInt = 2;
    }
    void OnTriggerExit(Collider other)
    {
        Maincam.enabled = true;
        Subcam.enabled = false;
        VJS.TempInt = 1;
    }
}
