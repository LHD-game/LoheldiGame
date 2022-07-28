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
            //초기화 성공 시 로직
            Debug.Log("초기화 성공!");
            //CustomSignUp();
        }
        else
        {
            // 초기화 실패 시 로직
            Debug.LogError("초기화 실패!");
        }
    }
}