using BackEnd;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HashGoogle : MonoBehaviour
{
    public InputField input; 
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

    public void GetGoogleHash()
    {
        string googlehash = Backend.Utils.GetGoogleHash();

        if(!string.IsNullOrEmpty(googlehash))
        {
            Debug.Log(googlehash);
            if (input != null)
                input.text = googlehash;
            
                 
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
