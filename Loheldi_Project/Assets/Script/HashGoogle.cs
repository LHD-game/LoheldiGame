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
