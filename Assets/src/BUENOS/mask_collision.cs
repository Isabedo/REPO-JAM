using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mask_collision : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public bool puedePasar = false;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player") )
        {

            if (animator != null)
            {
                animator.SetTrigger("activa");
            }
            gameObject.SetActive(false);
            puedePasar = true;
            Debug.Log("puede pasar");

            GameManager.Instance.sala1Completada = true;
            Debug.Log("Puzzle sala 1 resuelto");
        }
    }
}
