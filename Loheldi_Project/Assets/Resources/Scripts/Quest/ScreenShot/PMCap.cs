using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
public class PMCap : MonoBehaviour
{
    public Camera camera;       //�������� ī�޶�.

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
        public Camera _3DgameCamera;                // ����ȭ��(3D)�� �����ִ� ī�޶�
        public Camera _uiCamera;                    // ui�� �����ִ� ī�޶�
        public Root _uiroot;                        // Screen size�� ��� �ö� ���
        public Texture _uitextureScreenshot;        // ĸ�İ� �Ϸ�� Texture2D �� �����ϱ� ���� ���

        public bool nowCapturing { get; private set; }

        public void CaptureScreen()
        {
            // ����ȭ �÷��� ����
            nowCapturing = true;

            // ȭ�� ĸ�ĸ� ���� �ڷ�ƾ ����
            StartCoroutine(RenderToTexture(_uiroot.manualWidth, _uiroot.manualHeight, _uitextureScreenshot));
        }

        private IEnumerator RenderToTexture(int renderSizeX, int renderSizeY, Texture uitextureForSave)
        {
            // ĸó�� �Ϸ��� �ǵ��� WaitForEndOfFrame ���Ŀ� �ؾ� �մϴ�.
            // �׷��� ������ ���� ���� ���, �� ��� ���� ���� ������ ȭ���� ��� �� �� �ֽ��ϴ�.
            yield return new WaitForEndOfFrame();

            int targetWidth = uitextureForSave.width;
            int targetHeight = uitextureForSave.height;

            //RenderTexture ����
            RenderTexture rt = new RenderTexture(renderSizeX, renderSizeY, 24);
            //RenderTexture ������ ���� Texture2D ����
            Texture2D screenShot = new Texture2D(targetWidth, targetHeight, TextureFormat.ARGB32, false);

            // ī�޶� RenderTexture �Ҵ�
            _3DgameCamera.targetTexture = rt;
            _uiCamera.targetTexture = rt;

            // �� ī�޶� ���� �ִ� ȭ���� ������ �մϴ�.
            _3DgameCamera.Render();
            _uiCamera.Render();

            // read �ϱ� ����, ������ �� RenderTexture�� RenderTexture.active�� ����
            RenderTexture.active = rt;

            // RenderTexture.active�� ������ RenderTexture�� read �մϴ�.
            screenShot.ReadPixels(new Rect(0, 0, renderSizeX, renderSizeY), 0, 0);
            screenShot.Apply();

            // ĸ�İ� �Ϸ� �Ǿ����ϴ�.
            // ���� ĸ�ĵ� Texture2D �� ������ ���ϴ� �ൿ�� �ϸ� �˴ϴ�.

            // ���� UITexture �ʿ� �־�ξ��ٰ�, �����ϴ� �ʿ��� ������ ����Ͽ����ϴ�.
            uitextureForSave.mainTexture = screenShot;

            // File�� ���� �ʹٸ� �Ʒ�ó�� �ϸ� �˴ϴ�.
            //byte[] bytes = screenShot.EncodeToPNG();
            //System.IO.fFile.WriteAllBytes("capture.png", bytes);

            // ����� �͵� ����
            RenderTexture.active = null;
            _3DgameCamera.targetTexture = null;
            _uiCamera.targetTexture = null;
            Destroy(rt);

            // ����ȭ �÷��� ����
            nowCapturing = false;

            yield return 0;
        }
}
