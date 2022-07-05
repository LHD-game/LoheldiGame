using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HouseTutorial : MonoBehaviour
{
    public GameObject tutogameobject;
    private int button=0;
    public int tutoi;                            //튜토리얼 하이라이트 이미지용
    private string tutoTxt;
    private Sprite cuttoonImageList;
    public Color color;

    private QuestDontDestroy QDD;
    void Start()
    {
        Debug.Log("하우징 스타트");
        QDD = GameObject.Find("DontDestroyQuest").GetComponent<QuestDontDestroy>();
        if (QDD.QuestIndex == 0)
            Tutorial();
    }

    public void Tutorial()
    {
        tutogameobject.SetActive(true);
        Debug.Log("튜토리얼 실핻ㅇ22");
        QDD.tutorialLoading = true;

        GameObject tutoblack = GameObject.Find("tutoBlack");
        tutoblack.SetActive(true);
        color.a = 1f;
        tutoblack.GetComponent<Image>().color = color;
        if (button == 0)
        {
            cuttoonImageList = Resources.Load<Sprite>("Sprites/Quest/cuttoon/tutorial/tuto8");
            tutoblack.GetComponent<Image>().sprite = cuttoonImageList;
            tutoblack.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (button == 1)
        {
            cuttoonImageList = Resources.Load<Sprite>("Sprites/Quest/cuttoon/tutorial/tuto5");
            tutoblack.GetComponent<Image>().sprite = cuttoonImageList;
            tutoblack.transform.GetChild(0).gameObject.SetActive(false);
            tutoblack.transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (button == 2)
        {
            cuttoonImageList = Resources.Load<Sprite>("Sprites/Quest/cuttoon/tutorial/tuto1");
            tutoblack.GetComponent<Image>().sprite = cuttoonImageList;
            tutoblack.transform.GetChild(1).gameObject.SetActive(false);
            tutoblack.transform.GetChild(2).gameObject.SetActive(true);
        }
        else if (button == 3)
        {
            cuttoonImageList = Resources.Load<Sprite>("Sprites/Quest/cuttoon/tutorial/tuto5");
            tutoblack.GetComponent<Image>().sprite = cuttoonImageList;
            tutoblack.transform.GetChild(2).gameObject.SetActive(false);
            tutoblack.transform.GetChild(3).gameObject.SetActive(true);
        }
        else if (button == 4)
        {
            cuttoonImageList = Resources.Load<Sprite>("Sprites/Quest/cuttoon/tutorial/tuto1");
            tutoblack.GetComponent<Image>().sprite = cuttoonImageList;
            tutoblack.transform.GetChild(3).gameObject.SetActive(false);
            tutoblack.transform.GetChild(4).gameObject.SetActive(true);
        }
        button++;

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
