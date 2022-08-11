using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
public class ScreenShot : MonoBehaviour
{
    string timestamp;
    string fileName; 
    public GameObject ScreenshotGameobject;
    public ScreenShotFlash Flash;

    public string folderName = "ScreenShots";
    public void Capture()
    {
        timestamp = System.DateTime.Now.ToString("yyyy-MM-dd-mm-ss");
        fileName = "Loheldi-SCREENSHOT-" + timestamp + ".png";

#if UNITY_IPHONE || UNITY_ANDROID
        StartCoroutine(CaptureScreenForMobile(fileName));
#else
        StartCoroutine(CaptureScreenForPC(fileName));
#endif
    }

    private IEnumerator CaptureScreenForPC(string FileName)
    {
        ScreenCapture.CaptureScreenshot("~/Downloads/" + FileName);
        yield break;
    }

    
    private IEnumerator CaptureScreenForMobile(string FileName)
    {
        Texture2D texture = ScreenCapture.CaptureScreenshotAsTexture();
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        // do something with texture
        string albumName = "Çï½Ãºô¸®Áö";
        NativeGallery.SaveImageToGallery(texture, albumName, FileName, (success, path) =>
        {
            Debug.Log(success);
            Debug.Log(path);
        });

        // cleanup

        yield return new WaitForEndOfFrame();
        Image ScreenshotImg = ScreenshotGameobject.GetComponent<Image>();
        StartCoroutine(ReadScreenShotFileAndShow(ScreenshotImg, sprite));
        yield break;
    }

    public void Exit(Texture2D Texture)
    {
        ScreenshotGameobject.SetActive(false);
        UnityEngine.Object.Destroy(Texture);
    }

    private Texture2D texture;
    private IEnumerator ReadScreenShotFileAndShow(Image destination, Sprite sprite)
    {
        Flash.Show();
        yield return new WaitForSeconds(0.8f);
        //Sprite sprite = Sprite.Create(Texture, new Rect(0, 0, Texture.width, Texture.height), new Vector2(0.5f, 0.5f));
        destination.sprite = sprite;

        ScreenshotGameobject.SetActive(true);
        yield break;
    }
}
