using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogAI : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D coll;
    private Animator anim;

    private bool FaceLeft = true;
    private enum State { idle, jumping, falling};
    private State state = State.idle;
    [SerializeField] private float LeftCap;
    [SerializeField] private float RightCap;
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float JumpPower;
    [SerializeField] private LayerMask ground;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
        Invoke("Jump", Random.Range(1, 3));
    }

    private void Update()
    {
        if(rb.velocity.y > 0)
        {
            state = State.jumping;
        } else if(rb.velocity.y < 0)
        {
            state = State.falling;
        }
        else
        {
            state = State.idle;
        }
        anim.SetInteger("State", (int)state);
    }

    private void Jump()
    {
        if (FaceLeft) 
        {
            if (transform.position.x > LeftCap)
            {
                rb.transform.localScale = new Vector2(1, 1);

                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(-MoveSpeed, JumpPower);
                }
            }
            else
            {
                FaceLeft = false;
            }     
        }
        else
        {
            if (transform.position.x < RightCap)
            {
                rb.transform.localScale = new Vector2(-1, 1);

                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(MoveSpeed, JumpPower);
                }
            }
            else
            {
                FaceLeft = true;
            }
        }


        Invoke("Jump", Random.Range(1, 3));
    }
    
}
