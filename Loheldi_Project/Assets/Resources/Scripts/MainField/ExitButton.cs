using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    // exit button�� �����Ͽ� �ش� ������Ʈ�� �ݽ��ϴ�.
    public GameObject gameObject;

    public void Click()
    {
        gameObject.SetActive(false);
    }
}
