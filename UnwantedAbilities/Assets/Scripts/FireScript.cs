using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour
{
    // Array to hold the frames of the fire animation
    public Sprite[] fireFrames; 
    public float frameRate = 0.1f; 
    private SpriteRenderer spriteRenderer;
    private int currentFrameIndex = 0;
    private float frameTimer = 0f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = fireFrames[0]; 
    }

    void Update()
    {
        // Update frame based on frame rate
        frameTimer += Time.deltaTime;
        if (frameTimer >= frameRate)
        {
            frameTimer -= frameRate;
            currentFrameIndex = (currentFrameIndex + 1) % fireFrames.Length;
            spriteRenderer.sprite = fireFrames[currentFrameIndex];
        }
    }
}
