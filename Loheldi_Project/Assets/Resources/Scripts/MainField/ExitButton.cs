using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{

    public GameObject gameObject;

    public void Click()
    {
        gameObject.SetActive(false);
    }
}
