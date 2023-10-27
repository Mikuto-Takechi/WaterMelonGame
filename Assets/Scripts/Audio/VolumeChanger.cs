using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeChanger : MonoBehaviour
{
    [SerializeField] Slider _masterVolume;
    [SerializeField] Slider _bgmVolume;
    [SerializeField] Slider _seVolume;
    void Start()
    {
        _masterVolume.value = AudioManager.Instance.MasterVolume;
        _bgmVolume.value = AudioManager.Instance.BGMVolume;
        _seVolume.value = AudioManager.Instance.SEVolume;
        _masterVolume.onValueChanged.AddListener(linear => AudioManager.Instance.MasterVolume = linear);
        _bgmVolume.onValueChanged.AddListener(linear => AudioManager.Instance.BGMVolume = linear);
        _seVolume.onValueChanged.AddListener(linear => AudioManager.Instance.SEVolume = linear);
    }
}