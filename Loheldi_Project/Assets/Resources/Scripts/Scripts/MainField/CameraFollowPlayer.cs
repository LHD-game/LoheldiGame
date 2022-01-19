using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public GameObject Player;

    void Update()
    {
        transform.position = Player.transform.position + new Vector3(0, 10, -13);
    }
}
