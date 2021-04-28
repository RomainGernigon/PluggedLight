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

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapArea(GroundCheckLeft.position, GroundCheckRight.position);
        
        float horizontalMovemement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
        }

        MovementPlayer(horizontalMovemement);
    }

    void MovementPlayer(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);

        if(isJumping)
        {
            rb.AddForce(new Vector2(0f, Jumpforce));
            isJumping = false;
        }
    }
}
