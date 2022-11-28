using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IAPTest : MonoBehaviour
{
    public Text comment;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Success()
    {
        comment.text = "good";

    }
    public void Fail()
    {
        comment.text = "no good";
    }
}
