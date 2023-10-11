using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : SingletonBase<AudioManager>
{
    Dictionary<string, BGM> _bgmDic = new();
    Dictionary<string, AudioClip> _seDic = new();
    AudioSource _seSource;
    protected override void DoAwake()
    {
        foreach (var clip in Resources.LoadAll<AudioClip>("BGM"))
            _bgmDic.Add(clip.name, new BGM(clip, gameObject.AddComponent<AudioSource>()));
        foreach (var clip in Resources.LoadAll<AudioClip>("SE"))
            _seDic.Add(clip.name, clip);
        _seSource = gameObject.AddComponent<AudioSource>();
        _seSource.playOnAwake = false;
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += SceneLoaded;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneLoaded;
    }
    void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "InGame")
            PlayBGM("058_BPM150");
    }
    public void PlaySE(string name)
    {
        if (_seDic.ContainsKey(name))
            _seSource.PlayOneShot(_seDic[name]);
        else
            Debug.LogError("その名前のSEクリップが存在しません");
    }
    public void PlayBGM(string name)
    {
        if (_bgmDic.ContainsKey(name))
        {
            _bgmDic[name].audioSource.clip = _bgmDic[name].clip;
            _bgmDic[name].audioSource.Play();
        }
        else
            Debug.LogError("その名前のBGMクリップが存在しません");
    }
    /// <summary>
    /// BGMのポーズ、ポーズ解除を設定する
    /// </summary>
    /// <param name="name">クリップ名</param>
    /// <param name="flag">trueでポーズ、falseでポーズ解除</param>
    public void PauseBGM(string name, bool flag)
    {
        if (_bgmDic.ContainsKey(name))
        {
            if(flag)
                _bgmDic[name].audioSource.Pause();
            else
                _bgmDic[name].audioSource.UnPause();
        }
        else
            Debug.LogError("その名前のBGMクリップが存在しません");
    }
    public void StopBGM(string name)
    {
        if (_bgmDic.ContainsKey(name))
            _bgmDic[name].audioSource.Stop();
        else
            Debug.LogError("その名前のBGMクリップが存在しません");
    }
}
class BGM
{
    public AudioClip clip;
    public AudioSource audioSource;
    public BGM(AudioClip clip, AudioSource audioSource)
    {
        this.clip = clip;
        this.audioSource = audioSource;
        audioSource.playOnAwake = false;
        audioSource.loop = true;
    }
}