﻿using System.Collections;
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
            transform.position = target.position + new Vector3(192, 103, 82);
            //tr.position = new Vector3(tr.position.x, tr.position.y, target.position.z + 50);
        }
    }
    public void CameraReset()
    {
        tr.position = new Vector3(292.0f, 103.0f, 82.0f);

    }
}
