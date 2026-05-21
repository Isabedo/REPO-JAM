using UnityEngine;
using UnityEngine.Events;


public class SpriteFlipbook : MonoBehaviour
{
    [Header("Frames")]
    public Sprite[] frames;

    [Header("Timing")]
    public float frameTime = 0.15f; // segundos por frame

    [Header("Playback")]
    public bool playOnStart = true;
    public bool loop = true;
    [Header("collider")]
    public GameObject _collider;

    private SpriteRenderer spriteRenderer;
    private int currentFrame;
    private float timer;
    private bool isPlaying;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        if (frames.Length > 0)
            spriteRenderer.sprite = frames[0];

        isPlaying = playOnStart;
    }

    void Update()
    {
        if (!isPlaying || frames.Length == 0)
            return;

        timer += Time.deltaTime;

        if (timer >= frameTime)
        {
            timer = 0f;
            NextFrame();
        }
    }

    void NextFrame()
    {
        currentFrame++;

        if (currentFrame >= frames.Length)
        {
            if (loop)
                currentFrame = 0;
            else
            {
                currentFrame = frames.Length - 1;
                isPlaying = false;
                Destroy(_collider);

            }
        }

        spriteRenderer.sprite = frames[currentFrame];
    }

    public void Play()
    {
        isPlaying = true;
    }

    public void Stop()
    {
        isPlaying = false;
    }

    public void SetFrame(int frameIndex)
    {
        if (frameIndex < 0 || frameIndex >= frames.Length)
            return;

        currentFrame = frameIndex;
        spriteRenderer.sprite = frames[currentFrame];
    }

    public void ResetAnimation()
    {
        currentFrame = 0;
        timer = 0f;
        spriteRenderer.sprite = frames[0];
    }
}
