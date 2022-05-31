using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enter_Exit : MonoBehaviour //�г� ����/�ݱ⿡ ���Ǵ� Ŭ����
{
    public static GameObject SoundManager;

    private void Start()
    {
        SoundManager = this.gameObject;
    }

    public static void ExitBtn(GameObject exitThis)    //�ش� ������Ʈ�� ��Ȱ��ȭ
    {
        SoundManager.GetComponent<SoundEffect>().Sound("ClickBack");
        exitThis.SetActive(false);
    }
     
    public static void EnterBtn(GameObject enterThis)  //�ش� ������Ʈ�� Ȱ��ȭ
    {
        SoundManager.GetComponent<SoundEffect>().Sound("ClickIcon");
        enterThis.SetActive(true);
    }
}
