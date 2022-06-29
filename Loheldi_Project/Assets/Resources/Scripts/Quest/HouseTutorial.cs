using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HouseTutorial : MonoBehaviour
{
    private int button=0;
    public int tutoi;                            //튜토리얼 하이라이트 이미지용
    private string tutoTxt;
    private Sprite cuttoonImageList;
    public Color color;

    private QuestDontDestroy QDD;
    private void start()
    {
        QDD = GameObject.Find("QuestDontDestroy").GetComponent<QuestDontDestroy>();
        if (QDD.QuestIndex == 0)
            Tutorial();
    }

    public void Tutorial()
    {
        cuttoonImageList = Resources.Load<Sprite>("Sprites/Quest/cuttoon/tutorial/tuto8");
        Debug.Log("튜토리얼 실핻ㅇ22");
        Debug.Log("튜토i=" + tutoi);

        GameObject tutoblack = GameObject.Find("tutoBlack");
        tutoblack.SetActive(true);
        tutoblack.GetComponent<Image>().sprite = cuttoonImageList;
        if(button==0)
            tutoblack.transform.GetChild(0).gameObject.SetActive(true);
        else if (button == 1)
        {
            tutoblack.transform.GetChild(0).gameObject.SetActive(false);
            tutoblack.transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (button == 2)
        {
            tutoblack.transform.GetChild(1).gameObject.SetActive(false);
            tutoblack.transform.GetChild(2).gameObject.SetActive(true);
            QDD.tutorialLoading = true;
        }

        /*if (chat.fade_in_out.Panel == null)
        {
            chat.Cuttoon();
            chat.fade_in_out.Panel = chat.cuttoon.GetComponent<Image>();
            InvokeRepeating("fade_in_out.Fade", 0.5f, 0.1F);
        }
        else
            CancelInvoke("fade_in_out.Fade");*/
    }
}
