using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunHimchanRun : MonoBehaviour
{
    public Transform Marker;

    void Update()
    {
        if (RunCountDown.CountEnd == true)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 20);
            Marker.transform.Translate(Vector3.left * Time.deltaTime * 3.4f);
        }
    }
}
