using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossumAI : MonoBehaviour
{
    private bool FaceLeft = true;
    [SerializeField] private float LeftCap;
    [SerializeField] private float RightCap;
    [SerializeField] private float MoveSpeed;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if (FaceLeft)
        {
            if(transform.position.x > LeftCap)
            {
                rb.velocity = new Vector2(-MoveSpeed, 0);
            }
            else
            {
                FaceLeft = false;
                transform.localScale = new Vector2(-1, 1);
            }
        }
        else
        {
            if(transform.position.x < RightCap)
            {
                rb.velocity = new Vector2(MoveSpeed, 0);
            }
            else
            {
                FaceLeft = true;
                transform.localScale = new Vector2(1, 1);
            }
        }
    }
}
