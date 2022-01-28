using BackEnd;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartManager : MonoBehaviour
{
    void Start()
    {
        /*// 초기화
        // [.net4][il2cpp] 사용 시 필수 사용
        Backend.Initialize(() =>
        {
            // 초기화 성공한 경우 실행
            if (Backend.IsInitialized)
            {
                print("뒤끝 초기화 성공");

                // example
                // 버전체크 -> 업데이트
            }
            // 초기화 실패한 경우 실행
            else
            {
                print("뒤끝 초기화 실패");
            }*/
        // 첫 번째 방법 (동기)
        var bro = Backend.Initialize(true);
        if (bro.IsSuccess())
        {
            // 초기화 성공 시 로직
            print("뒤끝 초기화 성공");
        }
        else
        {
            // 초기화 실패 시 로직
            print("뒤끝 초기화 실패");
        }

    /*});*/
    }

    void Update()
    {
        
    }
}
