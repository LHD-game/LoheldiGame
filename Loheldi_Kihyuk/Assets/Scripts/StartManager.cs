using BackEnd;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartManager : MonoBehaviour
{
    void Start()
    {
        /*// �ʱ�ȭ
        // [.net4][il2cpp] ��� �� �ʼ� ���
        Backend.Initialize(() =>
        {
            // �ʱ�ȭ ������ ��� ����
            if (Backend.IsInitialized)
            {
                print("�ڳ� �ʱ�ȭ ����");

                // example
                // ����üũ -> ������Ʈ
            }
            // �ʱ�ȭ ������ ��� ����
            else
            {
                print("�ڳ� �ʱ�ȭ ����");
            }*/
        // ù ��° ��� (����)
        var bro = Backend.Initialize(true);
        if (bro.IsSuccess())
        {
            // �ʱ�ȭ ���� �� ����
            print("�ڳ� �ʱ�ȭ ����");
        }
        else
        {
            // �ʱ�ȭ ���� �� ����
            print("�ڳ� �ʱ�ȭ ����");
        }

    /*});*/
    }

    void Update()
    {
        
    }
}
