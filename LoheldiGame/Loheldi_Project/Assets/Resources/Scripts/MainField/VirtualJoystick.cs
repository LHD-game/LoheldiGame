using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class VirtualJoystick : MonoBehaviour
{
    public Transform Player;
    public Rigidbody Playerrb;
    public Transform Stick;
    public GameObject BicycleAnimator;
    public GameObject PlayerAnimator;
    public float speed1 = 8f;
    public float speed2 = 11f;
    public bool MoveFlag;

    private Vector3 StickFirstPos;
    private Vector3 JoyVec;
    private float Radius;

    public int TempInt = 0;

    void Start()
    {
        //레벨에 따른 속도 조절
        int my_lev = PlayerPrefs.GetInt("Level");
        if (my_lev >= 5)
        {
            if (my_lev >= 10)   //레벨이 10 이상
            {
                speed1 = 10f;
                speed2 = 13f;
            }
            else //레벨이 5 이상 10 미만
            {
                speed1 = 9f;
                speed2 = 12f;
            }
        }
        Debug.Log("속도: " + speed1);

        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "MainField")
            TempInt = 0;
        else if (scene.name == "Housing")
            TempInt = 1;
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
                if (SceneManager.GetActiveScene().name == "Housing")
                {
                    Playerrb.velocity = Playerrb.velocity.normalized / speed2;
                }
                else if (SceneManager.GetActiveScene().name == "MainField")
                {
                    //Debug.Log("메인속도");
                    Playerrb.velocity = Playerrb.velocity.normalized * speed2;  //최대 속도
                    if (BicycleAnimator.GetComponent<BicycleRide>().Ride)
                    {
                        BicycleAnimator.GetComponent<Animator>().speed = 1;
                        PlayerAnimator.GetComponent<Animator>().speed = 1;
                        Playerrb.velocity = Playerrb.velocity.normalized * speed2 * 2;  //최대 속도
                    }
                }
            }
        }

        else
        {
            if (SceneManager.GetActiveScene().name == "MainField")
            {
                if (BicycleAnimator.GetComponent<BicycleRide>().Ride)
                {
                    BicycleAnimator.GetComponent<Animator>().speed = 0;
                    PlayerAnimator.GetComponent<Animator>().speed = 0;
                }
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

        if (TempInt == 1)
        {
            Player.eulerAngles = new Vector3(0, Mathf.Atan2(JoyVec.x, JoyVec.y) * Mathf.Rad2Deg - 45, 0);
        }
        else if(TempInt == 2)
        {
            Player.eulerAngles = new Vector3(0, Mathf.Atan2(JoyVec.x, JoyVec.y) * Mathf.Rad2Deg - 115, 0);
        }
        else 
        {
            Player.eulerAngles = new Vector3(0, Mathf.Atan2(JoyVec.x, JoyVec.y) * Mathf.Rad2Deg, 0);
        }
        PlayerAnimator.GetComponent<Animator>().SetBool("JoyStickMove", true);
    }

    public void DragEnd()
    {
        Stick.position = StickFirstPos;
        JoyVec = Vector3.zero;
        MoveFlag = false;
        Playerrb.velocity = new Vector3(0, 0, 0);
        PlayerAnimator.GetComponent<Animator>().SetBool("JoyStickMove", false);
    }
}