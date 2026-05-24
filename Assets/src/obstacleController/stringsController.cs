using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stringsController : MonoBehaviour
{
    public GameObject guitarra;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            guitarra.GetComponent<room_2guitarrController>().guitarStrings--;
            Destroy(gameObject);
        }
    }
}
