using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enter_Exit : MonoBehaviour //�г� ����/�ݱ⿡ ���Ǵ� Ŭ����
{
    public GameObject SoundEffectManager;
    public GameObject JoyStick;
    public Interaction Inter;

    public Camera Camera;
    public Camera FarmCamera;

    public void ExitBtn(GameObject exitThis)    //�ش� ������Ʈ�� ��Ȱ��ȭ
    {
        GameObject SoundManager = GameObject.Find("SoundManager");
        SoundEffectManager.GetComponent<SoundEffect>().Sound("ClickBack");
        if (exitThis.name == "GachaUI")
        {
            SoundManager.GetComponent<SoundManager>().Sound("BGMField");
        }
        if (Inter)
            if (Inter.Farm)
            {
                FarmCamera.enabled = false;
                Camera.enabled = true;
                JoyStick.SetActive(true);
            }
        exitThis.SetActive(false);
    }
     
    public void EnterBtn(GameObject enterThis)  //�ش� ������Ʈ�� Ȱ��ȭ
    {
        SoundEffectManager.GetComponent<SoundEffect>().Sound("ClickIcon");
        enterThis.SetActive(true);
    }
}
