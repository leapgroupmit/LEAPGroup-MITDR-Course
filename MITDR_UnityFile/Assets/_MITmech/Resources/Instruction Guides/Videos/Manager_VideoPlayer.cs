using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Video;

public class Manager_VideoPlayer : MonoBehaviour
{
    [Header("Video Player Settings")]
    public float scrubSeconds = 5.0f;
    public GameObject videoProgress;

    [Header("Video Player")]
    public VideoPlayer videoPlayer;
    public RawImage videoImage;
    public ArrayList videoList;

    /*
    [Header("Video Player UI")]
    public Button uIPlayButton;
    public Button uIPauseButton;
    public Button uIPlusSecondsButton;
    public Button uIMinusSecondsButton;
    */

    // Start is called before the first frame update
    void Start()
    {
        //videoImage.texture = videoPlayer.texture;
    }

    public void PlayVideo()
    {
        videoPlayer.Play();
    }

    public void PauseVideo()
    {
        videoPlayer.Pause();
    }

    public void VideoForward()
    {
        videoPlayer.time += scrubSeconds;
    }

    public void VideoBackward()
    {
        videoPlayer.time -= scrubSeconds;
    }
}
