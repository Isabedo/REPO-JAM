using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacle_movement : MonoBehaviour
{
    float input_y;
    float input_x;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {

        if (Input.GetKey(KeyCode.Mouse0) && other.gameObject.CompareTag("Player"))
        {

            Debug.Log("FUNCIONA KEYCODE.E");
            input_y = Input.GetAxis("Vertical");
            input_x = Input.GetAxis("Horizontal");
            Vector3 vector3 = new Vector3(input_x, 0, input_y);
            gameObject.transform.Translate(vector3 * speed * Time.deltaTime);

        }
    }
    
   
    
}
