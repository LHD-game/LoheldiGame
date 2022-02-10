using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunHamiRun : MonoBehaviour
{
    public Transform Marker;

    void Update()
    {
        if (RunCountDown.CountEnd == true)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 10);
            Marker.transform.Translate(Vector3.left * Time.deltaTime * 1.7f);
        }
    }
}