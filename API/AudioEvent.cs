using System;
using UnityEngine;

/// <summary>
/// AudioSource的播放状态监听，包含一个AudioSource公共成员.
/// 使用方法：
/// AudioEvent ae =AudioEvent.AddComponentToGameObject(t1.gameObject);
/// ae.audioSource.clip = clip1;//自己控制赋值
/// ae.EventPlayStart += OnEventPlayStart;
/// ae.EventPlayEnd += OnEventPlayEnd;
/// ae.audioSource.Play();
/// </summary>
internal class AudioEvent : MonoBehaviour
{
    /// <summary>
    /// 这个脚本所在的物体上的audioSource
    /// </summary>
    public AudioSource audioSource;

    /// <summary>
    /// 播放开始事件
    /// </summary>
    public event Action<AudioEvent> EventPlayStart;

    /// <summary>
    /// 播放结束事件
    /// </summary>
    public event Action<AudioEvent> EventPlayEnd;

    /// <summary>
    /// 监控播放状态
    /// </summary>
    private bool _lastPlayStatus;

    /// <summary>
    /// 往一个物体上添加这个事件监听类
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static AudioEvent AddComponentToGameObject (GameObject obj)
    {
        AudioEvent com = obj.GetComponent<AudioEvent>();
        if (com == null)
        {
            com = obj.AddComponent<AudioEvent>();
        }
        return com;
    }

    private void Awake ()
    {
        //如果没有这个AudioSource东西那就要添加一个
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = false;
            audioSource.Stop();
            audioSource.playOnAwake = false;
        }
        _lastPlayStatus = false;
    }

    /// <summary>
    /// 监测一下当前播放状态
    /// </summary>
    private void UpdatePlaySstatus ()
    {
        if (_lastPlayStatus == false && audioSource.isPlaying == true)
        {
            if (EventPlayStart != null)
            {
                EventPlayStart(this);//发出事件：开始播放
            }
        }
        if (_lastPlayStatus == true && audioSource.isPlaying == false)
        {
            if (EventPlayEnd != null)
            {
                EventPlayEnd(this);//发出事件：播放停止
            }
        }
        _lastPlayStatus = audioSource.isPlaying;
    }

    public void Update ()
    {
        UpdatePlaySstatus();
    }

    private void OnDestoryed ()
    {
        //如果被销毁了是否需要发出播放停止事件？
        if (_lastPlayStatus == true)
        {
            if (EventPlayEnd != null)
            {
                EventPlayEnd(this);//发出事件：播放停止
            }
        }
    }
}