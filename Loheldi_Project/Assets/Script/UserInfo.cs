using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInfo : MonoBehaviour
{
    [Header("User Info")]

    public Text userNick;
    public Text userID;
    public Text userPW;
    public Text userEmail;
    public void Load()
    {
        if (PlayerPrefs.HasKey("ID"))
        {
            userID.text = PlayerPrefs.GetString("ID");
            userPW.text = PlayerPrefs.GetString("PW").ToString();
            userEmail.text = PlayerPrefs.GetString("Email").ToString();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Load();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
