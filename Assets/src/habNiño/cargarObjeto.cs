using UnityEngine;

public class cargarObjeto : MonoBehaviour
{
    [SerializeField] Transform puntoOrigen;       
    [SerializeField] Transform jugador;        
    [SerializeField] string tagHabitacion;      
    [SerializeField] Vector3 offsetEnMano = new Vector3(0.5f, 0f, 1f);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    bool estaCargado = false;
    Vector3 posicionOriginal;
    
    void Start()
    {
        posicionOriginal = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
         if (estaCargado)
        {
            // El objeto sigue al jugador
            transform.position = jugador.position + jugador.TransformDirection(offsetEnMano);
        }
    }

    public void AlternarCarga()
    {
        estaCargado = !estaCargado;

        if (!estaCargado)
        {
            // Si lo sueltan manualmente vuelve al origen
            transform.position = posicionOriginal;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("habNiño") && estaCargado)
        {
            estaCargado = false;
            transform.position = posicionOriginal;
            Debug.Log("El objeto salió de la habitación, volvió al origen");
        }
    }
}
