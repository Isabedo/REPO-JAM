using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollicionColectible : MonoBehaviour
{
    public ControlAnimacionPuerta door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            door.AdvanceState();
            Destroy(gameObject);
        }
    }
}

