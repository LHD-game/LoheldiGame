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
    public AudioClip ToothBrush;
    public AudioClip audioGameTimeless;
    public AudioClip audioGameSuccess;
    public AudioClip audioGameFail;
    public AudioClip audioReward;
    public AudioClip audioLevelUp;
    public AudioClip audioClickIcon;
    public AudioClip audioClickBack;
    public AudioClip audioJump;
    public AudioClip audioOpenDoor;


    public AudioClip QaudioWind;
    public AudioClip QaudioWater;
    public AudioClip QaudioBird;

    AudioSource audioSource;

    private void Awake()
    {
        this.audioSource = GetComponent<AudioSource>();
    }

    public void Sound(string action)
    {
        audioSource.loop = false;
        switch (action)
        {
            case "Idle":
                break;
            case "FoodGood":
                audioSource.clip = audioFoodGood;
                audioSource.volume = 2 * PlayerSett.SEValue;
                break;
            case "FoodNotGood":
                audioSource.clip = audioFoodNotGood;
                audioSource.volume = 2 * PlayerSett.SEValue;
                break;
            case "LevelUp":
                audioSource.clip = audioLevelUp;
                audioSource.volume = 2 * PlayerSett.SEValue;
                break;
            case "RunCount":
                audioSource.clip = audioRunCount;
                audioSource.volume = 0.1f * PlayerSett.SEValue;
                break;
            case "RunCountFinish":
                audioSource.clip = audioRunCountFinish;
                audioSource.volume = 0.1f * PlayerSett.SEValue;
                break;
            case "RunFootSteps1":
                audioSource.clip = audioRunFootSteps1;
                audioSource.volume = 0.1f * PlayerSett.SEValue;
                break;
            case "RunFootSteps2":
                audioSource.clip = audioRunFootSteps2;
                audioSource.volume = 1f * PlayerSett.SEValue;
                break;
            case "RunFootSteps3":
                audioSource.clip = audioRunFootSteps3;
                audioSource.volume = 1f * PlayerSett.SEValue;
                break;
            case "RunClose":
                audioSource.clip = audioRunClose;
                audioSource.volume = 2 * PlayerSett.SEValue;
                break;
            case "CardFlip":
                audioSource.clip = audioCardFlip;
                audioSource.volume = 2 * PlayerSett.SEValue;
                break;
            case "CardCurrect":
                audioSource.clip = audioCardCurrect;
                audioSource.volume = 2 * PlayerSett.SEValue;
                break;
            case "CardWrong":
                audioSource.clip = audioCardWrong;
                audioSource.volume = 2 * PlayerSett.SEValue;
                break;
            case "ToothBrushing":
                audioSource.clip = ToothBrush;
                audioSource.volume = 2 * PlayerSett.SEValue;
                break;
            case "audioGameTimeless":
                audioSource.clip = audioGameTimeless;
                audioSource.volume = 2 * PlayerSett.SEValue;
                break;
            case "GameSuccess":
                audioSource.clip = audioGameSuccess;
                audioSource.volume = 2 * PlayerSett.SEValue;
                break;
            case "GameFail":
                audioSource.clip = audioGameFail;
                audioSource.volume = 2 * PlayerSett.SEValue;
                break;
            case "Reward":
                audioSource.clip = audioReward;
                audioSource.volume = 2 * PlayerSett.SEValue;
                break;
            case "ClickIcon":
                audioSource.clip = audioClickIcon;
                audioSource.volume = 2 * PlayerSett.SEValue;
                break;
            case "ClickBack":
                audioSource.clip = audioClickBack;
                audioSource.volume = 2 * PlayerSett.SEValue;
                break;
            case "Jump":
                audioSource.clip = audioJump;
                audioSource.volume = 2 * PlayerSett.SEValue;
                break;
            case "QWind":
                audioSource.clip = QaudioWind;
                audioSource.volume = 2 * PlayerSett.SEValue;
                break;
            case "QWater":
                audioSource.clip = QaudioWater;
                audioSource.volume = 2 * PlayerSett.SEValue;
                break;
            case "QBird":
                audioSource.clip = QaudioBird;
                audioSource.volume = 2 * PlayerSett.SEValue;
                break;
            default:
                break;
                
        }
        audioSource.Play();
    }
}
