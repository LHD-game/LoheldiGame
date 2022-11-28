using UnityEngine;
using System.Collections;

public class frame_count : MonoBehaviour
{
    [SerializeField]
    GameObject[] HiddenObjectList = new GameObject[4];

    float deltaTime = 0.0f;
    float fps;
    Coroutine crt;

    void Start()
    {
        crt = StartCoroutine(ChkFps());    
    }
    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        fps = 1.0f / deltaTime;
    }
/*
    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 100;
        style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
        float msec = deltaTime * 1000.0f;
        fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        GUI.Label(rect, text, style);
    }*/

    IEnumerator ChkFps()
    {
        while (true)
        {

            yield return new WaitForSeconds(5.0f);
            Debug.Log("内风凭 角青");
            if (fps < 20.0f)
            {
                for (int i = 0; i < HiddenObjectList.Length; i++)
                {
                    HiddenObjectList[i].SetActive(false);
                }
                Debug.Log("内风凭 辆丰");
                StopCoroutine(crt);
            }
            
        }

    }
}