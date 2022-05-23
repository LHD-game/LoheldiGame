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
        
        animator = GetComponent<Animator>();   //�ִϸ����� ������Ʈ �ҷ�����
    }

    // Update is called once per frame
    void Update()
    {
        if (GetButtonDown)                                          //��ư�� �����ٸ� (ĳ���͸� �����̰� �ϴ� ��ũ��Ʈ�� On/Off)
        {
            if (Playerrb.velocity.magnitude >= 1.428f)              //Player�� 1.428f���� ������ (�ܼ� �밡�ٷ� ����� ��)
            {
                animator.SetBool("JoyStickMove2", true);            //�ٴ� �ִϸ��̼� ����
            }
            else
            {
                animator.SetBool("JoyStickMove2", false);
                animator.SetBool("JoyStickMove", true);             //�ȴ� �ִϸ��̼� ����
            }
        }
        else                                                        //�Ѵ� �ƴϸ� (�� �����̰� �ִٸ�)
        {
            animator.SetBool("JoyStickMove", false);                //�� �Ķ���� ����
            animator.SetBool("JoyStickMove2", false);
        }
        if (Playerrb.velocity.magnitude <= 0.001f && GetButtonDown)
        {
            animator.SetBool("JoyStickMove", false);                //�� �Ķ���� ����
            animator.SetBool("JoyStickMove2", false);
            GetButtonDown = false;
        }
    }
    public void JumpUpEvent()
    {
        UIButton.OnLand = false;
    }
}
