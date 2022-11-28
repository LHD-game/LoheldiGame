using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoScript : MonoBehaviour
{
    public GameObject myVideo;
    public VideoPlayer videoClip;
    public VideoClip[] VideoClip = new VideoClip[2];
    //bool play = false;

    public void OnPlayVideo()
    {
        videoClip.loopPointReached += CheckOver;
        myVideo.SetActive(true);
        videoClip.Play();
        //play = true;
    }

    void CheckOver(VideoPlayer vp)
    {
        finishButtonActive();
    }
    public void OnFinishVideo()
    {
        myVideo.SetActive(false);
        videoClip.Stop();
    }

    public void OnResetVideo()
    {
        videoClip.time = 0f;
        videoClip.playbackSpeed = 1f;
        videoClip.Play();
    }

    public GameObject finishButton;
    void finishButtonActive()
    {
        finishButton.SetActive(true);
    }
}
