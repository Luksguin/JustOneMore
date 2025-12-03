using System;
using UnityEngine;

public class SpriteRendererManager : MonoBehaviour
{
    public Renderer myRenderer;

    private void Update()
    {
        myRenderer.sortingOrder = (int) Math.Floor(1000f - (transform.position.y * 100));
    }
}
