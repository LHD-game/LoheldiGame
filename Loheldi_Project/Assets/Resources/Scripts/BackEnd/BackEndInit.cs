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
            CheckVersion();
            //CustomSignUp();
        }
        else
        {
            // 초기화 실패 시 로직
            Debug.LogError("초기화 실패!");
        }
    }

    void CheckVersion()
    {
        //Example(비동기 및 SendQueue에서도 동일한 로직으로 사용할 수 있습니다.)
        var bro = Backend.Utils.GetLatestVersion();
        string version = bro.GetReturnValuetoJSON()["version"].ToString();
        //최신 버전일 경우
        if (version == Application.version)
        {
            Debug.Log("최신버전입니다.");
            return;
        }

        //현재 앱의 버전과 버전관리에서 설정한 버전이 맞지 않을 경우
        string forceUpdate = bro.GetReturnValuetoJSON()["type"].ToString();

        if (forceUpdate == "1") //업데이트 방식: 선택
        {
            Debug.Log("업데이트를 하시겠습니까? y/n");
        }
        else if (forceUpdate == "2") //업데이트 방식: 강제
        {
            Debug.Log("업데이트가 필요합니다. 스토어에서 업데이트를 진행해주시기 바랍니다");

            //해당 앱의 스토어로 가게 해주는 유니티 함수
            #if UNITY_ANDROID
                    Application.OpenURL("market://details?id=" + Application.identifier);
            #elif UNITY_IOS
                    Application.OpenURL("https://itunes.apple.com/kr/app/apple-store/" + "id1461432877");
            #endif
        }
    }
}