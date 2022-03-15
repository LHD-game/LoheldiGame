using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RunPlayerRun : MonoBehaviour
{
    public Rigidbody player;

    public void PlayerRun()
    {
        if (RunCountDown.CountEnd == true)
        {
            player.AddRelativeForce(Vector3.forward * 2000f);   //600f
            //ShFX_EffectHandler.shakeCamera = true;
            runFX.instance. RunningFX(this.gameObject);
        }
    }
}