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
        Debug.Log("����1");
        if (onCheck == false)
        {
            Debug.Log("����2");
            StartCoroutine("PermissionCheckCoroutine");
        }
    }

    IEnumerator PermissionCheckCoroutine()
    {
        Debug.Log("����3");
        onCheck = true;

        yield return new WaitForEndOfFrame();
        if (Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite) == false)
        {
            Debug.Log("����4");
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);

            yield return new WaitForSeconds(0.2f); // 0.2���� ������ �� focus�� üũ����.
            yield return new WaitUntil(() => Application.isFocused == true);

            if (Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite) == false)
            {
                Debug.Log("����5");
                // ���̾�α׸� ���� ������ �÷������� ����߱� ������ �ּ�ó��. �׳� ������ UI�� ������ָ� ��.
                //AGAlertDialog.ShowMessageDialog("���� �ʿ�", "��ũ������ �����ϱ� ���� ����� ������ �ʿ��մϴ�.",
                //"Ok", () => OpenAppSetting(),
                //"No!", () => AGUIMisc.ShowToast("����� ��û ������"));

                OpenAppSetting(); // ������ ���̾�α� ���ÿ��� Yes�� ������ ȣ���.

                onCheck = false;
                yield break;
            }
        }

        // ������ ����ؼ� ó���ϴ� �κ�. ��ũ�����̳�, ���� ���� ���.

        onCheck = false;
    }


    // �ش� ���� ����â�� ȣ���Ѵ�.
    // https://forum.unity.com/threads/redirect-to-app-settings.461140/
    private void OpenAppSetting()
    {
        Debug.Log("����6");
        try
        {
            Debug.Log("����7");
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
            Debug.Log("����8");
            Debug.LogException(ex);
        }
    }
}
