using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PMCap : MonoBehaviour
{
    public Camera camera;       //�������� ī�޶�.

    private int resWidth;
    private int resHeight;
    string path;


    public Image ScreenshotImg;
    public GameObject PlayerstatusImg;
    public Image MyInfoImg;
    public Material PlayerImage;
    public Texture PlayerImageTexture;

    public GameObject PCamLight;
    void Start()
    {
        int Time;
        Time = int.Parse(DateTime.Now.ToString("HH"));
        if (Time < 4 || Time > 16) ;
        else
        {
            PCamLight.GetComponent<Light>().intensity = 0.5f;
        }
        if (SceneManager.GetActiveScene().name == "Game_Tooth")
            ScreenshotImg = GameObject.Find("PlayerImage").GetComponent<Image>(); //�̹��� ��� ��;
        resWidth = 2400;
        resHeight = 2400;
        path = Application.dataPath + "/ScreenShot/";
        Debug.Log(path);
        StartCoroutine(ClickScreenShot());

    }

    public IEnumerator ClickScreenShot()
    {
        yield return new WaitForEndOfFrame();
        RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
        camera.targetTexture = rt;
        Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
        camera.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
        screenShot.Apply();

        //ffbyte[] bytes = screenShot.EncodeToPNG();
        //File.WriteAllBytes(name, bytes);
        Sprite sprite = Sprite.Create(screenShot, new Rect(0, 0, screenShot.width, screenShot.height), new Vector2(0.5f, 0.5f));
        //ScreenshotImg.sprite = sprite;

        this.transform.position = this.transform.position + new Vector3(0, -1, 2);

        if (SceneManager.GetActiveScene().name == "MainField")
        {
            PlayerstatusImg.SetActive(true);
            //MyInfoImg.sprite = sprite;
            //PlayerImage.SetTexture("_MainTex", screenShot);
        }
        
        camera.gameObject.SetActive(false);
    }
}
