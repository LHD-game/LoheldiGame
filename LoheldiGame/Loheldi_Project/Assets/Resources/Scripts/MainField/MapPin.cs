using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPin : MonoBehaviour
{
    public GameObject Owner;                //���� �޸� �Ű�ü ����

    public void Start()
    {
        StartCoroutine("MapPins");
    }
    IEnumerator MapPins()                           
    {
        while (true)
        {
            if (this.gameObject.layer == 8)                                             //�ǹ����� NPC�� ���� �׻� �����ֵ��� �ϱ�
            {
                transform.position = Owner.transform.position + new Vector3(0, 200, 0);         //�Ű�ü�� ��ġ + y200�� ��ġ���� ���� �����̰� ����
            }
            else
            {
                transform.position = Owner.transform.position + new Vector3(0, 300, 0);         //�Ű�ü�� ��ġ + y300�� ��ġ���� ���� �����̰� ����
            }
            yield return null;
        }
    }
}
