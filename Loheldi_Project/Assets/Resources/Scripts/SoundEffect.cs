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
            default:
                break;
        }
        audioSource.Play();
    }
}
