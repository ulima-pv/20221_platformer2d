
using System;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float accel;
    public float deccel;
    public float speedExp;

    [Header("Jump")]
    public float raycastDistance;
    public float jumpForce;
    public float fallMultiplier;

    private Rigidbody2D mRigidBody;
    private float mMovement;

    private void Start()
    {
        mRigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        mMovement = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && !IsOnAir())
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        Move();

        if (mRigidBody.velocity.y < 0)
        {
            // Esta cayendo
            mRigidBody.velocity += (fallMultiplier - 1) * 
                Time.fixedDeltaTime * Physics2D.gravity;
        }
    }

    private void Move()
    {
        float targetSpeed = mMovement * moveSpeed;
        float speedDif = targetSpeed - mRigidBody.velocity.x;
        float accelRate = Mathf.Abs(targetSpeed) > 0.01f ? accel : deccel;
        float movement = Mathf.Pow(
            accelRate * Mathf.Abs(speedDif),
            speedExp
        ) * Mathf.Sign(speedDif);

        mRigidBody.AddForce(movement * Vector2.right);
    }

    private void Jump()
    {
        mRigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public bool IsOnAir()
    {
        Transform rayCastOrigin = transform.Find("RaycastPoint");
        RaycastHit2D hit = Physics2D.Raycast(
            rayCastOrigin.position,
            Vector2.down,
            raycastDistance
        );

        /*Color rayColor;
        if (hit)
        {
            rayColor = Color.red;
        }else
        {
            rayColor = Color.blue;
        }
        Debug.DrawRay(rayCastOrigin.position, Vector2.down * raycastDistance, rayColor);*/

        return !hit;
        //return hit == null ? true : false;
        
    }
}
