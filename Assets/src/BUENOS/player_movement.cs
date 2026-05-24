using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private Rigidbody _rb;
    private Vector3 _input;
    private Joystick _joystickMovement;
    [SerializeField] private float _rotationSpeed = 360f;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _joystickMovement = FindObjectOfType<Joystick>();
    }
    void Update()
    {
        GatherInput();
        Look();
    }

    void FixedUpdate()
    {
        Move();
    }

    void GatherInput() // Obtiene la entrada del joystick para mover al jugador, si no hay joystick no hace nada
    {
        if (_joystickMovement == null) return;
        _input = new Vector3(_joystickMovement.Horizontal, 0, _joystickMovement.Vertical);
    }

    void Look() // Hace que el jugador mire en la dirección del movimiento, si no hay input no hace nada
    {
        if (_input != Vector3.zero)
        {
            var relative = (transform.position + _input) - transform.position;
            var rotation = Quaternion.LookRotation(relative, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, _rotationSpeed * Time.deltaTime);
        }
    }

    void Move() // Mueve al jugador en la dirección del input, si no hay input no hace nada
    {
        if (_rb == null) return;
        _rb.MovePosition(transform.position + (transform.forward * _input.magnitude) * _speed * Time.fixedDeltaTime);
    }


}
