using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBoolReset : MonoBehaviour
{
    public void Reset()
    {
        Animator animator = this.GetComponent<Animator>();
        animator.SetBool("BrushStart", false);
    }
}
