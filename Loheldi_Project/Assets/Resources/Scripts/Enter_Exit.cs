using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enter_Exit : MonoBehaviour //�г� ����/�ݱ⿡ ���Ǵ� Ŭ����
{
    public static void ExitBtn(GameObject exitThis)    //�ش� ������Ʈ�� ��Ȱ��ȭ
    {
        exitThis.SetActive(false);
    }
     
    public static void EnterBtn(GameObject enterThis)  //�ش� ������Ʈ�� Ȱ��ȭ
    {
        enterThis.SetActive(true);
    }
}
