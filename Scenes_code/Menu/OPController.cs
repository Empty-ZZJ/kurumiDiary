using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class OPController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Image imageDisplay;
    public GameObject mainCanvas;
    public GameObject logo;
    public List<VideoClip> videoList;
    public List<Sprite> imageList;

    private int currentVideoIndex = 0;
    private int currentImageIndex = 0;

    void Start ()
    {
        PlayNextMedia();
    }

    void PlayNextMedia ()
    {
        if (currentVideoIndex < videoList.Count)
        {

            PlayVideo();
        }
        else if (currentImageIndex < imageList.Count)
        {
            logo.SetActive(true);
            videoPlayer.gameObject.SetActive(false);
            PlayImage();
        }
        else
        {
            // 所有视频和图片已经播放完毕
            GameObject.Find("LogoCanvas").GetComponent<Logo>().Fide();
        }
    }

    void PlayVideo ()
    {
        VideoClip video = videoList[currentVideoIndex];
        videoPlayer.clip = video; // 假设您已经有一个名为videoPlayer的VideoPlayer组件
        videoPlayer.Play(); // 播放视频
        currentVideoIndex++;
        // 根据视频的长度来设置延迟调用的时间，确保在视频播放完成后继续下一个媒体
        Invoke(nameof(PlayNextMedia), (float)video.length);
    }


    void PlayImage ()
    {
        Sprite image = imageList[currentImageIndex];
        // 在imageDisplay上显示图片
        imageDisplay.sprite = image;
        Debug.Log("正在显示图片");

        currentImageIndex++;
        Invoke(nameof(PlayNextMedia), 3f); // 假设每张图片显示3秒钟
    }
}
