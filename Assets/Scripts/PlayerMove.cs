using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float runSpeed = 2;
    public float jumpSpeed = 3;
    private Rigidbody2D rigidbody2D;

    public bool betterJump = false;
    public float fallMultiplier = 0.5f;
    public float lowJumpMultiplier = 1f;
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (Input.GetKey("d")  ||  Input.GetKey("right"))
        {
            spriteRenderer.flipX = false;
            animator.SetBool("Run", true);
            rigidbody2D.velocity = new Vector2(runSpeed, rigidbody2D.velocity.y);
        }
        else if (Input.GetKey("a") || Input.GetKey("left"))
        {
            spriteRenderer.flipX = true;
            animator.SetBool("Run", true);
            rigidbody2D.velocity = new Vector2(-runSpeed, rigidbody2D.velocity.y);
        }
        else
        {
            animator.SetBool("Run", false);
            rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
        }

        if (Input.GetKey("space")  &&  CheckGround.isGrounded)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpSpeed);
        }

        if (!CheckGround.isGrounded)
        {
            animator.SetBool("Jump", true);
            animator.SetBool("Run", false);
        }
        if (CheckGround.isGrounded)
        {
            animator.SetBool("Jump", false);
        }

        if (betterJump)
        {
            if (rigidbody2D.velocity.y < 0)
            {
                rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
            }
            if (rigidbody2D.velocity.y > 0  &&  !Input.GetKey("space"))
            {
                rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * lowJumpMultiplier * Time.deltaTime;
            }
        }
    }
}
