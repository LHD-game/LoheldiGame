using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enter_Exit : MonoBehaviour //�г� ����/�ݱ⿡ ���Ǵ� Ŭ����
{
    public GameObject SoundEffectManager;

    public void ExitBtn(GameObject exitThis)    //�ش� ������Ʈ�� ��Ȱ��ȭ
    {
        GameObject SoundManager = GameObject.Find("SoundManager");
        SoundEffectManager.GetComponent<SoundEffect>().Sound("ClickBack");
        exitThis.SetActive(false);
        if (exitThis.name == "GachaUI")
        {
            SoundManager.GetComponent<SoundManager>().Sound("BGMField");
        }
    }
     
    public void EnterBtn(GameObject enterThis)  //�ش� ������Ʈ�� Ȱ��ȭ
    {
        SoundEffectManager.GetComponent<SoundEffect>().Sound("ClickIcon");
        enterThis.SetActive(true);
    }
}
