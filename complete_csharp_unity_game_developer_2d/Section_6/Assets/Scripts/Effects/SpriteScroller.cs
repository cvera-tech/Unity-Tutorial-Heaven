using System;
using UnityEngine;

public class SpriteScroller : MonoBehaviour
{
    [SerializeField] private Vector2 _scrollSpeed;

    private Vector2 _offset;
    private Material _material;
    private void Start()
    {
        _material = GetComponent<SpriteRenderer>().material;
    }

    private void Update()
    {
        _offset = _scrollSpeed * Time.deltaTime;
        float newOffsetX = _material.mainTextureOffset.x + _offset.x;
        if (newOffsetX >= 1f)
        {
            newOffsetX -= 1f;
        }
        float newOffsetY = _material.mainTextureOffset.y + _offset.y;
        if (newOffsetY >= 1f)
        {
            newOffsetY -= 1f;
        }
        _material.mainTextureOffset = new(newOffsetX, newOffsetY);
        // Debug.Log(_material.mainTextureOffset);
    }
}
