using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Video;

public class Manager_VideoClipSelection : MonoBehaviour
{
    [Header("Video Player Section Selection")]
    public VideoPlayer videoPlayer;
    public VideoClip videoClip;
    public GameObject videoPlayerUI;
    public double sectionStartSec = 0;
    public double sectionEndSec = 0;
    private double currentVideoTime;

    // Start is called before the first frame update
    void Start()
    {
    }
    void Update()
    {
        currentVideoTime = videoPlayer.time;
    }

    public void SelectSection()
    {
        videoPlayer.clip = videoClip;
        videoPlayer.time = sectionStartSec;
        videoPlayer.Play();

        if (currentVideoTime > sectionEndSec)
        {
            Debug.Log("videoplayer has reached time");
            videoPlayerUI.SetActive(true);
            videoPlayer.Pause();
            videoPlayer.time = sectionStartSec;
        }
    }
}
