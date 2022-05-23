using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RunPlayerRun : MonoBehaviour
{
    //RunGameManager npc;
    //public Transform NPC;
    public Transform NPC_;
    private float Player_;
    public int dif;
    public GameObject FXpo;

    public Rigidbody player;

    private float warntime = 1.0f;
    private float nexttime = 0.0f;

    public GameObject SoundManager;
    public GameObject AnimationTrigger;

    void Update()
    {
        if (RunCountDown.CountEnd == true)
        {
            if (Time.time > nexttime)
            {
                nexttime = Time.time + warntime;

                if (Vector3.Distance(NPC_.position, transform.position) <= 400f)
                {
                    Hurry();
                }
            }
        }
        
    
    }

    public void PlayerRun()
    {
        if (RunCountDown.CountEnd == true)
        {
            player.AddRelativeForce(Vector3.forward * 2000f);   //600f
            runFX.instance. RunningFX(this.gameObject);

            if (RunGameManager.difficulty == 1)
            {
                SoundManager.GetComponent<SoundEffect>().Sound("RunFootSteps1");
            }
            else if (RunGameManager.difficulty == 2)
            {
                SoundManager.GetComponent<SoundEffect>().Sound("RunFootSteps2");
            }
            else if (RunGameManager.difficulty == 3)
            {
                SoundManager.GetComponent<SoundEffect>().Sound("RunFootSteps3");
            }
            AnimationTrigger.GetComponent<AnimationTriggerforMinigame>().GetButtonDown = true;
        }
    }

    public void Hurry()
    {
        warningFX.instance.WunningFX(FXpo);
    }
}