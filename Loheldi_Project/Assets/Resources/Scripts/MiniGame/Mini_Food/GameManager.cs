using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }

    GameObject[] foods;
    GameObject[] lifes;
    int score = 0;
    int highScore = 10;
    int life = 3;

    Image lifeImg;
    Color lifeColor;

    [SerializeField]
    private Text scoreTxt;
    [SerializeField]
    private Text highScoreTxt;
    [SerializeField]
    private GameObject WelcomePanel;
    [SerializeField]
    private GameObject GameOverPanel;
    [SerializeField]
    private GameObject PausePanel;
    
    private float[] foodDropSec = new float[] {1.8f, 1.6f, 1.4f, 1.0f, 0.8f, 0.5f, 0.4f, 0.3f, 0.3f  }; //�߰��ܰ� �� ���� �������� ����
    private float newFoodDropSec;

    private bool stopTrigger = false;   //true�� ���� ���� ����
    private bool pauseTrigger = false;  //true�� ��� �Ͻ�����

    private void Start()
    {
        Welcome();
    }

    IEnumerator CreateFoodRoutine()
    {
        
        while (stopTrigger)
        {
            
            CreateFood();
            yield return new WaitForSeconds(newFoodDropSec);
        }
        Debug.Log("stoptrigger" + stopTrigger);

    }

    public void Welcome()
    {
        GameOverPanel.SetActive(false);
        GameOverPanel.SetActive(false);
        PausePanel.SetActive(false);
        WelcomePanel.SetActive(true);

        pauseTrigger = false;

        //Debug.Log("food.Length: " + foods.Length);
        GameObject playerPos = GameObject.FindWithTag("Player");
        playerPos.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
        playerPos.transform.rotation = new Quaternion(0, 0, 0, 0);
        CreateLife();
        InitScore();
        newFoodDropSec = foodDropSec[0];
        foods = Resources.LoadAll<GameObject>("Prefebs/Foods/");

    }
    public void GameStart()
    {
        stopTrigger = true;
        
        //foods = GameObject.FindGameObjectsWithTag("Food");  //Ȱ��ȭ�� ������Ʈ��!
        StopCoroutine(CreateFoodRoutine());
        StartCoroutine(CreateFoodRoutine());
        WelcomePanel.SetActive(false);
    }


    public void GameOver()
    {
        Time.timeScale = 1f;
        stopTrigger = false;
        StopCoroutine(CreateFoodRoutine());
        GameObject[] disGfoods = GameObject.FindGameObjectsWithTag("GoodFood");
        for (int i = 0; i < disGfoods.Length; i++)
        {
            Destroy(disGfoods[i]);
        }        
        GameObject[] disBfoods = GameObject.FindGameObjectsWithTag("BadFood");
        for (int i = 0; i < disBfoods.Length; i++)
        {
            Destroy(disBfoods[i]);
        }
        GameOverPanel.SetActive(true);
    }

    public void PauseGame()
    {
        if (!pauseTrigger)  //���� �Ͻ�����
        {
            pauseTrigger = true;
            PausePanel.SetActive(true);
        }
        else    //���� �簳
        {
            pauseTrigger = false;   //Ʈ���� ����
            PausePanel.SetActive(false);    //�Ͻ����� �г� ��Ȱ��ȭ
        }
        

    }

    public bool Blocker()
    {
        return stopTrigger;
    }
    public int ReturnLife()
    {
        return life;
    }


    private void CreateFood()
    {
        float randf;
        if (!pauseTrigger)
        {
            while (true)
            {

                randf = Random.Range(-40.0f, 40.0f);
                //food�� ��Ÿ�� ��ǥ��
                Vector3 randpos = Camera.main.WorldToViewportPoint(new Vector3(randf, 0.0f, 0.0f));

                if (randpos.x < 0.05f || randpos.x > 0.95f)
                {
                    continue;
                }
                else
                {
                    randpos = Camera.main.ViewportToWorldPoint(randpos);
                    randpos.y = 7.0f;
                    randpos.z = 0.0f;
                    int randFood = Random.Range(0, foods.Length);
                    Instantiate(foods[randFood], randpos, Quaternion.Euler(0, 0, 0));
                    //��Ÿ�� ������Ʈ, ��ǥ��, ȸ������

                    break;
                }
            }
        }
        
        //Vector3 randpos = Camera.main.ViewportToWorldPoint(new Vector3(randf, 0.0f, 0.0f));
        
    }

    private void CreateLife()
    {
        life = 3;
        lifes = GameObject.FindGameObjectsWithTag("Life");

        for (int i=0; i < lifes.Length; i++)
        {
            lifeImg = lifes[i].GetComponent<Image>();
            lifeColor = lifeImg.color;
            lifeColor.a = 1.0f;
            lifeImg.color = lifeColor;
            //lifes[i].SetActive(true);
        }

    }

    private void InitScore()
    {
        score = 0;
        scoreTxt.text = "����: " + score;
    }

    
    public void ScoreCnt()
    {
        score++;
        if(score > highScore)
        {
            highScore = score;
        }
        scoreTxt.text = "����: " + score;
        highScoreTxt.text = "�ְ�����: " + highScore;
        //Debug.Log("score: "+score);
    }
    public void LifeCnt()
    {
        life--;
        lifeImg = lifes[life].GetComponent<Image>();
        lifeColor = lifeImg.color;
        lifeColor.a = 0.0f;
        lifeImg.color = lifeColor;

        if (life <= 0)
        {
            GameOver();
        }
    }

    public void FoodDropsec(int gLevel)
    {
        newFoodDropSec = foodDropSec[gLevel];
        Debug.Log("FoodDropSec: " + foodDropSec[gLevel]);
    }

}
