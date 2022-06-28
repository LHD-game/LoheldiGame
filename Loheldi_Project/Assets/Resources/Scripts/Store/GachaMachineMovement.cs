using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaMachineMovement : MonoBehaviour
{

    private Animator SpinAnimator;
    private Animator CapsuleAnimator;

    public GameObject Lever;
    public GameObject Capsule;

    public GameObject ButtonPanel;
    public GameObject Machine;
    public GameObject BackGround;

    public void LeverSpin()
    {
        ButtonPanel.SetActive(false);
        Capsule.transform.position = new Vector3(-0.2544488f, 1.7f, 0.3488888f);
        SpinAnimator = Lever.GetComponent<Animator>();   //�ִϸ����� ������Ʈ �ҷ�����
        SpinAnimator.SetBool("Spin", true);
    }
    public void TimeLineEvent()
    {
        CapsuleAnimator = Capsule.GetComponent<Animator>();
        CapsuleAnimator.SetBool("Capsule", true);
    }
    public void TimeLineEventReset()
    {
        SpinAnimator = Lever.GetComponent<Animator>();
        CapsuleAnimator = Capsule.GetComponent<Animator>();
        SpinAnimator.SetBool("Spin", false);
        CapsuleAnimator.SetBool("Capsule", false);
        Machine.SetActive(false);
        BackGround.SetActive(true);
        ButtonPanel.SetActive(true);
        Debug.Log("�ִϸ��̼� �ʱ�ȭ �Ϸ�");
    }
}
