using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    public AudioClip audioVomit;
    public AudioClip audioLevelUp;
    public AudioClip audioCount;
    public AudioClip audioCountFinish;
    public AudioClip audioFootSteps1;
    public AudioClip audioFootSteps2;
    public AudioClip audioFootSteps3;
    public AudioClip audioCardCurrect;
    public AudioClip audioGameSuccess;
    public AudioClip audioGameFail;
    public AudioClip audioReward;
    public AudioClip audioClickIcon;
    public AudioClip audioJump;
    public AudioClip audioOpenDoor;
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
            case "Vomit":
                audioSource.clip = audioVomit;       //100%
                break;
            case "LevelUp":
                audioSource.clip = audioLevelUp;     //100%
                break;
            case "Count":
                audioSource.clip = audioCount;       //100%
                break;
            case "CountFinish":
                audioSource.clip = audioCountFinish; //100%
                break;
            case "FootSteps1":
                audioSource.clip = audioFootSteps1;  //100%
                break;
            case "FootSteps2":
                audioSource.clip = audioFootSteps2;  //100%
                break;
            case "FootSteps3":
                audioSource.clip = audioFootSteps3;  //100%
                break;
            case "CardCurrect":
                audioSource.clip = audioCardCurrect; //100%
                break;
            case "GameSuccess":
                audioSource.clip = audioGameSuccess; //25%
                break;
            case "GameFail":
                audioSource.clip = audioGameFail;    //25% (음식먹기, 카드 뒤집기에 성공 실패 기준이 없음)
                break;
            case "Reward":
                audioSource.clip = audioReward;      //0%
                break;
            case "ClickIcon":
                audioSource.clip = audioClickIcon;   //0%
                break;
            case "Jump":
                audioSource.clip = audioJump;        //100%
                break;
            case "OpenDoor":
                audioSource.clip = audioOpenDoor;    //50%
                break;
            default:
                break;
        }
        audioSource.Play();
    }
}
