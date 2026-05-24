using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // accesible desde cualquier script

    // Estado de progresión
    public bool sala1Completada = false;
    public bool sala2Completada = false;
    public bool sala3Completada = false;

    void Awake()
    {
        // Solo existe un GameManager en toda la escena
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
}
