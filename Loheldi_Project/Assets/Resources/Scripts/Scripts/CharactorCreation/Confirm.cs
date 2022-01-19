using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Confirm : MonoBehaviour
{
    public void ConfirmClick()
    {
        SceneManager.LoadScene("GameMove");
    }
}