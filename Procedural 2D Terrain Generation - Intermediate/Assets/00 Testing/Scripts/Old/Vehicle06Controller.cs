using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle06Controller : MonoBehaviour
{
    public WheelJoint2D frontWheel;
    public WheelJoint2D backWheel;

    public float maxMotorTorque = 1000f; // Maximum torque applied to the wheel
    public float motorSpeed = 2000f; // Speed of wheel rotation when accelerating
    public float idleMotorSpeed = 0f; // Base idle speed on flat surface
    public float slopeMotorSpeedUp = 1000f; // Motor speed when climbing up a slope
    public float slopeMotorSpeedDown = 1000f; // Motor speed when going down a slope
    public float smoothTime = 0.5f; // Time in seconds to smooth the speed transition
    public float maxSlopeAngle = 15f; // Maximum angle of the slope to adjust speed
    public float speedThreshold = 0.1f; // Threshold below which speed is considered zero
    public LayerMask groundLayer; // Layer mask for detecting ground

    private float currentMotorSpeed = 0f; // Current speed of the wheels
    private float velocity = 0f; // Used by SmoothDamp for smooth speed transition
    private float slopeMotorSpeed = 0f; // Motor speed depending on slope direction
    private bool isOnSlope = false; // To store whether the car is on a slope

    private bool isMovementKeyPressed = false;

    void Start()
    {
        InitializeWheel(frontWheel);
        InitializeWheel(backWheel);
    }

    void InitializeWheel(WheelJoint2D wheel)
    {
        JointMotor2D motor = wheel.motor;
        motor.maxMotorTorque = maxMotorTorque;
        motor.motorSpeed = idleMotorSpeed; // Initialize with idleMotorSpeed
        wheel.motor = motor;
        wheel.useMotor = true; // Ensure motor is enabled
    }

    void Update()
    {
        CheckIfOnSlope();

        // Determine target speed
        float targetSpeed = idleMotorSpeed;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            targetSpeed = -motorSpeed; // Move forward
            isMovementKeyPressed = true;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            targetSpeed = motorSpeed; // Move backward
            isMovementKeyPressed = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            isMovementKeyPressed = false;
        }

        // Adjust idleMotorSpeed based on slope
        if (isOnSlope && !isMovementKeyPressed)
        {
            targetSpeed = slopeMotorSpeed; // Use slopeMotorSpeed on slope
        }
        else
        {
            idleMotorSpeed = 0f; // Reset to 0 when not on a slope
        }

        // Smoothly transition the current motor speed to the target speed
        currentMotorSpeed = Mathf.SmoothDamp(currentMotorSpeed, targetSpeed, ref velocity, smoothTime);

        // Set currentMotorSpeed to 0 if it's below the threshold
        if (Mathf.Abs(currentMotorSpeed) < speedThreshold)
        {
            currentMotorSpeed = 0f;
        }

        // Apply the smoothed motor speed to the wheels
        SetMotorSpeed(currentMotorSpeed);
    }

    void CheckIfOnSlope()
    {
        // Cast a ray from the center of the car slightly downward to detect ground
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, groundLayer);

        if (hit.collider != null)
        {
            float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
            isOnSlope = slopeAngle > maxSlopeAngle;

            // Determine slope direction and adjust motor speed
            if (isOnSlope)
            {
                float slopeDirection = Mathf.Sign(Vector2.Dot(hit.normal, Vector2.right));
                if (slopeDirection > 0) // Climbing up
                {
                    slopeMotorSpeed = -slopeMotorSpeedUp; // Use speed for climbing up
                }
                else // Going down
                {
                    slopeMotorSpeed = slopeMotorSpeedDown; // Use negative speed for going down
                }
            }
        }
        else
        {
            isOnSlope = false;
        }

        // Debug information
        Debug.Log("Slope Angle: " + (isOnSlope ? "On slope" : "Not on slope"));
    }

    void SetMotorSpeed(float speed)
    {
        JointMotor2D motorFront = frontWheel.motor;
        motorFront.motorSpeed = speed;
        frontWheel.motor = motorFront;

        JointMotor2D motorBack = backWheel.motor;
        motorBack.motorSpeed = speed;
        backWheel.motor = motorBack;
    }


}
