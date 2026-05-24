using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class room_2pianoTriggers : MonoBehaviour
{
    public int keyId; // 1, 2, 3, 4
    public room_2pianoController manager;

    [Header("Colores")]
    public Color idleColor = Color.white;
    public Color activeColor = Color.green;

    private Renderer rend;
    private bool used = false;
    private float time;
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.color = idleColor;
    }
    void Update()
    {
        time += Time.deltaTime;
        if (time>15 && manager.canPlay)
        {
            Debug.Log("entra");
            ResetVisual();
            time = 0;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (used) return;
        if (!other.CompareTag("Player")) return;

        used = true;
        rend.material.color = activeColor;

        manager.PressKey(keyId);
    }

    public void ResetVisual()
    {
        used = false;
        rend.material.color = idleColor;
    }
}

