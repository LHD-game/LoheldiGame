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
        
    }

    public void OnPauseVideo()
    {
        myVideo.SetActive(false);
        videoClip.Pause();
    }

    public void OnResetVideo()
    {
        myVideo.SetActive(false);
        videoClip.Stop();
        
    }

}
