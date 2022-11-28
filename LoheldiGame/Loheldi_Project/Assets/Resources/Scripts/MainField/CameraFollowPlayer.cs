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

    private void Awake()
    {
        vec3 = Camera.position - Player.transform.position;                                                     //ī�޶�� �÷��̾� ���̿� �Ÿ��� Vector3���·� ����
        StartCoroutine("CameraPlayerFollow");
    }

    
    IEnumerator CameraPlayerFollow()
    {
        while (true)
        {
            Vector3 PlayerPosition = Player.transform.position + vec3;                                              //�÷��̾�� Vector3��ŭ ������ ��ġ�� ����
            Camera.position = Vector3.SmoothDamp(transform.position, PlayerPosition, ref velocity, SmoothTime);     //�ش���ġ�� ī�޶� �ű�

            transform.LookAt(Player.transform);
            yield return null;
        }//ī�޶� �÷��̾ ����
    }
}
