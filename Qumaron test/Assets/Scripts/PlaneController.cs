using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Використовувати для керування літаком через джойстик!!!!
public class PlaneController : MonoBehaviour
{
    public Joystick joystick;
    private Rigidbody _rb;

    private float _horizontalInput;
    private float _verticalInput;

    public float forwardSpeed = 5f;
    public float horizontalSpeed = 2f;
    public float verticalSpeed = 2f;

    public float maxHorizontalRotation = 0.15f;
    public float maxVerticalRotation = 0.2f;

    public float smoothness = 4f;
    public float rotationSmoothness = 2f;


    private float forwardSpeedMultiplier = 100f;
    private float speedMultiplier = 1000f;



    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(Input.GetMouseButton(0) || Input.touches.Length != 0)
        {
            _horizontalInput = joystick.Horizontal;
            _verticalInput = joystick.Vertical;
        }
        else
        {
            _horizontalInput = Input.GetAxisRaw("Horizontal");
            _verticalInput = Input.GetAxisRaw("Vertical");
        }

        HandlePlaneRotation();

    }

    private void FixedUpdate()
    {
        HandlePlaneMovement();
    }

    private void HandlePlaneRotation()
    {
        float horizontalRotation = _horizontalInput * maxHorizontalRotation;
        float verticalRotation = _verticalInput * maxVerticalRotation;

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            new Quaternion(verticalRotation, transform.rotation.y,
            horizontalRotation, transform.rotation.w),
            Time.deltaTime * rotationSmoothness);
    }

    private void HandlePlaneMovement()
    {
        _rb.velocity = new Vector3(_rb.velocity.x, _rb.velocity.y, forwardSpeed * forwardSpeedMultiplier * Time.deltaTime);

        float xVelocity = _horizontalInput * speedMultiplier * horizontalSpeed * Time.deltaTime;
        float yVelocity = -_verticalInput * speedMultiplier * verticalSpeed * Time.deltaTime;

        _rb.velocity = Vector3.Lerp(_rb.velocity, new Vector3(xVelocity, yVelocity, _rb.velocity.z), Time.deltaTime * smoothness);
    }
}
