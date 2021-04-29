using UnityEngine;

public class Move_player : MonoBehaviour
{
    public float moveSpeed;

    private bool isGrounded;

    public SpriteRenderer spriteRenderer;

    public float JumpForce;
    public float WallJumpForce;

    public Rigidbody2D rb;

    private bool isJumping;

    public Transform GroundCheckLeft;
    public Transform GroundCheckRight;

    public Animator animator;

    private Vector3 velocity = Vector3.zero;

    public Transform WallCheckLeft1;
    public Transform WallCheckLeft2;
    public Transform WallCheckRight1;
    public Transform WallCheckRight2;

    private bool WallcheckLeft;
    private bool WallcheckRight;

    void Update()
    {
        isGrounded = Physics2D.OverlapArea(GroundCheckLeft.position, GroundCheckRight.position);
        WallcheckLeft = Physics2D.OverlapArea(WallCheckLeft1.position, WallCheckLeft2.position);
        WallcheckRight = Physics2D.OverlapArea(WallCheckRight1.position, WallCheckRight2.position);

        float horizontalMovemement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

        if(Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }

        MovementPlayer(horizontalMovemement);

        Flip(rb.velocity.x);

        float characterVelocity = Mathf.Abs(rb.velocity.x);

        animator.SetFloat("Speed", characterVelocity);
    }

    void MovementPlayer(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);

        if(isJumping && isGrounded)
        {
            rb.AddForce(new Vector2(0f, JumpForce));
            isJumping = false;
        }
        if(isJumping && WallcheckLeft)
        {
            rb.AddForce(new Vector2(750, WallJumpForce));
            isJumping = false;
        }
        if (isJumping && WallcheckRight)
        {
            rb.AddForce(new Vector2(-750, WallJumpForce));
            isJumping = false;
        }
    }

    void Flip(float _velocity)
    {
        if (_velocity > 0.1f)
        {
            spriteRenderer.flipX = false;
        }
        else if (_velocity < -0.1f) 
        {
            spriteRenderer.flipX = true;
        }
    }
}
