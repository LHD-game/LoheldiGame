using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    public void Shoot(Vector3 speed)
    {   //Y������ 200��ŭ Z ������ 2000��ŭ�� ������ �߻��Ű�� �Լ�
        GetComponent<Rigidbody>().AddForce(speed);
    }
}
