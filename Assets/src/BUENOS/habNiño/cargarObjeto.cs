using UnityEngine;
using UnityEngine.UI;

public class cargarObjeto : MonoBehaviour
{   
    [SerializeField] Button boton;
    public bool estaCargado = false;
    Vector3 posicionOriginal;
    
    void Start()
    {
        posicionOriginal = transform.position;
    }

    public void AlternarCarga()
    {
        estaCargado = !estaCargado;

        if (!estaCargado)
        {
            transform.position = posicionOriginal;
            gameObject.SetActive(false);
            estaCargado = true;

        } 
    }
     
}


