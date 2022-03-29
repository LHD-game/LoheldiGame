using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public GameObject Player;
    public Transform Camera;
    public Vector3 vec3;
    public float SmoothTime = 1f;
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        vec3 = Camera.position - Player.transform.position;                                                     //ī�޶�� �÷��̾� ���̿� �Ÿ��� Vector3���·� ����
    }

    private void FixedUpdate()
    {
        Vector3 PlayerPosition = Player.transform.position + vec3;                                              //�÷��̾�� Vector3��ŭ ������ ��ġ�� ����
        Camera.position = Vector3.SmoothDamp(transform.position, PlayerPosition, ref velocity, SmoothTime);     //�ش���ġ�� ī�޶� �ű�

        transform.LookAt(Player.transform);                                                                     //ī�޶� �÷��̾ ����
    }
}
