using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mask_one_collider : MonoBehaviour
{
    public gameManager gameManager;
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
            gameManager.completed[1] = true;
            Destroy(gameObject);
        }
    }
}
