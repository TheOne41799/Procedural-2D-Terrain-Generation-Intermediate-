using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VehicleControlller : MonoBehaviour
{
    /*[Header("Car Settings")]
    public float acceleration = 1000f;
    public float maxFrontSpeed = 20f;
    public float maxBackSpeed = 10f;
    public float brakeForce = 3000f;
    public LayerMask groundLayerMask;
    public float maxMotorTorque = 1500f;  // New variable for motor torque

    private Rigidbody2D rb;
    private WheelJoint2D[] wheelJoints;
    private JointMotor2D motor;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        wheelJoints = GetComponents<WheelJoint2D>();

        if (wheelJoints.Length < 2)
        {
            Debug.LogError("CarController: Car must have two WheelJoint2D components.");
        }
    }

    private void Update()
    {
        HandleMovement();
        AdjustSpeedBasedOnSlope();
    }

    private void HandleMovement()
    {
        float horizontalInput = InputManager.Instance.HorizontalInput;

        foreach (var wheelJoint in wheelJoints)
        {
            motor = wheelJoint.motor;
            motor.motorSpeed = Mathf.Lerp(motor.motorSpeed, -horizontalInput * acceleration, Time.deltaTime * 10f);
            motor.maxMotorTorque = maxMotorTorque;  // Set the motor torque
            wheelJoint.motor = motor;

            // Limit max speed
            float currentSpeed = Mathf.Abs(rb.velocity.x);
            if (currentSpeed > maxFrontSpeed && horizontalInput > 0)
            {
                rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxFrontSpeed, rb.velocity.y);
            }
            else if (currentSpeed > maxBackSpeed && horizontalInput < 0)
            {
                rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxBackSpeed, rb.velocity.y);
            }

            // Apply braking
            if (InputManager.Instance.IsBraking)
            {
                motor.motorSpeed = 0f;
                wheelJoint.useMotor = false;  // Disable motor while braking to avoid unexpected behavior
                rb.AddForce(-rb.velocity * brakeForce * Time.deltaTime);
            }
            else
            {
                wheelJoint.useMotor = true;  // Re-enable motor when not braking
            }
        }
    }

    private void AdjustSpeedBasedOnSlope()
    {
        // Raycast to detect ground slope
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, groundLayerMask);

        if (hit.collider != null)
        {
            float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
            float slopeEffect = Mathf.Cos(slopeAngle * Mathf.Deg2Rad);

            // Reduce speed on uphill, increase on downhill
            rb.velocity = new Vector2(rb.velocity.x * slopeEffect, rb.velocity.y);
        }
    }*/

    /*
        [SerializeField] private Rigidbody2D frontTireRB;
        [SerializeField] private Rigidbody2D backTireRB;
        [SerializeField] private float vehicleSpeed = 150f;

        [SerializeField] private Rigidbody2D vehicleRB;
        [SerializeField] private float vehicleRotationSpeed = 300f;

        private float moveInput;


        private void Update()
        {
            moveInput = Input.GetAxisRaw("Horizontal");
        }


        private void FixedUpdate()
        {
            frontTireRB.AddTorque(-moveInput * vehicleSpeed * Time.fixedDeltaTime);
            backTireRB.AddTorque(-moveInput * vehicleSpeed * Time.fixedDeltaTime);
            vehicleRB.AddTorque(moveInput * vehicleRotationSpeed * Time.fixedDeltaTime);
        }*/




    [Header("Car Settings")]
    [SerializeField] private Rigidbody2D frontTireRB;
    [SerializeField] private Rigidbody2D backTireRB;
    [SerializeField] private float acceleration = 150f;
    [SerializeField] private float maxSpeed = 20f;
    [SerializeField] private float brakeForce = 300f;
    [SerializeField] private float vehicleRotationSpeed = 300f;
    [SerializeField] private LayerMask groundLayerMask;

    private Rigidbody2D vehicleRB;

    private void Awake()
    {
        vehicleRB = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleBraking();
        AdjustSpeedBasedOnSlope();
    }

    private void HandleMovement()
    {
        float moveInput = InputManager.Instance.HorizontalInput;

        // Apply torque to tires based on input
        float torque = -moveInput * acceleration * Time.fixedDeltaTime;
        frontTireRB.AddTorque(torque);
        backTireRB.AddTorque(torque);

        // Rotate vehicle based on input
        float rotation = -moveInput * vehicleRotationSpeed * Time.fixedDeltaTime;
        vehicleRB.AddTorque(rotation);

        // Limit speed
        Vector2 velocity = vehicleRB.velocity;
        if (Mathf.Abs(velocity.x) > maxSpeed)
        {
            vehicleRB.velocity = new Vector2(Mathf.Sign(velocity.x) * maxSpeed, velocity.y);
        }
    }

    private void HandleBraking()
    {
        if (InputManager.Instance.IsBraking)
        {
            // Apply brake force
            Vector2 brakeForceVector = -vehicleRB.velocity.normalized * brakeForce * Time.fixedDeltaTime;
            vehicleRB.AddForce(brakeForceVector);

            // Optionally, reduce the torque applied to tires while braking
            frontTireRB.AddTorque(0);
            backTireRB.AddTorque(0);
        }
    }

    private void AdjustSpeedBasedOnSlope()
    {
        // Raycast to detect ground slope
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, groundLayerMask);

        if (hit.collider != null)
        {
            float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
            float slopeEffect = Mathf.Cos(slopeAngle * Mathf.Deg2Rad);

            // Reduce speed on uphill, increase on downhill
            vehicleRB.velocity = new Vector2(vehicleRB.velocity.x * slopeEffect, vehicleRB.velocity.y);
        }
    }
}
