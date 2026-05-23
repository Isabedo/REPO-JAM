using UnityEngine;

public class activarBoton : MonoBehaviour
{
    [SerializeField] float distanciaInteraccion = 3f;
    [SerializeField] GameObject botonInteraccion; // botón UI "Craftear"
    Transform jugador;

    void Start()
    {
        jugador = GameObject.FindWithTag("Player").transform;
        botonInteraccion.SetActive(false);
    }

    void Update()
    {
        float distancia = Vector3.Distance(transform.position, jugador.position);
        botonInteraccion.SetActive(distancia <= distanciaInteraccion);
    }

}