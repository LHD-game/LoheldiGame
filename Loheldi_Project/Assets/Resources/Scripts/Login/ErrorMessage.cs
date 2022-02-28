using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorMessage : MonoBehaviour
{

    public GameObject Error;
    public Text Id;
    public Text Passward;
    //private string Passward;
    private bool Idmessage;
    private bool Pasmessage;
   


    public void ErrorActive()
    {
        //Passward = Passward.ToString();
       


        if (string.Equals("sowon", Id))
        {
            Error.SetActive(false);
            Debug.Log("ÆÞ½º");
        }
       else
        {
            Error.SetActive(true);
            Debug.Log("Æ©¸£");
            Debug.Log(Id.ToString());
        }
        if (Pasmessage == false)
        {
            Error.SetActive(true);
        }
    }
}
