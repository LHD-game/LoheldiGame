using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour
{
    public Transform Player;
    public Rigidbody Playerrb;
    public Transform Stick;
    public float speed1 = 8f;
    public float speed2 = 10f;

    private Vector3 StickFirstPos;
    private Vector3 JoyVec;
    private float Radius;
    private bool MoveFlag;

    //public Animator Animation;

    void Start()
    {
        Radius = GetComponent<RectTransform>().sizeDelta.y* 1.5f;
        StickFirstPos = Stick.transform.position;

        float Can = transform.parent.GetComponent<RectTransform>().localScale.x;
        Radius *= Can;

        MoveFlag = false;
    }

    void FixedUpdate()
    {
        if (MoveFlag)  //Player가 움직이고 있다면
        {
            Playerrb.AddRelativeForce(Vector3.forward * 3000f);  //앞 방향으로 밀기 (방향 * 힘)

            if (Playerrb.velocity.magnitude > speed1)
            {
                Playerrb.velocity = Playerrb.velocity.normalized * speed2;  //최대 속도
            }
        }
    }

    public void Drag(BaseEventData _Data)
    {
        MoveFlag = true;
        PointerEventData Data = _Data as PointerEventData;
        Vector3 Pos = Data.position;

        JoyVec = (Pos - StickFirstPos).normalized;

        float Dis = Vector3.Distance(Pos, StickFirstPos);

        if (Dis < Radius)
            Stick.position = StickFirstPos + JoyVec * Dis;
        else
            Stick.position = StickFirstPos + JoyVec * Radius;

        Player.eulerAngles = new Vector3(0, Mathf.Atan2(JoyVec.x, JoyVec.y) * Mathf.Rad2Deg, 0);
        //Animation.SetBool("IsWalk", true);
    }

    public void DragEnd()
    {
        Stick.position = StickFirstPos;
        JoyVec = Vector3.zero;
        MoveFlag = false;
        Playerrb.velocity = new Vector3(0, 0, 0);
        //Animation.SetBool("IsWalk", false);
    }
}