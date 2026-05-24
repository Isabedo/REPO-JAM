using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivaDesactiva : MonoBehaviour
{



    public GameObject objeto1;

    public GameObject objeto2;

    private bool yaSeActivo = false; // Evita que se ejecute más de una vez

    void Update()
    {
        if (yaSeActivo) return;

        // Si objeto1 es null (destruido) o está desactivado
        if (objeto1 == null || !objeto1.activeSelf)
        {
            if (objeto2 != null)
                objeto2.SetActive(true);

            yaSeActivo = true; // Marca que ya se ejecutó
        }
    }
}
