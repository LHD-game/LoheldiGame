using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    public void Shoot(Vector3 speed)
    {   //Y축으로 200만큼 Z 축으로 2000만큼의 힘으로 발사시키는 함수
        GetComponent<Rigidbody>().AddForce(speed);
    }
}
