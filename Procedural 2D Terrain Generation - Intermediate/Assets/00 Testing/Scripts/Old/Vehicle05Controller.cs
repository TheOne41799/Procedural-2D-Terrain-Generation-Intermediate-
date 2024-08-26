using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle05Controller : MonoBehaviour
{
    public WheelJoint2D frontWheel;
    public WheelJoint2D backWheel;

    public float maximumMotorTorque = 1000f;
    public float motorSpeed = 2000f; // Base motor speed value

    private JointMotor2D motorFront;
    private JointMotor2D motorBack;

    void Start()
    {
        // Initialize the motor settings
        motorFront = new JointMotor2D { maxMotorTorque = maximumMotorTorque, motorSpeed = 0 };
        motorBack = new JointMotor2D { maxMotorTorque = maximumMotorTorque, motorSpeed = 0 };

        frontWheel.useMotor = true;
        backWheel.useMotor = true;
    }

    void Update()
    {
        // Control the car with the arrow keys
        if (Input.GetKey(KeyCode.RightArrow))
        {
            motorFront.motorSpeed = -motorSpeed;
            motorBack.motorSpeed = -motorSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            motorFront.motorSpeed = motorSpeed;
            motorBack.motorSpeed = motorSpeed;
        }
        else
        {
            motorFront.motorSpeed = 0;
            motorBack.motorSpeed = 0;
        }

        // Apply the motor settings to the wheels
        frontWheel.motor = motorFront;
        backWheel.motor = motorBack;
    }
}