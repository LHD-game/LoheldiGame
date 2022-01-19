using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickBack : MonoBehaviour
{
    public void ChangeBack()
    {
        SceneManager.LoadScene("LoginScene");
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
