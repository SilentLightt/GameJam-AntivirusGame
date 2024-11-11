using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer; // Reference to SpriteRenderer
    [SerializeField] private float _x, _y;

    private Material _material;

    void Start()
    {
        // Get the material of the sprite
        _material = _spriteRenderer.material;
    }

    void Update()
    {
        // Move the texture by modifying the material's offset
        Vector2 offset = new Vector2(_x, _y) * Time.deltaTime;
        _material.mainTextureOffset += offset;
    }
}