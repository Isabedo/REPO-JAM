using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class room_1_collision : MonoBehaviour
{
    float time;
    public GameObject ONEtoTWOtrigger;
    public GameObject maskONE;
    private new MeshRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        
        if(other.gameObject.CompareTag("room1_cube"))
        time +=Time.deltaTime;
        
        if (time > 1&& ONEtoTWOtrigger!=null)
        {

            if (other.gameObject.TryGetComponent<Rigidbody>(out var rb))
            {
                rb.useGravity = false;
            }

            other.gameObject.transform.SetLocalPositionAndRotation(new Vector3(0.066f, -0.236f, -0.502f), other.gameObject.transform.rotation);
            ONEtoTWOtrigger.SetActive(true);
            Color c = renderer.material.color;
            c.a = 1f;
            renderer.material.color = c;
            if (maskONE!=null)maskONE.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("OntrigerExit room1");
        if (other.gameObject.CompareTag("room1_cube")) time = 0;
    }
}
