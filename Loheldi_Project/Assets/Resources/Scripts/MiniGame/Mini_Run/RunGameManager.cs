using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunGameManager : MonoBehaviour
{
    public static int difficulty = 0;

    public Transform NPC1;
    public Transform NPC2;
    public Transform NPC3;
    public Transform Player;

    public Transform NMarker;
    public Transform PMarker;

    public GameObject WelcomePanel;
    public GameObject GameOverPanel;
    public GameObject RunBtnPanel;

    public GameObject Win_txt;
    public GameObject Lose_txt;

    private Vector3 markerPos;
    
    void Start()
    {
        markerPos = NMarker.localPosition;
        Reset();

    }
    void Update()
    {
        if (difficulty == 1)
        {
            NPC1.gameObject.SetActive(true);
            if (NPC1.position.z >= 857)
            {
                NPC1.gameObject.GetComponent<RunHamiRun>().enabled = false;
                RunBtnPanel.SetActive(false);
                Lose_txt.gameObject.SetActive(true);
                GameOverPanel.SetActive(true);
            }
            if (Player.position.z >= 857)
            {
                NPC1.gameObject.GetComponent<RunHamiRun>().enabled = false;
                RunBtnPanel.SetActive(false);
                Win_txt.gameObject.SetActive(true);
                GameOverPanel.SetActive(true);
            }
        }
        if (difficulty == 2)
        {
            NPC2.gameObject.SetActive(true);
            if (NPC2.position.z >= 857)
            {
                NPC2.gameObject.GetComponent<RunNariRun>().enabled = false;
                RunBtnPanel.SetActive(false);
                Lose_txt.gameObject.SetActive(true);
                GameOverPanel.SetActive(true);
            }
            if (Player.position.z >= 857)
            {
                NPC2.gameObject.GetComponent<RunNariRun>().enabled = false;
                RunBtnPanel.SetActive(false);
                Win_txt.gameObject.SetActive(true);
                GameOverPanel.SetActive(true);
            }
        }
        if (difficulty == 3)
        {
            NPC3.gameObject.SetActive(true);
            if (NPC3.position.z >= 857)
            {
                NPC3.gameObject.GetComponent<RunHimchanRun>().enabled = false;
                RunBtnPanel.SetActive(false);
                Lose_txt.gameObject.SetActive(true);
                GameOverPanel.SetActive(true);
            }
            if (Player.position.z >= 857)
            {
                NPC3.gameObject.GetComponent<RunHimchanRun>().enabled = false;
                RunBtnPanel.SetActive(false);
                Win_txt.gameObject.SetActive(true);
                GameOverPanel.SetActive(true);
            }
        }
    }

    public void Reset()
    {
        //player, npc, marker 위치 원상복귀, difficulty 값 초기화(0), welcomepanel 활성화, 기타 비활성화
        Vector3 startPpos = new Vector3(100.0f, 0.0f, 0.0f);
        Vector3 startNpos = new Vector3(0.0f, 0.0f, 0.0f);

        Player.position = startPpos;
        NPC1.position = startNpos;
        NPC2.position = startNpos;
        NPC3.position = startNpos;

        NMarker.localPosition = markerPos;
        PMarker.localPosition = markerPos;

        NPC1.gameObject.SetActive(false);
        NPC2.gameObject.SetActive(false);
        NPC3.gameObject.SetActive(false);

        difficulty = 0;

        Win_txt.SetActive(false);
        Lose_txt.SetActive(false);

        GameOverPanel.SetActive(false);
        WelcomePanel.SetActive(true);

        NPC1.gameObject.GetComponent<RunHamiRun>().enabled = true;
        NPC2.gameObject.GetComponent<RunNariRun>().enabled = true;
        NPC3.gameObject.GetComponent<RunHimchanRun>().enabled = true;
    }
}
