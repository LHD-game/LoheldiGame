using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    private Animator animator;
    private bool Jumping;
    private bool running=false;

    public GameObject JoyStick;

    private void Awake()
    {
        animator = GetComponent<Animator>();   //애니메이터 컴포넌트 불러오기
        StartCoroutine("PlayerMove");
    }

    IEnumerator PlayerMove()
    {
        while (true)
        {
            if (JoyStick.GetComponent<VirtualJoystick>().MoveFlag)      //Player가 움직이고 있다면
            {
                animator.SetBool("JoyStickMove", true);                 //JoyStickMove 파라미터 조절 (애니메이션 컴포넌트에 있음, Animator 탭을 열어야 보임)
                if (JoyStick.GetComponent<VirtualJoystick>().Playerrb.velocity.magnitude >= JoyStick.GetComponent<VirtualJoystick>().speed1 * 1.428f && running)    //Player가 최대속도 라면
                {
                    animator.SetBool("JoyStickMove2", true);            //JoyStickMove2 파라미터 조절
                }
            }
            else                                                        //둘다 아니면 (안 움직이고 있다면)
            {
                animator.SetBool("JoyStickMove", false);                //두 파라미터 조절
                animator.SetBool("JoyStickMove2", false);
            }

            if (UIButton.OnLand == false)
            {
                if (Jumping == false)
                {
                    animator.SetBool("JumpUp", true);
                    animator.SetBool("JumpDown", false);
                    Jumping = true;
                }
            }

            if (UIButton.OnLand == true)
            {
                animator.SetBool("JumpUp", false);
                if (Jumping)
                {
                    animator.SetBool("JumpDown", true);
                    Jumping = false;
                }
                else
                {
                    animator.SetBool("JumpDown", true);
                }
            }
            yield return null;
        }
    }
    

    public void JumpUpEvent()
    {
        UIButton.OnLand = false;
    }
}
