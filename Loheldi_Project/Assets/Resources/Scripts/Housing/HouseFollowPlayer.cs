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
        vec3 = Camera.position - Player.transform.position;                                                     //카메라와 플레이어 사이에 거리를 Vector3형태로 구함
    }

    private void FixedUpdate()
    {
        vecPlayer = Player.transform.position + vec4;
        Vector3 PlayerPosition = Player.transform.position + vec3;                                              //플레이어와 Vector3만큼 떨어진 위치를 구함
        Camera.position = Vector3.SmoothDamp(transform.position, PlayerPosition, ref velocity, SmoothTime);     //해당위치로 카메라를 옮김

        transform.LookAt(vecPlayer);                                                                     //카메라가 플레이어를 향함
    }
}
