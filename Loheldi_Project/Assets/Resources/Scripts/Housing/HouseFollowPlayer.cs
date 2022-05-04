using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseFollowPlayer : MonoBehaviour
{
    public GameObject Player;
    public Transform Camera;
    private Vector3 vec3;
    private Vector3 vec4;
    private Vector3 vecPlayer;
    public float SmoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        vec4 = new Vector3(0, 1, 0);
        vec3 = Camera.position - Player.transform.position;                                                     //ī�޶�� �÷��̾� ���̿� �Ÿ��� Vector3���·� ����
    }

    private void FixedUpdate()
    {
        vecPlayer = Player.transform.position + vec4;
        Vector3 PlayerPosition = Player.transform.position + vec3;                                              //�÷��̾�� Vector3��ŭ ������ ��ġ�� ����
        Camera.position = Vector3.SmoothDamp(transform.position, PlayerPosition, ref velocity, SmoothTime);     //�ش���ġ�� ī�޶� �ű�

        transform.LookAt(vecPlayer);                                                                     //ī�޶� �÷��̾ ����
    }
}
