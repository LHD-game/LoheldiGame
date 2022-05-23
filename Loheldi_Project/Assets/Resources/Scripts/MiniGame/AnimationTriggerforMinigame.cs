using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTriggerforMinigame : MonoBehaviour
{
    private Animator animator;
    private bool Jumping;
    public bool GetButtonDown;

    public Rigidbody Playerrb;

    private void Awake()
    {
        
        animator = GetComponent<Animator>();   //애니메이터 컴포넌트 불러오기
    }

    // Update is called once per frame
    void Update()
    {
        if (GetButtonDown)                                          //버튼을 눌렀다면 (캐릭터를 움직이게 하는 스크립트로 On/Off)
        {
            if (Playerrb.velocity.magnitude >= 1.428f)              //Player가 1.428f보다 빠르면 (단순 노가다로 산출된 값)
            {
                animator.SetBool("JoyStickMove2", true);            //뛰는 애니메이션 적용
            }
            else
            {
                animator.SetBool("JoyStickMove2", false);
                animator.SetBool("JoyStickMove", true);             //걷는 애니메이션 적용
            }
        }
        else                                                        //둘다 아니면 (안 움직이고 있다면)
        {
            animator.SetBool("JoyStickMove", false);                //두 파라미터 조절
            animator.SetBool("JoyStickMove2", false);
        }
        if (Playerrb.velocity.magnitude <= 0.001f && GetButtonDown)
        {
            animator.SetBool("JoyStickMove", false);                //두 파라미터 조절
            animator.SetBool("JoyStickMove2", false);
            GetButtonDown = false;
        }
    }
    public void JumpUpEvent()
    {
        UIButton.OnLand = false;
    }
}
