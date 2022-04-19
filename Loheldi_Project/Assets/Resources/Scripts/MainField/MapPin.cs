using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPin : MonoBehaviour
{
    public GameObject Owner;                //핀이 달릴 매개체 선언

    void Update()                           
    {
        if (this.gameObject.layer == 8)                                             //건물보다 NPC에 핀이 항상 위에있도록 하기
        {
            transform.position = Owner.transform.position + new Vector3(0, 200, 0);         //매개체에 위치 + y200에 위치에서 같이 움직이게 지정
        }
        else
        {
            transform.position = Owner.transform.position + new Vector3(0, 300, 0);         //매개체에 위치 + y300에 위치에서 같이 움직이게 지정
        }
    }
}
