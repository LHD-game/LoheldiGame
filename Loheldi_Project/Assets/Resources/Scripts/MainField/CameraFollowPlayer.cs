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
        vec3 = Camera.position - Player.transform.position;                                                     //카메라와 플레이어 사이에 거리를 Vector3형태로 구함
    }

    private void FixedUpdate()
    {
        Vector3 PlayerPosition = Player.transform.position + vec3;                                              //플레이어와 Vector3만큼 떨어진 위치를 구함
        Camera.position = Vector3.SmoothDamp(transform.position, PlayerPosition, ref velocity, SmoothTime);     //해당위치로 카메라를 옮김

        transform.LookAt(Player.transform);                                                                     //카메라가 플레이어를 향함
    }
}
