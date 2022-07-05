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
                tiptext.text = "��ü���� Ȯ���غ�����! �����̳� Ư�� �̺�Ʈ�� ��ٸ��� �������� �����";
                break;
            case 1:
                tiptext.text = "�̴ϰ����� ���� ���� �ٸ纸�� ���� ����?";
                break;
            case 2:
                tiptext.text = "���� ����Ʈ�� �����ϸ� ����� ����ġ�� ȹ���� �� �־��";
                break;
            case 3:
                tiptext.text = "�׵��� ���� ������ �̱⵵ �غ�����! ���� �������� �̱⿡ ������ �־��";
                break;
            case 4:
                tiptext.text = "���ӿ� �ʿ��� �����͸� �������� ������ ��ø� ��ٷ� �ּ���";
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
