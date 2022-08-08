using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public AudioClip audioGameSuccess;
    public AudioClip audioGameFail;
    public AudioClip audioBGMOpening;
    public AudioClip audioBGMField;
    public AudioClip audioBGMHouse;
    public AudioClip audioBGMLobby;
    public AudioClip audioBGMFood;
    public AudioClip audioBGMTooth;
    public AudioClip audioBGMCard;
    public AudioClip audioBGMGacha;
    public AudioClip audioBGMRun;
    public AudioClip audioBGMQuest;
    public AudioClip audioBGMQuestEnd;
    public AudioClip audioBGMTutorial;
    public AudioClip audioBGMNight;
    public AudioClip audioHaHasong;
    AudioSource audioSource;

    QuestDontDestroy QDD;

    void Awake()
    {
        QDD = GameObject.Find("DontDestroyQuest").GetComponent<QuestDontDestroy>();
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        this.audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log("OnSceneLoaded: " + scene.name);
        if (scene.name == "Welcome")
        {
            Sound("BGMOpening");
        }
        else if (scene.name == "Housing")
        {
            Sound("BGMNight");
        }
        else if (scene.name == "Game_Lobby")
        {
            Sound("BGMLobby");
        }
        else if (scene.name == "Game_Eating")
        {
            Sound("BGMFood");
        }
        else if (scene.name == "Game_Tooth")
        {
            Sound("BGMTooth");
        }
        else if (scene.name == "Game_Card")
        {
            Sound("BGMCard");
        }
        else if (scene.name == "Game_Running")
        {
            Sound("BGMRun");
        }
        if (scene.name == "Game_Quest")
        {
            Sound("BGMQuest");
        }
        if (scene.name == "MainField")
        {
            if (QDD.QuestIndex.Equals("0_1"))
                Sound("BGMTutorial");
            else
                Sound("BGMField");
        }
    }

    public void Win()
    {
        Sound("GameSuccess");
    }
    public void Loose()
    {
        Sound("GameFail");
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void Sound(string action)
    {
        audioSource.loop = true;
        audioSource.volume = 0.8f * PlayerSett.BGMValue;
        switch (action)
        {
            case "Idle":
                break;
            case "GameSuccess":
                audioSource.clip = audioGameSuccess;
                audioSource.volume = 2f * PlayerSett.BGMValue;
                audioSource.loop = false;
                break;
            case "GameFail":
                audioSource.clip = audioGameFail;
                audioSource.volume = 2f * PlayerSett.BGMValue;
                audioSource.loop = false;
                break;
            case "BGMOpening":
                audioSource.clip = audioBGMOpening;
                break;
            case "BGMField":
                audioSource.clip = audioBGMField;
                break;
            case "BGMHouse":
                audioSource.clip = audioBGMHouse;
                break;
            case "BGMLobby":
                audioSource.clip = audioBGMLobby;
                break;
            case "BGMFood":
                audioSource.clip = audioBGMFood;
                break;
            case "BGMTooth":
                audioSource.clip = audioBGMTooth;
                break;
            case "BGMCard":
                audioSource.clip = audioBGMCard;
                break;
            case "BGMGacha":
                audioSource.clip = audioBGMGacha;
                break;
            case "BGMRun":
                audioSource.clip = audioBGMRun;
                break;
            case "BGMQuest":
                audioSource.clip = audioBGMQuest;
                break;
            case "BGMQuestEnd":
                audioSource.clip = audioBGMQuestEnd;
                break;
            case "BGMTutorial":
                audioSource.clip = audioBGMTutorial;
                break;
            case "BGMNight":
                audioSource.clip = audioBGMNight;
                break;
            case "HaHasong":
                audioSource.clip = audioHaHasong;
                break;
            default:
                break;
        }
        audioSource.Play();
    }
}
