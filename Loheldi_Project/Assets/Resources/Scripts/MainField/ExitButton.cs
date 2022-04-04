using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    // exit button에 적용하여 해당 오브젝트를 닫습니다.
    public GameObject gameObject;

    public void Click()
    {
        gameObject.SetActive(false);
    }
}
