using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{
    public static string nextScene;
    [SerializeField] Slider progressBar;

    public static Text tiptext;

    private void Start()
    {
        tiptext = GameObject.Find("Text").GetComponent<Text>();
        int tipnum = Random.Range(0, 4);
        switch (tipnum)
        {
            case 0:
                tiptext.text = "우체통을 확인해보세요! 선물이나 특별 이벤트가 기다리고 있을지도 몰라요";
                break;
            case 1:
                tiptext.text = "미니게임을 통해 집을 꾸며보는 것은 어떨까요?";
                break;
            case 2:
                tiptext.text = "당일 퀘스트에 도전하면 보상과 경험치를 획득할 수 있어요";
                break;
            case 3:
                tiptext.text = "그동안 모은 돈으로 뽑기도 해보세요! 레어 아이템이 뽑기에 숨겨져 있어요";
                break;
            case 4:
                tiptext.text = "게임에 필요한 데이터를 가져오고 있으니 잠시만 기다려 주세요";
                break;
            default:
                break;
        }
        StartCoroutine(LoadScene());
    }

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("Loading");
    }

    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        float timer = 0.0f;
        while (!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime;
            if (op.progress < 0.9f)
            {
                progressBar.value = Mathf.Lerp(progressBar.value, op.progress, timer);
                if (progressBar.value >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                progressBar.value = Mathf.Lerp(progressBar.value, 1f, timer);
                if (progressBar.value == 1.0f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
