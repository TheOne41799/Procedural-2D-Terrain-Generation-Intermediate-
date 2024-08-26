using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    // SCRIPT DID NOT WORK




    /*private WheelJoint2D[] wheelJoints;
    JointMotor2D frontWheelMotor;
    JointMotor2D backWheelMotor;

    //[SerializeField] private float deceleration = -400f;
    [SerializeField] private float gravity = 9.8f;
    [SerializeField] private float angleCar = 0;
    [SerializeField] private float acceleration = 500f;
    [SerializeField] private float maxSpeed = -800f;
    [SerializeField] private float maxBackSpeed = 600f;
    //[SerializeField] private float brakeForce = 1000f;
    [SerializeField] private float wheelSize;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private Transform backWheel;
        
    private bool isCarGrounded = false;


    private void Start()
    {
        wheelJoints = gameObject.GetComponents<WheelJoint2D>();
        frontWheelMotor = wheelJoints[0].motor;
        backWheelMotor = wheelJoints[1].motor;
    }


    private void FixedUpdate()
    {
        isCarGrounded = Physics2D.OverlapCircle(backWheel.transform.position, wheelSize, groundLayerMask);

        Debug.Log(isCarGrounded);

        angleCar = transform.localEulerAngles.z;
        if (angleCar > 180) angleCar -= 360;



        if (isCarGrounded)
        {
            backWheelMotor.motorSpeed = Mathf.Clamp(
            backWheelMotor.motorSpeed - (acceleration - gravity * Mathf.PI * (angleCar/180) * 80)
                                                        * Time.deltaTime * Input.GetAxis("Horizontal"),
            maxSpeed,
            maxBackSpeed
            );

            Debug.Log("asdasd");
        }
        

        *//*if (isCarGrounded)
        {
            if(Input.GetAxis("Horizontal") > 0)
            {
                backWheelMotor.motorSpeed = Mathf.Clamp(
                    backWheelMotor.motorSpeed - (acceleration - gravity * Mathf.PI * (angleCar/180) * 80 * Time.fixedDeltaTime),
                    maxSpeed,
                    maxBackSpeed
                    );

                Debug.Log("asdasd");
            }
        }

        if (Input.GetAxis("Horizontal") < 0 && backWheelMotor.motorSpeed < 0)
        {
            backWheelMotor.motorSpeed = Mathf.Clamp(
                    backWheelMotor.motorSpeed - (deceleration - gravity * Mathf.PI * (angleCar / 180) * 80 * Time.fixedDeltaTime),
                    maxSpeed,
                    maxBackSpeed
                    );
        }
        else if (Input.GetAxis("Horizontal") < 0 && backWheelMotor.motorSpeed > 0)
        {
            backWheelMotor.motorSpeed = Mathf.Clamp(
                    backWheelMotor.motorSpeed - (-deceleration - gravity * Mathf.PI * (angleCar / 180) * 80 * Time.fixedDeltaTime),
                    maxSpeed,
                    maxBackSpeed
                    );
        }*//*


        frontWheelMotor = backWheelMotor;

        wheelJoints[0].motor = backWheelMotor;
        wheelJoints[1].motor = frontWheelMotor;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(backWheel.transform.position, wheelSize);
    }*/
}









