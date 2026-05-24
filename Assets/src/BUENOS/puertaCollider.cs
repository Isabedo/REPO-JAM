using UnityEngine;

public class puertaCollider : MonoBehaviour
{
    [SerializeField] Transform puntoDestino;
    [SerializeField] string salaRequerida; // "sala1", "sala2", "sala3"
    [SerializeField] Animator animadorPuerta;
    bool estaPasando = false;
    [SerializeField] float tiempoEsperaAnimacion = 1f;


    Transform jugadorGuardado;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !estaPasando)
        {
            if (PuedePasar())
            {
                estaPasando = true;
                jugadorGuardado = other.transform;

                // Activa la animación
                if (animadorPuerta != null)
                    animadorPuerta.SetTrigger("activa");

                // Espera que termine la animación y luego teletransporta
                Invoke(nameof(Teletransportar), tiempoEsperaAnimacion);

                Debug.Log("Abriendo puerta " + salaRequerida);
            }
            else
            {
                Debug.Log("no puede pasar, falta completar " + salaRequerida);
            }
        }
    }

    void Teletransportar()
    {
        if (jugadorGuardado != null)
            jugadorGuardado.position = puntoDestino.position;

        Invoke(nameof(ResetearPaso), 0.5f);
    }

    bool PuedePasar()
    {
        switch (salaRequerida)
        {
            case "sala1": return GameManager.Instance.sala1Completada;
            case "sala2": return GameManager.Instance.sala2Completada;
            case "sala3": return GameManager.Instance.sala3Completada;
            default: return false;
        }
    }

    void ResetearPaso() => estaPasando = false;
}