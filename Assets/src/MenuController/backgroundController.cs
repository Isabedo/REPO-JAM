using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class backgroundController : MonoBehaviour
{
    [Header("Scroll Speed")]
    public Vector2 scrollSpeed = new Vector2(0.02f, 0.0f);

    private Material material;
    private Vector2 offset;

    void Awake()
    {
        Image img = GetComponent<Image>();
        material = img.material;
    }

    void Update()
    {
        offset += scrollSpeed * Time.deltaTime;
        material.mainTextureOffset = offset;
    }
}

