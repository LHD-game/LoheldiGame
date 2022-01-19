using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    private Transform tr;

    void Start()
    {
        tr = GetComponent<Transform>();
    }

    void LateUpdate()
    {
        if (target.position.z >= 62.9)
        {
            tr.position = new Vector3(tr.position.x, tr.position.y, target.position.z + 50);
        }
    }
    public void CameraReset()
    {
        tr.position = new Vector3(320.0f, 200.0f, 120.0f);

    }
}
