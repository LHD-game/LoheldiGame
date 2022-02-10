﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunGameManager : MonoBehaviour
{
    public static int difficulty = 0;

    public Transform[] NPC = new Transform[3];  //hami, nari, himchan 순서대로
    public Transform Player;
    private float NPCz;
    private float Playerz;

    public Rigidbody player;

    public Transform NMarker;
    public Transform PMarker;

    public GameObject WelcomePanel;
    public GameObject GameOverPanel;
    public GameObject PausePanel;
    public GameObject RunBtnPanel;

    public GameObject Win_txt;
    public GameObject Lose_txt;

    private Vector3 markerPos;

    public static bool isPause = false;
    
    void Start()
    {
        markerPos = NMarker.localPosition;
        Reset();

    }

    private Transform nowNPC;
    void Update()
    {
        nowNPC = NPC[difficulty];
        nowNPC.gameObject.SetActive(true);
        if(nowNPC.position.z >= 3000)
        {
            nowNPC.gameObject.GetComponent<RunNPC>().enabled = false;
            RunBtnPanel.SetActive(false);
            Lose_txt.gameObject.SetActive(true);
            GameOverPanel.SetActive(true);
        }
        else if (Player.position.z >= 3000)
        {
            nowNPC.gameObject.GetComponent<RunNPC>().enabled = false;
            RunBtnPanel.SetActive(false);
            Win_txt.gameObject.SetActive(true);
            GameOverPanel.SetActive(true);
        }
        NPCz = nowNPC.position.z;
        Playerz = Player.position.z;

        PMarker.localPosition = new Vector3(Playerz - 1600, 125, 0);
        NMarker.localPosition = new Vector3(NPCz - 1600, 125, 0);

        player.velocity = player.velocity / 1.0085f;
    }

    public void Reset()
    {
        //player, npc, marker 위치 원상복귀, difficulty 값 초기화(0), welcomepanel 활성화, 기타 비활성화
        Vector3 startPpos = new Vector3(100.0f, 0.0f, 0.0f);
        Vector3 startNpos = new Vector3(0.0f, 0.0f, 0.0f);

        Player.position = startPpos;
        NPC[0].position = startNpos;
        NPC[1].position = startNpos;
        NPC[2].position = startNpos;

        NMarker.localPosition = markerPos;
        PMarker.localPosition = markerPos;

        NPC[0].gameObject.SetActive(false);
        NPC[1].gameObject.SetActive(false);
        NPC[2].gameObject.SetActive(false);

        difficulty = 0;
        isPause = false;

        Win_txt.SetActive(false);
        Lose_txt.SetActive(false);

        GameOverPanel.SetActive(false);
        WelcomePanel.SetActive(true);
        PausePanel.SetActive(false);

        NPC[0].gameObject.GetComponent<RunNPC>().enabled = true;
        NPC[1].gameObject.GetComponent<RunNPC>().enabled = true;
        NPC[2].gameObject.GetComponent<RunNPC>().enabled = true;

        player.velocity = new Vector3(0, 0, 0);
    }

    public void GamePause()
    {
        isPause = !isPause;
        if (isPause)
        {
            PausePanel.SetActive(true);
        }
        else
        {
            PausePanel.SetActive(false);
        }
        
    }
}
