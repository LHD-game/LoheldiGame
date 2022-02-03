using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
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
    public void GotoMainGame()
    {
        SceneManager.LoadScene("GameMove");
    }
}
