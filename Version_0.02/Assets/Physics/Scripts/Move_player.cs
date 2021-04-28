using UnityEngine;

public class Move_player : MonoBehaviour
{
    public float moveSpeed;

    private bool isGrounded;

    public float Jumpforce;

    public Rigidbody2D rb;

    private bool isJumping;

    public Transform GroundCheckLeft;
    public Transform GroundCheckRight;

    private Vector3 velocity = Vector3.zero;

    public Transform WallCheckLeft1;
    public Transform WallCheckLeft2;
    public Transform WallCheckRight1;
    public Transform WallCheckRight2;

    private bool WallcheckLeft;
    private bool WallcheckRight;

    void FixedUpdate()
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
    }

    void MovementPlayer(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);

        if(isJumping && isGrounded)
        {
            rb.AddForce(new Vector2(0f, Jumpforce));
            isJumping = false;
        }
        if(isJumping && WallcheckLeft)
        {
            rb.AddForce(new Vector2(-2 * _horizontalMovement, Jumpforce));
            isJumping = false;
        }
        if (isJumping && WallcheckRight)
        {
            rb.AddForce(new Vector2(-2 * _horizontalMovement, Jumpforce));
            isJumping = false;
        }
    }
}
