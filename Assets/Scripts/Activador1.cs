using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activador1 : MonoBehaviour
{



    [Header("Objeto que se activará")]
    public GameObject elementosCasa;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            elementosCasa.SetActive(true);
            Destroy(gameObject);
        }
    }

}
