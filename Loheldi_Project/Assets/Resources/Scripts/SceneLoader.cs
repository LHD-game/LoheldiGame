using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private VirtualJoystick joystic;
    private static SceneLoader _instance;
    private QuestDontDestroy QDD;
    public GameObject Player;
    public static SceneLoader instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SceneLoader>();
            }
            return _instance;
        }
    }
    //Login
    public void GotoUserInfo()
    {
        LoadingSceneManager.LoadScene("UserInfo");
    }
    public void GotoMainField()
    {
        Input.multiTouchEnabled = false;
        LoadingSceneManager.LoadScene("MainField");
    }
    public void GotoGameMove()  // 테스트 용 - 이민진 5/3
    {
        LoadingSceneManager.LoadScene("GameMove");
    }

    public void GotoCreateAcc()
    {
        LoadingSceneManager.LoadScene("CreateAcc");
    }
    public void GotoPlayerCustom()
    {
        PlayerTransForm();
        LoadingSceneManager.LoadScene("PlayerCustom");
    }
    public void GotoPlayerCloset()
    {
        PlayerTransForm();
        LoadingSceneManager.LoadScene("PlayerCloset");
    }


    //Mini Game
    public void GotoLobby()
    {
        Input.multiTouchEnabled = true;
        if (SceneManager.GetActiveScene().name == "MainField")
            PlayerTransForm();
        LoadingSceneManager.LoadScene("Game_Lobby");
    }
    public void GotoEatingGame()
    {
        LoadingSceneManager.LoadScene("Game_Eating");
    }
    public void GotoCardGame()
    {
        LoadingSceneManager.LoadScene("Game_Card");
    }
    public void GotoRunningGame()
    {
        LoadingSceneManager.LoadScene("Game_Running");
    }
    public void GotoToothGame()
    {
        LoadingSceneManager.LoadScene("Game_Tooth");
    }

    public void GotoComditionWindow() //삭제될 친구
    {
        LoadingSceneManager.LoadScene("CharacterCondition");
    }

    public void GotoHouse()
    {
        PlayerTransForm();
        LoadingSceneManager.LoadScene("Housing");
    }
    public void GotoMail()
    {
        LoadingSceneManager.LoadScene("Mail");
    }

    public void GotoTestGacha()
    {
        LoadingSceneManager.LoadScene("Gacha");
    }
    public void GotoQuizGame()
    {
        PlayerTransForm();
        LoadingSceneManager.LoadScene("Quiz");
    }

    private void PlayerTransForm()
    {
        if (SceneManager.GetActiveScene().name == "MainField")
        {
            QDD = GameObject.Find("DontDestroyQuest").GetComponent<QuestDontDestroy>();
            QDD.LastPlayerTransform.transform.position = Player.transform.position;
            Debug.Log(QDD.LastPlayerTransform);
        }
    }
}
