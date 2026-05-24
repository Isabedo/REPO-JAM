using UnityEngine;

public class puzzles : MonoBehaviour
{
    [SerializeField] string salaQueCompleta; // "sala1", "sala2", "sala3"

    public void PuzzleResuelto()
    {
        switch (salaQueCompleta)
        {
            case "sala1": GameManager.Instance.sala1Completada = true; break;
            case "sala2": GameManager.Instance.sala2Completada = true; break;
            case "sala3": GameManager.Instance.sala3Completada = true; break;
        }
        Debug.Log(salaQueCompleta + " completada");
    }
}