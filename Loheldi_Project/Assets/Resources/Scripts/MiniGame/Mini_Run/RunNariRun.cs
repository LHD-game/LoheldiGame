using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunNariRun : MonoBehaviour
{
    public Transform Marker;

    void Update()
    {
        if (RunCountDown.CountEnd == true)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 14);
            Marker.transform.Translate(Vector3.left * Time.deltaTime * 2.38f);
        }
    }
}