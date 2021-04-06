using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float MovementSpeed;
    [SerializeField] private float CrawlSpeed;
    [SerializeField] private float JumpForce;
    [SerializeField] private float ClimbSpeed;
    [SerializeField] private LayerMask PlatformLayer;
    
    private Animator animator;
    private Rigidbody2D rb;
    private Collider2D coll;
    private bool CanClimb = false;
    private bool Climbing = false;
    private bool Dead = false;
    private enum State { idle, running, jumping, falling, climbing, crouching, crawling, death }
    private State state = State.idle;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!PauseMenu.isPaused)
        {
            Movement();
            VelocityState();
            animator.SetInteger("State", (int)state);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Collectible")
        {
            Animator cherryAnimator = collision.gameObject.GetComponent<Animator>();
            cherryAnimator.SetTrigger("Collected");
            InGameUI.Score += 10000;
        }
        if(collision.gameObject.tag == "Ladder")
        {
            CanClimb = true;
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Dead = true;
            animator.SetTrigger("Dead");
        }
    }

    private void RestartLevel()
    {
        PlayerPrefs.SetInt("Life", InGameUI.Life - 1);
        if(PlayerPrefs.GetInt("Life") <= 0)
        {
            PlayerPrefs.SetInt("Score", InGameUI.Score);
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ladder")
        {
            CanClimb = false;
            Climbing = false;
            rb.gravityScale = 2;
            animator.speed = 1;
            state = State.falling;
        }
    }

    private void Movement()
    {
        float MoveDirection = Input.GetAxisRaw("Horizontal");
        if (!Dead)
        {
            if (Input.GetAxisRaw("Vertical") < 0 && !Climbing && (state != State.jumping || state != State.falling))
            {
                if (MoveDirection > 0)
                {
                    rb.velocity = new Vector2(CrawlSpeed, rb.velocity.y);
                    rb.transform.localScale = new Vector2(1, 1);
                }
                else if (MoveDirection < 0)
                {
                    rb.velocity = new Vector2(-CrawlSpeed, rb.velocity.y);
                    rb.transform.localScale = new Vector2(-1, 1);
                }
            }
            else
            {
                if (MoveDirection > 0)
                {
                    rb.velocity = new Vector2(MovementSpeed, rb.velocity.y);
                    rb.transform.localScale = new Vector2(1, 1);
                }
                else if (MoveDirection < 0)
                {
                    rb.velocity = new Vector2(-MovementSpeed, rb.velocity.y);
                    rb.transform.localScale = new Vector2(-1, 1);
                }
            }
            if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(PlatformLayer))
            {
                rb.velocity = new Vector2(rb.velocity.x, JumpForce);
                state = State.jumping;
            }

            if (CanClimb && Input.GetButton("Vertical"))
            {
                Climbing = true;
                state = State.climbing;
                rb.velocity = new Vector2(rb.velocity.x, ClimbSpeed * Input.GetAxisRaw("Vertical"));
            }

            if (Climbing == true)
            {
                rb.gravityScale = 0;
            }
        }
    }
    private void VelocityState()
    {
        if(Climbing)
        {
            state = State.climbing;
            if (Input.GetAxisRaw("Vertical") == 0)
            {
                animator.speed = 0;
            }
            else
            {
                animator.speed = 1;
            }
        }
        else
        {
            if(rb.velocity.x == 0 && rb.velocity.y == 0)
            {
                state = State.idle;
            } else if(rb.velocity.y > 0.5)
            {
                state = State.jumping;
            } else if(rb.velocity.y < -0.5)
            {
                state = State.falling;
            }
            else if (Mathf.Abs(rb.velocity.x) >= CrawlSpeed)
            {
                if(Input.GetAxisRaw("Vertical") < 0)
                {
                    state = State.crawling;
                }
                else
                {
                    state = State.running;
                }
            }
            else
            {
                if (Input.GetAxisRaw("Vertical") < 0)
                {
                    state = State.crouching;
                }
                else
                {
                    state = State.idle;
                }
            }
        }
    }
}
