using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    public bool firstStart=false;
    public GameObject DontDestrotObject;
    // Start is called before the first frame update
    void Start()
    {
        //firstStart = false;
    }

    public void ChageScene()
    {
        if (SceneManager.GetActiveScene().name == "MainField")
            DontDestroyOnLoad(DontDestrotObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
