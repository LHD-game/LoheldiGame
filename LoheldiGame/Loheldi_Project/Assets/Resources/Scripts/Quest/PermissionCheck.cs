using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Android;

public class PermissionCheck : MonoBehaviour
{
    public void Awake()
    {
        PressBtnCapture();
    }
    bool onCheck = false;

    public void PressBtnCapture()
    {
        Debug.Log("실행1");
        if (onCheck == false)
        {
            Debug.Log("실행2");
            StartCoroutine("PermissionCheckCoroutine");
        }
    }

    IEnumerator PermissionCheckCoroutine()
    {
        Debug.Log("실행3");
        onCheck = true;

        yield return new WaitForEndOfFrame();
        if (Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite) == false)
        {
            Debug.Log("실행4");
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);

            yield return new WaitForSeconds(0.2f); // 0.2초의 딜레이 후 focus를 체크하자.
            yield return new WaitUntil(() => Application.isFocused == true);

            if (Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite) == false)
            {
                Debug.Log("실행5");
                // 다이얼로그를 위해 별도의 플러그인을 사용했기 때문에 주석처리. 그냥 별도의 UI를 만들어주면 됨.
                //AGAlertDialog.ShowMessageDialog("권한 필요", "스크린샷을 저장하기 위해 저장소 권한이 필요합니다.",
                //"Ok", () => OpenAppSetting(),
                //"No!", () => AGUIMisc.ShowToast("저장소 요청 거절됨"));

                OpenAppSetting(); // 원래는 다이얼로그 선택에서 Yes를 누르면 호출됨.

                onCheck = false;
                yield break;
            }
        }

        // 권한을 사용해서 처리하는 부분. 스크린샷이나, 파일 저장 등등.

        onCheck = false;
    }


    // 해당 앱의 설정창을 호출한다.
    // https://forum.unity.com/threads/redirect-to-app-settings.461140/
    private void OpenAppSetting()
    {
        Debug.Log("실행6");
        try
        {
            Debug.Log("실행7");
#if UNITY_ANDROID
            using (var unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            using (AndroidJavaObject currentActivityObject = unityClass.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                string packageName = currentActivityObject.Call<string>("getPackageName");

                using (var uriClass = new AndroidJavaClass("android.net.Uri"))
                using (AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("fromParts", "package", packageName, null))
                using (var intentObject = new AndroidJavaObject("android.content.Intent", "android.settings.APPLICATION_DETAILS_SETTINGS", uriObject))
                {
                    intentObject.Call<AndroidJavaObject>("addCategory", "android.intent.category.DEFAULT");
                    intentObject.Call<AndroidJavaObject>("setFlags", 0x10000000);
                    currentActivityObject.Call("startActivity", intentObject);
                }
            }
#endif
        }
        catch (Exception ex)
        {
            Debug.Log("실행8");
            Debug.LogException(ex);
        }
    }
}
