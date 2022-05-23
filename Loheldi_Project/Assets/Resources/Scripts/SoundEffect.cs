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
    public AudioClip audioOpening;
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
                break;
            case "FoodNotGood":
                audioSource.clip = audioFoodNotGood;
                break;
            case "LevelUp":
                audioSource.clip = audioLevelUp;
                break;
            case "RunCount":
                audioSource.clip = audioRunCount;
                audioSource.volume = 0.05f;
                break;
            case "RunCountFinish":
                audioSource.clip = audioRunCountFinish;
                audioSource.volume = 0.05f;
                break;
            case "RunFootSteps1":
                audioSource.clip = audioRunFootSteps1;
                audioSource.volume = 0.5f;
                break;
            case "RunFootSteps2":
                audioSource.clip = audioRunFootSteps2;
                audioSource.volume = 0.5f;
                break;
            case "RunFootSteps3":
                audioSource.clip = audioRunFootSteps3;
                audioSource.volume = 0.5f;
                break;
            case "RunClose":
                audioSource.clip = audioRunClose;
                break;
            case "CardFlip":
                audioSource.clip = audioCardFlip;
                break;
            case "CardCurrect":
                audioSource.clip = audioCardCurrect;
                break;
            case "CardWrong":
                audioSource.clip = audioCardWrong;
                break;
            case "audioGameTimeless":
                audioSource.clip = audioGameTimeless;
                break;
            case "GameSuccess":
                audioSource.clip = audioGameSuccess;
                break;
            case "GameFail":
                audioSource.clip = audioGameFail;
                break;
            case "Reward":
                audioSource.clip = audioReward;
                break;
            case "ClickIcon":
                audioSource.clip = audioClickIcon;
                break;
            case "audioClickBack":
                audioSource.clip = audioClickBack;
                break;
            case "Jump":
                audioSource.clip = audioJump;
                break;
            case "OpenDoor":
                audioSource.clip = audioOpenDoor;
                break;
            case "Opening":
                audioSource.clip = audioOpening;
                break;
            default:
                break;
        }
        audioSource.Play();
    }
}
