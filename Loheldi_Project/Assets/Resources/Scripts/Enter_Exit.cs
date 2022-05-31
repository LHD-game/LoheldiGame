using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enter_Exit : MonoBehaviour //패널 열기/닫기에 사용되는 클래스
{
    public static GameObject SoundManager;

    private void Start()
    {
        SoundManager = this.gameObject;
    }

    public static void ExitBtn(GameObject exitThis)    //해당 오브젝트를 비활성화
    {
        SoundManager.GetComponent<SoundEffect>().Sound("ClickBack");
        exitThis.SetActive(false);
    }
     
    public static void EnterBtn(GameObject enterThis)  //해당 오브젝트를 활성화
    {
        SoundManager.GetComponent<SoundEffect>().Sound("ClickIcon");
        enterThis.SetActive(true);
    }
}
