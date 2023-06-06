using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    [SerializeField] Rigidbody rb;

    private float horizontalInput;
    [SerializeField] float horizontalMultiplier = 2;

    [SerializeField] float jumpForce = 1200f;
    [SerializeField] LayerMask groundMask;
    private float speedMax = 75f;

    void Start()
    {
    }

    void FixedUpdate()
    {
        if (speed < speedMax)
        {
            speed += 1f;
        }
        Vector3 moveForward = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime * horizontalMultiplier;
        rb.MovePosition(rb.position + moveForward + horizontalMove);
        
    }


    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

    }

    void Jump()
    {
        //Check if the player is on the ground
        float height = GetComponent<Collider>().bounds.size.y;
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, (height/2)+0.1f, groundMask);
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce);
        }
    }
}
