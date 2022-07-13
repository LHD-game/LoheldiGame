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
        Debug.Log("OnSceneLoaded: " + scene.name);
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
        switch (action)
        {
            case "Idle":
                break;
            case "GameSuccess":
                audioSource.clip = audioGameSuccess;
                audioSource.loop = false;
                break;
            case "GameFail":
                audioSource.clip = audioGameFail;
                audioSource.loop = false;
                break;
            case "BGMOpening":
                audioSource.clip = audioBGMOpening;
                audioSource.volume = 0.1f;
                audioSource.loop = true;
                break;
            case "BGMField":
                audioSource.clip = audioBGMField;
                audioSource.volume = 0.1f;
                audioSource.loop = true;
                break;
            case "BGMHouse":
                audioSource.clip = audioBGMHouse;
                audioSource.volume = 0.1f;
                audioSource.loop = true;
                break;
            case "BGMLobby":
                audioSource.clip = audioBGMLobby;
                audioSource.volume = 0.1f;
                audioSource.loop = true;
                break;
            case "BGMFood":
                audioSource.clip = audioBGMFood;
                audioSource.volume = 0.1f;
                audioSource.loop = true;
                break;
            case "BGMTooth":
                audioSource.clip = audioBGMTooth;
                audioSource.volume = 0.1f;
                audioSource.loop = true;
                break;
            case "BGMCard":
                audioSource.clip = audioBGMCard;
                audioSource.volume = 0.1f;
                audioSource.loop = true;
                break;
            case "BGMGacha":
                audioSource.clip = audioBGMGacha;
                audioSource.volume = 0.1f;
                audioSource.loop = true;
                break;
            case "BGMRun":
                audioSource.clip = audioBGMRun;
                audioSource.volume = 0.1f;
                audioSource.loop = true;
                break;
            case "BGMQuest":
                audioSource.clip = audioBGMQuest;
                audioSource.volume = 0.1f;
                audioSource.loop = true;
                break;
            case "BGMQuestEnd":
                audioSource.clip = audioBGMQuestEnd;
                audioSource.volume = 0.1f;
                audioSource.loop = true;
                break;
            case "BGMTutorial":
                audioSource.clip = audioBGMTutorial;
                audioSource.volume = 0.1f;
                audioSource.loop = true;
                break;
            case "BGMNight":
                audioSource.clip = audioBGMNight;
                audioSource.volume = 0.1f;
                audioSource.loop = true;
                break;
            default:
                break;
        }
        audioSource.Play();
    }
}
