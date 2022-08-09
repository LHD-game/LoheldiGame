using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunNPC : MonoBehaviour
{
    public GameObject block;
    public Transform upup;
    public Animator Animator;
    private float[] NPCSpeed = { 0, 130, 160, 200 }; //그래프 상 1 = 20 ex) 힘찬 10 : NPCSpeed = 200
    private bool up = false;
    int fxnum=0; //npc이펙트 적당히 뜨게하기 용
    void FixedUpdate()
    {
        if (RunCountDown.CountEnd && !RunGameManager.isPause)
        {
            if (up)
            {
                transform.position = Vector3.MoveTowards(transform.position, upup.position, NPCSpeed[RunGameManager.difficulty] * Time.deltaTime);

                if (fxnum == 10)
                {
                    runFX.instance.RunningFX(this.gameObject);
                    fxnum = 0;
                }
                else
                {
                    fxnum += 1;
                }
            }

            else
            {
                transform.Translate(Vector3.forward * Time.deltaTime * NPCSpeed[RunGameManager.difficulty]);

                if (fxnum == 10)
                {
                    //runShFX_EffectHandler.shakeCamera = false;
                    runFX.instance.RunningFX(this.gameObject);
                    fxnum = 0;
                }
                else
                {
                    fxnum += 1;
                }
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