using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPin : MonoBehaviour
{
    public GameObject Owner;                //���� �޸� �Ű�ü ����

    void Update()                           //�Ű�ü�� ��ġ + y200�� ��ġ���� ���� �����̰� ����
    {
        transform.position = Owner.transform.position + new Vector3(0, 200, 0);
    }
}
