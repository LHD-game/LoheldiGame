using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoScript : MonoBehaviour
{
    public GameObject myVideo;
    public VideoPlayer videoClip;
    public VideoClip[] VideoClip = new VideoClip[2];
    public void OnPlayVideo()
    {
        myVideo.SetActive(true);
        videoClip.Play();
        Invoke("finishButtonActive", 30f);
    }

    public void OnPauseVideo()
    {
        myVideo.SetActive(false);
        videoClip.Pause();
        Invoke("finishButtonActive", 30f);
    }

    public void OnResetVideo()
    {
        videoClip.time = 0f;
        videoClip.playbackSpeed = 1f;
    }

    public GameObject finishButton;
    void finishButtonActive()
    {
        finishButton.SetActive(true);
    }
}
