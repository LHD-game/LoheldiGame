using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EatingPBehaviour : MonoBehaviour
{
    //Rigidbody 컴포넌트 참조
    private Rigidbody rb;

    //플레이어가 좌우로 움직이는 속도
    public float playerSpeed = 10;

    public GameObject gObject;
    public GameObject AnimationTrigger;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (GameManager.instance.Blocker())
        {
            PlayerMove();
            ScreenChk();
        }

    }

    void PlayerMove()   //플레이어 캐릭터 이동 함수
    {
        float rMove = 0;

        //마우스 입력(모바일 스크린 입력)
        if (Input.GetMouseButton(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                //0과 1 스케일로 변환한다.
                Vector3 worldPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

                transform.Translate(Vector3.forward * playerSpeed * Time.deltaTime * 2);
                //스크린 왼쪽 터치
                if (worldPos.x < 0.5f)
                {
                    rMove = 90;
                }
                //스크린 오른쪽 터치
                else
                {

                    rMove = -90;
                }
                AnimationTrigger.GetComponent<AnimationTriggerforMinigame>().GetButtonDown = true;
            }
        }
           
        else
        {
            rMove = 0;
        }

        Transform from;
        Transform to;
        from = gameObject.GetComponent<Transform>();
        to = gameObject.GetComponent<Transform>();
        to.transform.localEulerAngles = new Vector3(0, rMove, 0);
        transform.rotation = Quaternion.Slerp(from.rotation, to.rotation, 1);
    }

    void ScreenChk()
    {
        Vector3 worldPos = Camera.main.WorldToViewportPoint(this.transform.position);
        if (worldPos.x < 0.05f) worldPos.x = 0.05f;
        else if (worldPos.x > 0.95f) worldPos.x = 0.95f;
        this.transform.position = Camera.main.ViewportToWorldPoint(worldPos);

        Vector3 worldPos2 = Camera.main.ScreenToViewportPoint(this.transform.position);
        if (worldPos2.z != 0.00f) worldPos2.z = 0.00f;
        this.transform.position = Camera.main.ViewportToScreenPoint(worldPos2);
    }
}
