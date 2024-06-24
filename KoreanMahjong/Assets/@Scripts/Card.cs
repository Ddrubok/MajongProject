using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardInfo
{
    public string _shape;
}


public class Card : MonoBehaviour
{
    SpriteRenderer _spriteRenderer;
    TextMeshPro _textMeshPro;

    string _shape;

    public void init(string shape)
    {
        _shape = shape;
    }
    void Start()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _textMeshPro = GetComponentInChildren<TextMeshPro>();

        _textMeshPro.text = _shape;
    }
}
