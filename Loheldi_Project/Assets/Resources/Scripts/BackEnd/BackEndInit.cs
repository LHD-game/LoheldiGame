using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;

public class BackEndInit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var bro = Backend.Initialize(true);
        if (bro.IsSuccess())
        {
            //�ʱ�ȭ ���� �� ����
            Debug.Log("�ʱ�ȭ ����!");
            //CustomSignUp();
        }
        else
        {
            // �ʱ�ȭ ���� �� ����
            Debug.LogError("�ʱ�ȭ ����!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
