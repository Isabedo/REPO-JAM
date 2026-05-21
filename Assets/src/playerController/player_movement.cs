using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class player_movement : MonoBehaviour
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
        input_y = Input.GetAxis("Vertical");
        input_x = Input.GetAxis("Horizontal");
        Vector3 vector3 = new Vector3(input_x, 0, input_y);
        gameObject.transform.Translate(vector3 * speed * Time.deltaTime);


    }
}
