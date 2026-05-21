using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CambioEscena : MonoBehaviour
{
    public Transform puntoDestino; // Lugar donde aparecerá el jugador

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Colisiona");

            // Si el player NO usa Rigidbody
            other.transform.position = puntoDestino.position;
        }
    }
}
