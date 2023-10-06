using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreText : MonoBehaviour
{
    Text _text;
    void Start()
    {
        _text = GetComponent<Text>();
    }
    void Update()
    {
        _text.text = $"Score : {GameManager.Instance.ScoreText}";
    }
}
