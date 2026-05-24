using UnityEngine;

public class AnimacionesPuerta : MonoBehaviour
{

    private mask_collision puedePasarScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") )
        {
            puedePasarScript = other.GetComponent<mask_collision>();
            if (puedePasarScript != null)
            {
                puedePasarScript.puedePasar = false;
            }
            Debug.Log("no puede pasar");
        }
    }
}
