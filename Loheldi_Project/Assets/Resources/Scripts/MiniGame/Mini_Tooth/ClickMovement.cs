using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMovement : MonoBehaviour
{
    private new Camera camera;
    private bool isMove;
    private Vector3 destination;
    private void Awake()
    {
        camera = Camera.main;
    }
    void Update()
    {
        if (ToothCountDown.CountEnd && !ToothGameManager.isPause)
        {
            if (Input.GetMouseButton(0))
            {
                RaycastHit hit;
                if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    SetDestination(hit.transform.gameObject);
                }
            }
            Move();
        }
    }
    private void SetDestination(GameObject gameobject)
    {
        Transform ChildObject = gameobject.transform.GetChild(0);
        destination = ChildObject.position;   //이빨에 자식 오브젝트인 MovePosition을 목적지로 지정
        isMove = true;
    }
    private void Move()
    {
        if (isMove && !ToothGameManager.isPause)
        {
            if (Vector3.Distance(destination, transform.position) <= 0.1f)
            {
                isMove = false; return;
            }
            var dir = destination - transform.position;
            transform.position += dir.normalized * Time.deltaTime * 100f;
        }
    }
}