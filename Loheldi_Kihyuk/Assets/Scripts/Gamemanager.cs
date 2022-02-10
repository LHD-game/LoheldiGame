using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;
    public InputField ID;
    public InputField PW;
    public Text userID;
    public Text userPW;
    public bool login;

    void Awake()
    {
        if (instance == null)
            instance = this;
        /*else if (instance != this)
            Destroy(gameObject);*/
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
