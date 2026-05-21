using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mask_rotation : MonoBehaviour
{
    public float rotationSpeed = 60f;
    public float floatAmplitude = 0.30f;
    public float floatSpeed = 2f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);

        float y_floating = Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.position = startPos + Vector3.up * y_floating;
    }
}