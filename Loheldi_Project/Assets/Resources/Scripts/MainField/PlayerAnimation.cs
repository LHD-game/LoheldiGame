using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator anim;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void WalkAnime()
    {
        anim.enabled = true;
        anim.Play("Armature|ArmatureAction");
    }
}
