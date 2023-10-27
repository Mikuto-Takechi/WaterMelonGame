using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : SingletonBase<AudioManager>
{
    [SerializeField] AudioMixer _audioMixer = default;
    [SerializeField] AudioMixerGroup[] _audioMixerGroup = default;
    Dictionary<string, BGM> _bgmDic = new();
    Dictionary<string, AudioClip> _seDic = new();
    AudioSource _seSource;
    /// <summary>�f�V�x���̍ő�l</summary>
    static readonly float MAX_DECIBEL = 0;
    /// <summary>�f�V�x���̍ŏ��l</summary>
    static readonly float MIN_DECIBEL = -80f;
    /// <summary>�}�X�^�[�O���[�v��</summary>
    static readonly string MASTER_NAME = "Master";
    /// <summary>BGM�O���[�v��</summary>
    static readonly string BGM_NAME = "BGM_Group";
    /// <summary>SE�O���[�v��</summary>
    static readonly string SE_NAME = "SE_Group";
    protected override void DoAwake()
    {
        foreach (var clip in Resources.LoadAll<AudioClip>("BGM"))
            _bgmDic.Add(clip.name, new BGM(clip, gameObject.AddComponent<AudioSource>(), _audioMixerGroup[1]));
        foreach (var clip in Resources.LoadAll<AudioClip>("SE"))
            _seDic.Add(clip.name, clip);
        _seSource = gameObject.AddComponent<AudioSource>();
        _seSource.outputAudioMixerGroup = _audioMixerGroup[2];
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
            Debug.LogError("���̖��O��SE�N���b�v�����݂��܂���");
    }
    public void PlayBGM(string name)
    {
        if (_bgmDic.ContainsKey(name))
        {
            _bgmDic[name].audioSource.clip = _bgmDic[name].clip;
            _bgmDic[name].audioSource.Play();
        }
        else
            Debug.LogError("���̖��O��BGM�N���b�v�����݂��܂���");
    }
    /// <summary>
    /// BGM�̃|�[�Y�A�|�[�Y������ݒ肷��
    /// </summary>
    /// <param name="name">�N���b�v��</param>
    /// <param name="flag">true�Ń|�[�Y�Afalse�Ń|�[�Y����</param>
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
            Debug.LogError("���̖��O��BGM�N���b�v�����݂��܂���");
    }
    public void StopBGM(string name)
    {
        if (_bgmDic.ContainsKey(name))
            _bgmDic[name].audioSource.Stop();
        else
            Debug.LogError("���̖��O��BGM�N���b�v�����݂��܂���");
    }
    public float MasterVolume
    {
        get 
        {
            if(_audioMixer.GetFloat(MASTER_NAME, out float decibel))
            {
                return FromDecibel(decibel);
            }
            return 0;
        }
        set
        {
            _audioMixer.SetFloat(MASTER_NAME , ToDecibel(value));
        }
    }
    public float BGMVolume
    {
        get
        {
            if (_audioMixer.GetFloat(BGM_NAME, out float decibel))
            {
                return FromDecibel(decibel);
            }
            return 0;
        }
        set
        {
            _audioMixer.SetFloat(BGM_NAME, ToDecibel(value));
        }
    }
    public float SEVolume
    {
        get
        {
            if (_audioMixer.GetFloat(SE_NAME, out float decibel))
            {
                return FromDecibel(decibel);
            }
            return 0;
        }
        set
        {
            _audioMixer.SetFloat(SE_NAME, ToDecibel(value));
        }
    }
    /// <summary>
    /// ���l���f�V�x���֕ϊ�����
    /// </summary>
    float ToDecibel(float linear)
    {
        return Mathf.Clamp(Mathf.Log10(Mathf.Clamp(linear, 0f, 1f)) * 20f, MIN_DECIBEL, MAX_DECIBEL);
    }
    /// <summary>
    /// �f�V�x�����琔�l�֕ϊ�����
    /// </summary>
    float FromDecibel(float decibel)
    {
        return Mathf.Pow(10f, decibel / 20f);
    }
}
class BGM
{
    public AudioClip clip;
    public AudioSource audioSource;
    public BGM(AudioClip clip, AudioSource audioSource, AudioMixerGroup audioMixerGroup)
    {
        this.clip = clip;
        this.audioSource = audioSource;
        audioSource.playOnAwake = false;
        audioSource.loop = true;
        audioSource.outputAudioMixerGroup = audioMixerGroup;
    }
}