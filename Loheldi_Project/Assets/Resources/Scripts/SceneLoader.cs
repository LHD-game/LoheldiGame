using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private VirtualJoystick joystic;
    private static SceneLoader _instance;
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
        SceneManager.LoadScene("UserInfo");
    }
    public void GotoMainField()
    {
        SceneManager.LoadScene("MainField");
    }
    public void GotoGameMove()  // 테스트 용 - 이민진 5/3
    {
        SceneManager.LoadScene("GameMove");
    }

    public void GotoCreateAcc()
    {
        SceneManager.LoadScene("CreateAcc");
    }
    public void GotoPlayerCustom()
    {
        SceneManager.LoadScene("PlayerCustom");
    }


    //Mini Game
    public void GotoLobby()
    {
        SceneManager.LoadScene("Game_Lobby");
    }
    public void GotoEatingGame()
    {
        SceneManager.LoadScene("Game_Eating");
    }
    public void GotoCardGame()
    {
        SceneManager.LoadScene("Game_Card");
    }
    public void GotoRunningGame()
    {
        SceneManager.LoadScene("Game_Running");
    }
    public void GotoToothGame()
    {
        SceneManager.LoadScene("Game_Tooth");
    }

    public void GotoComditionWindow()
    {
        SceneManager.LoadScene("CharacterCondition");
    }

    public void GotoHouse()
    { 
        SceneManager.LoadScene("Housing");
    }
    public void GotoMail()
    {
        SceneManager.LoadScene("Mail");
    }
}
