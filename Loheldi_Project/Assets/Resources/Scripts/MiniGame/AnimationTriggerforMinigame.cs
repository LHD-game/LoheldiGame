using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTriggerforMinigame : MonoBehaviour
{
    public Animator animator;
    private bool Running;
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
            Running = true;
            if (Playerrb.velocity.magnitude >= 3.5f)              //Player�� 1.428f���� ������ (�ܼ� �밡�ٷ� ����� ��)
            {
                animator.SetBool("JoyStickMove2", true);
                animator.SetBool("JoyStickMove", true);            //�ٴ� �ִϸ��̼� ����
            }
            else
            {
                animator.SetBool("JoyStickMove2", false);
                animator.SetBool("JoyStickMove", true);             //�ȴ� �ִϸ��̼� ����
            }
        }
        if (Playerrb.velocity.magnitude <= 0.0002f && Running)
        {
            animator.SetBool("JoyStickMove", false);                //�� �Ķ���� ����
            animator.SetBool("JoyStickMove2", false);
            GetButtonDown = false;
            Running = false;
        }
    }
    public void JumpUpEvent()
    {
        UIButton.OnLand = false;
    }
}
