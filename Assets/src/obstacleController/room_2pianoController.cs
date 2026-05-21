using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class room_2pianoController : MonoBehaviour
{
    [Header("Orden correcto (ej: 1 2 3 4)")]
    public int[] correctOrder;

    [Header("Jugador")]
    public Transform player;
    public Transform resetPosition;
    [Header("guitar controller")]
    public GameObject guitarBool;
    public GameObject trigger;
    private int currentIndex = 0;
    public bool canPlay;

    public gameManager gameManager;

    void Start()
    {
        
    }
    public void PressKey(int keyId)
    {
        Debug.Log("Presionada"+keyId+" correcta " + correctOrder[currentIndex]);
        if (keyId == correctOrder[currentIndex] &&canPlay)
        {
            currentIndex++;

            
            if (currentIndex >= correctOrder.Length)
            {
                Debug.Log("Piano completado");
                canPlay = false;
                gameManager.completed[2] = true;
                trigger.SetActive(true);
                currentIndex = 0;
            }
        }
        else if(canPlay)
        {
            Debug.Log("Orden incorrecto");
            ResetPuzzle();
        }
    }

    void ResetPuzzle()
    {
        currentIndex = 0;

        if (player != null && resetPosition != null)
        {
            player.position = resetPosition.position;
        }

        // opcional: avisar a los triggers que se reseteen
        BroadcastMessage("ResetVisual", SendMessageOptions.DontRequireReceiver);
    }
}