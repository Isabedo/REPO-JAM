using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mask_one_collider : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);

            // Avisa al GameManager que sala1 está completada
            GameManager.Instance.sala1Completada = true;
            Debug.Log("Puzzle sala 1 resuelto");
        }
    }
}
