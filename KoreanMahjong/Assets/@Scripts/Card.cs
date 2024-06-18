using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{
    SpriteRenderer _spriteRenderer;
    TextMeshPro _textMeshPro;

    Card(char shape)
    {

    }
    void Start()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _textMeshPro = GetComponentInChildren<TextMeshPro>();

        _textMeshPro.text = "¤¿";
    }
}
