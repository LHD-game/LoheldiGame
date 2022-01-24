using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunNPC : MonoBehaviour
{
    public Transform Marker;

    private int[] NPCSpeed = { 100, 140, 200 };

    void FixedUpdate()
    {
        if (RunCountDown.CountEnd && !RunGameManager.isPause)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * NPCSpeed[RunGameManager.difficulty]);
        }
    }
}