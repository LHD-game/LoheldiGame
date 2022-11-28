using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HouseTutorial : MonoBehaviour
{
    public GameObject tutogameobject;
    public int button=0;
    private Sprite cuttoonImageList;
    public Color color;

    private QuestDontDestroy QDD;
    void Start()
    {
        Debug.Log("�Ͽ�¡ ��ŸƮ");
        QDD = GameObject.Find("DontDestroyQuest").GetComponent<QuestDontDestroy>();
        if (QDD.QuestIndex.Equals("0_1"))
            Tutorial();
    }

    public void Tutorial()
    {
        tutogameobject.SetActive(true);
        Debug.Log("Ʃ�丮�� ���P��22");
        QDD.tutorialLoading = true;

        GameObject tutoblack = tutogameobject.transform.GetChild(0).gameObject;
        //color.a = 0f;
        //tutoblack.GetComponent<Image>().color = color;
        if (button == 0)
        {
            //cuttoonImageList = Resources.Load<Sprite>("Sprites/Quest/cuttoon/tutorial/tuto8");
            //tutoblack.GetComponent<Image>().sprite = cuttoonImageList;
            
            tutoblack.SetActive(true);
            tutoblack.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (button == 1)
        {
           // cuttoonImageList = Resources.Load<Sprite>("Sprites/Quest/cuttoon/tutorial/tuto5");
            //tutoblack.GetComponent<Image>().sprite = cuttoonImageList;
            tutoblack.transform.GetChild(0).gameObject.SetActive(false);
            tutoblack.transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (button == 2)
        {
            //cuttoonImageList = Resources.Load<Sprite>("Sprites/Quest/cuttoon/tutorial/tuto1");
            //tutoblack.GetComponent<Image>().sprite = cuttoonImageList;
            tutoblack.transform.GetChild(1).gameObject.SetActive(false);
            tutoblack.transform.GetChild(2).gameObject.SetActive(true);
        }
        else if (button == 3)
        {
            //cuttoonImageList = Resources.Load<Sprite>("Sprites/Quest/cuttoon/tutorial/tuto5");
            //tutoblack.GetComponent<Image>().sprite = cuttoonImageList;
            tutoblack.transform.GetChild(2).gameObject.SetActive(false);
            tutoblack.transform.GetChild(3).gameObject.SetActive(true);
        }
        else if (button == 4)
        {
            //cuttoonImageList = Resources.Load<Sprite>("Sprites/Quest/cuttoon/tutorial/tuto1");
            //tutoblack.GetComponent<Image>().sprite = cuttoonImageList;
            tutoblack.transform.GetChild(3).gameObject.SetActive(false);
            tutoblack.transform.GetChild(4).gameObject.SetActive(true);
        }
        button++;
    }
}
