using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzle_1 : MonoBehaviour
{
 
    [SerializeField] cargarObjeto cargarObjetoScript;
    private new MeshRenderer renderer;
    [SerializeField] Material nuevoMaterial;
    public bool pinturaLista = false;


    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
    }


    private void OnTriggerStay(Collider other)
    {
       if (other.gameObject.CompareTag("Player") && cargarObjetoScript.estaCargado)
        {
            renderer.material = nuevoMaterial; 
            pinturaLista = true;


            GameManager.Instance.sala2Completada = true;
            Debug.Log("Puzzle sala 2 resuelto");
        }
    }
    
}
