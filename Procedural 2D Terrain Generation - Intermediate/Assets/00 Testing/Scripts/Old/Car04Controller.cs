using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car04Controller : MonoBehaviour
{
    public Transform Car;
    public int power;
    public int reverse;
    public Vector2 force;

    private Vector2 trigFunction;
    public float maxSpeed = 200f;

    public Rigidbody2D carRB;


    private void Update()
    {
        trigFunction = Car.TransformDirection(Vector2.right);

        force.Set(trigFunction.x, trigFunction.y);

        if (Input.GetKey(KeyCode.W))
        {
            carRB.AddForce(force * power);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            carRB.AddForce(-force * reverse);
        }
    }


    private void FixedUpdate()
    {
        if (carRB.velocity.magnitude > maxSpeed)
        {
            carRB.velocity = carRB.velocity.normalized * maxSpeed;
        }
    }
}
