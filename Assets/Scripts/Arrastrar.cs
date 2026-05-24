using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrastrar : MonoBehaviour
{

    public float grabDistance = 3f;
    public Transform holdPoint;
    public float moveForce = 500f;

    private Rigidbody grabbedRb;
    private Camera cam;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryGrab();
        }

        if (Input.GetMouseButtonUp(0))
        {
            Release();
        }
    }

    void FixedUpdate()
    {
        if (grabbedRb != null)
        {
            MoveObject();
        }
    }

    void TryGrab()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, grabDistance))
        {
            if (hit.collider.CompareTag("grabbable"))
            {
                grabbedRb = hit.collider.GetComponent<Rigidbody>();
                grabbedRb.useGravity = false;
                grabbedRb.linearDamping = 10;
            }
        }
    }

    void MoveObject()
    {
        Vector3 direction = holdPoint.position - grabbedRb.position;
        grabbedRb.linearVelocity = direction * Time.fixedDeltaTime * moveForce;
    }

    void Release()
    {
        if (grabbedRb != null)
        {
            grabbedRb.useGravity = true;
            grabbedRb.linearDamping = 0;
            grabbedRb = null;
        }
    }
}
