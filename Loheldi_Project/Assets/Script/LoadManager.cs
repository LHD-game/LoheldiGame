using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadManager : MonoBehaviour
{
    [Header("User Info")]
    public Text Name;
    public Text Age;
    public void LoadGameMove()
    {
        if (PlayerPrefs.HasKey("Name"))
        {
            Name.text = PlayerPrefs.GetString("Name");
            Age.text = PlayerPrefs.GetString("Age").ToString();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        LoadGameMove();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
