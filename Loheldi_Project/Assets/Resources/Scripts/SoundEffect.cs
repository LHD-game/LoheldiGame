using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    public AudioClip audioFoodGood;
    public AudioClip audioFoodNotGood;
    public AudioClip audioRunCount;
    public AudioClip audioRunCountFinish;
    public AudioClip audioRunFootSteps1;
    public AudioClip audioRunFootSteps2;
    public AudioClip audioRunFootSteps3;
    public AudioClip audioRunClose;
    public AudioClip audioCardFlip;
    public AudioClip audioCardCurrect;
    public AudioClip audioCardWrong;
    public AudioClip audioGameTimeless;
    public AudioClip audioGameSuccess;
    public AudioClip audioGameFail;
    public AudioClip audioReward;
    public AudioClip audioLevelUp;
    public AudioClip audioClickIcon;
    public AudioClip audioClickBack;
    public AudioClip audioJump;
    public AudioClip audioOpenDoor;
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
    public AudioClip audioBGMNight;
    AudioSource audioSource;

    private void Awake()
    {
        this.audioSource = GetComponent<AudioSource>();
    }

    public void Sound(string action)
    {
        switch (action)
        {
            case "Idle":
                break;
            case "FoodGood":
                audioSource.clip = audioFoodGood;
                audioSource.loop = false;
                break;
            case "FoodNotGood":
                audioSource.clip = audioFoodNotGood;
                audioSource.loop = false;
                break;
            case "LevelUp":
                audioSource.clip = audioLevelUp;
                audioSource.loop = false;
                break;
            case "RunCount":
                audioSource.clip = audioRunCount;
                audioSource.volume = 0.05f;
                audioSource.loop = false;
                break;
            case "RunCountFinish":
                audioSource.clip = audioRunCountFinish;
                audioSource.volume = 0.05f;
                audioSource.loop = false;
                break;
            case "RunFootSteps1":
                audioSource.clip = audioRunFootSteps1;
                audioSource.volume = 0.5f;
                audioSource.loop = false;
                break;
            case "RunFootSteps2":
                audioSource.clip = audioRunFootSteps2;
                audioSource.volume = 0.5f;
                audioSource.loop = false;
                break;
            case "RunFootSteps3":
                audioSource.clip = audioRunFootSteps3;
                audioSource.volume = 0.5f;
                audioSource.loop = false;
                break;
            case "RunClose":
                audioSource.clip = audioRunClose;
                audioSource.loop = false;
                break;
            case "CardFlip":
                audioSource.clip = audioCardFlip;
                audioSource.loop = false;
                break;
            case "CardCurrect":
                audioSource.clip = audioCardCurrect;
                audioSource.loop = false;
                break;
            case "CardWrong":
                audioSource.clip = audioCardWrong;
                audioSource.loop = false;
                break;
            case "audioGameTimeless":
                audioSource.clip = audioGameTimeless;
                audioSource.loop = false;
                break;
            case "GameSuccess":
                audioSource.clip = audioGameSuccess;
                audioSource.loop = false;
                break;
            case "GameFail":
                audioSource.clip = audioGameFail;
                audioSource.loop = false;
                break;
            case "Reward":
                audioSource.clip = audioReward;
                audioSource.loop = false;
                break;
            case "ClickIcon":
                audioSource.clip = audioClickIcon;
                audioSource.loop = false;
                break;
            case "ClickBack":
                audioSource.clip = audioClickBack;
                audioSource.loop = false;
                break;
            case "Jump":
                audioSource.clip = audioJump;
                audioSource.loop = false;
                break;
            case "OpenDoor":
                audioSource.clip = audioOpenDoor;
                audioSource.loop = false;
                break;
            case "BGMOpening":
                audioSource.clip = audioBGMOpening;
                audioSource.volume = 1f;
                audioSource.loop = true;
                break;
            case "BGMField":
                audioSource.clip = audioBGMField;
                audioSource.volume = 1f;
                audioSource.loop = true;
                break;
            case "BGMHouse":
                audioSource.clip = audioBGMHouse;
                audioSource.volume = 1f;
                audioSource.loop = true;
                break;
            case "BGMLobby":
                audioSource.clip = audioBGMLobby;
                audioSource.volume = 1f;
                audioSource.loop = true;
                break;
            case "BGMFood":
                audioSource.clip = audioBGMFood;
                audioSource.volume = 1f;
                audioSource.loop = true;
                break;
            case "BGMTooth":
                audioSource.clip = audioBGMTooth;
                audioSource.volume = 1f;
                audioSource.loop = true;
                break;
            case "BGMCard":
                audioSource.clip = audioBGMCard;
                audioSource.volume = 1f;
                audioSource.loop = true;
                break;
            case "BGMGacha":
                audioSource.clip = audioBGMGacha;
                audioSource.volume = 1f;
                audioSource.loop = true;
                break;
            case "BGMRun":
                audioSource.clip = audioBGMRun;
                audioSource.volume = 1f;
                audioSource.loop = true;
                break;
            case "BGMQuest":
                audioSource.clip = audioBGMQuest;
                audioSource.volume = 1f;
                audioSource.loop = true;
                break;
            case "BGMQuestEnd":
                audioSource.clip = audioBGMQuestEnd;
                audioSource.volume = 1f;
                audioSource.loop = true;
                break;
            case "BGMNight":
                audioSource.clip = audioBGMNight;
                audioSource.volume = 1f;
                audioSource.loop = true;
                break;
            default:
                break;
        }
        audioSource.Play();
    }
}
