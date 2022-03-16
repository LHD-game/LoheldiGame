using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunNPC : MonoBehaviour
{
    public GameObject block;
    public Transform upup;
    private float[] NPCSpeed = { 0, 100, 140, 200 };
    private bool up = false;

    void FixedUpdate()
    {
        if (RunCountDown.CountEnd && !RunGameManager.isPause)
        {
            if (up)
            {
                transform.position = Vector3.MoveTowards(transform.position, upup.position, NPCSpeed[RunGameManager.difficulty] * Time.deltaTime);
                runFX.instance.RunningFX(this.gameObject);
            }

            else
            {
                transform.Translate(Vector3.forward * Time.deltaTime * NPCSpeed[RunGameManager.difficulty]);
                runFX.instance.RunningFX(this.gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == block)
        {
            up = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == block)
        {
            up = false;
        }
    }
}