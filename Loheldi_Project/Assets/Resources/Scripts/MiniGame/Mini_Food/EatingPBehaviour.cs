using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EatingPBehaviour : MonoBehaviour
{
    //Rigidbody ������Ʈ ����
    private Rigidbody rb;

    //�÷��̾ �¿�� �����̴� �ӵ�
    public float playerSpeed = 10;

    public GameObject gObject;


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

    void PlayerMove()   //�÷��̾� ĳ���� �̵� �Լ�
    {
        float rMove = 0;

        //���콺 �Է�(����� ��ũ�� �Է�)
        if (Input.GetMouseButton(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                //0�� 1 �����Ϸ� ��ȯ�Ѵ�.
                Vector3 worldPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

                transform.Translate(Vector3.forward * playerSpeed * Time.deltaTime * 2);
                //��ũ�� ���� ��ġ
                if (worldPos.x < 0.5f)
                {
                    rMove = 90;
                }
                //��ũ�� ������ ��ġ
                else
                {

                    rMove = -90;
                }
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
