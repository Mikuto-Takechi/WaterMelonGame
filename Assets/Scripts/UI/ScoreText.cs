using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreText : MonoBehaviour
{
    [SerializeField] TextType _textType;
    Text _text;
    void Start()
    {
        _text = GetComponent<Text>();
    }
    void Update()
    {
        _text.text = _textType switch
        {
            TextType.Score => GameManager.Instance.ScoreText,
            TextType.BestScore => GameManager.Instance.SaveData.BestScore.ToString(),
            _ => throw new System.NotImplementedException(),
        };
    }
    enum TextType
    {
        Score,
        BestScore,
    }
}