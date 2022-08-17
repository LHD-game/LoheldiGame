using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
public class PMCap : MonoBehaviour
{
    public Camera camera;       //보여지는 카메라.

    private int resWidth;
    private int resHeight;
    string path;
    public Image ScreenshotImg;
    public RenderTexture rt;
    public Sprite Psprite;
    // Use this for initialization
    void Start()
    {
        resWidth = Screen.width;
        resHeight = Screen.height;
        path = Application.dataPath + "/ScreenShot/";
        Debug.Log(path);
        ClickScreenShot();
    }

    public void ClickScreenShot()
    {
        DirectoryInfo dir = new DirectoryInfo(path);
        if (!dir.Exists)
        {
            Directory.CreateDirectory(path);
        }
        Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
        Rect rec = new Rect(0, 0, screenShot.width, screenShot.height);

        Sprite sprite = Sprite.Create(screenShot, new Rect(0, 0, screenShot.width, screenShot.height), new Vector2(0.5f, 0.5f));
        Psprite = sprite;
        ScreenshotImg.sprite = sprite;
    }
        public Camera _3DgameCamera;                // 게임화면(3D)을 보여주는 카메라
        public Camera _uiCamera;                    // ui를 보여주는 카메라
        public Root _uiroot;                        // Screen size를 얻어 올때 사용
        public Texture _uitextureScreenshot;        // 캡쳐가 완료된 Texture2D 를 보관하기 위해 사용

        public bool nowCapturing { get; private set; }

        public void CaptureScreen()
        {
            // 동기화 플래그 설정
            nowCapturing = true;

            // 화면 캡쳐를 위한 코루틴 시작
            StartCoroutine(RenderToTexture(_uiroot.manualWidth, _uiroot.manualHeight, _uitextureScreenshot));
        }

        private IEnumerator RenderToTexture(int renderSizeX, int renderSizeY, Texture uitextureForSave)
        {
            // 캡처를 하려면 되도록 WaitForEndOfFrame 이후에 해야 합니다.
            // 그렇지 않으면 운이 나쁜 경우, 다 출력 되지 않은 상태의 화면을 찍게 될 수 있습니다.
            yield return new WaitForEndOfFrame();

            int targetWidth = uitextureForSave.width;
            int targetHeight = uitextureForSave.height;

            //RenderTexture 생성
            RenderTexture rt = new RenderTexture(renderSizeX, renderSizeY, 24);
            //RenderTexture 저장을 위한 Texture2D 생성
            Texture2D screenShot = new Texture2D(targetWidth, targetHeight, TextureFormat.ARGB32, false);

            // 카메라에 RenderTexture 할당
            _3DgameCamera.targetTexture = rt;
            _uiCamera.targetTexture = rt;

            // 각 카메라가 보고 있는 화면을 랜더링 합니다.
            _3DgameCamera.Render();
            _uiCamera.Render();

            // read 하기 위해, 랜더링 된 RenderTexture를 RenderTexture.active에 설정
            RenderTexture.active = rt;

            // RenderTexture.active에 설정된 RenderTexture를 read 합니다.
            screenShot.ReadPixels(new Rect(0, 0, renderSizeX, renderSizeY), 0, 0);
            screenShot.Apply();

            // 캡쳐가 완료 되었습니다.
            // 이제 캡쳐된 Texture2D 를 가지고 원하는 행동을 하면 됩니다.

            // 저는 UITexture 쪽에 넣어두었다가, 공유하는 쪽에서 꺼내서 사용하였습니다.
            uitextureForSave.mainTexture = screenShot;

            // File로 쓰고 싶다면 아래처럼 하면 됩니다.
            //byte[] bytes = screenShot.EncodeToPNG();
            //System.IO.fFile.WriteAllBytes("capture.png", bytes);

            // 사용한 것들 해제
            RenderTexture.active = null;
            _3DgameCamera.targetTexture = null;
            _uiCamera.targetTexture = null;
            Destroy(rt);

            // 동기화 플래그 해제
            nowCapturing = false;

            yield return 0;
        }
}
