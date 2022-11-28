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
        animator = GetComponent<Animator>();   //�ִϸ����� ������Ʈ �ҷ�����
        StartCoroutine("PlayerMove");
    }

    IEnumerator PlayerMove()
    {
        while (true)
        {
            if (JoyStick.GetComponent<VirtualJoystick>().MoveFlag)      //Player�� �����̰� �ִٸ�
            {
                animator.SetBool("JoyStickMove", true);                 //JoyStickMove �Ķ���� ���� (�ִϸ��̼� ������Ʈ�� ����, Animator ���� ����� ����)
                if (JoyStick.GetComponent<VirtualJoystick>().Playerrb.velocity.magnitude >= JoyStick.GetComponent<VirtualJoystick>().speed1 * 1.428f && running)    //Player�� �ִ�ӵ� ���
                {
                    animator.SetBool("JoyStickMove2", true);            //JoyStickMove2 �Ķ���� ����
                }
            }
            else                                                        //�Ѵ� �ƴϸ� (�� �����̰� �ִٸ�)
            {
                animator.SetBool("JoyStickMove", false);                //�� �Ķ���� ����
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
