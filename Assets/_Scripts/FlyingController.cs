using System;
using UnityEngine;

public class FlyingController : MonoBehaviour
{
    public float speed = 5f;
    public float liftForce = 5f;
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        BoundsEnforcer();
    }

    private void BoundsEnforcer()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, - 13f, 1.5f), 
            Mathf.Clamp(transform.position.y, - 5.38f, 2.312f), 
            transform.position.z
        );
    }

    private void Movement()
    {
        float moveX = 0f;  //Input.GetAxis("Horizontal");
        float moveY = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            moveY = liftForce;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveY = -liftForce;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveX = liftForce;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            moveX = -liftForce;
        }

        rb.linearVelocity = new Vector2(moveX * speed, moveY * speed);

    }

}
