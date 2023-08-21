using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPosition : MonoBehaviour
{
    void Start()
    {
        // Hide the sprite when the game starts.
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (var spriteRenderer in spriteRenderers)
        {
            spriteRenderer.enabled = false;
        }
    }

}
