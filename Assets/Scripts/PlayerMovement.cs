using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;

    [Header("Coyote Time")]
    [SerializeField] private float coyoteTime; //How much time the player can hang in the air before jumping
    private float coyoteCounter; //How much time passed since the player ran off the edge


    [Header("Multiple Jumps")]
    [SerializeField] private int extraJumps;
    private int jumpCounter;

    private bool isWallSliding;

    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 2f;
    private Vector2 wallJumpingPower = new Vector2(8f, 16f);


    [SerializeField] private float size;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    [Header("Sounds")]
    [SerializeField] private AudioClip jumpSound;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    // private float wallJumpCooldown;
    private float horizontalInput;

    private void Awake()
    {
        //Grabs references for rigidbody and animator from game object.
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (DialogueManager.GetInstance().isDialoguePlaying)
            return;

        horizontalInput = Input.GetAxis("Horizontal");

        //Flip player when facing left/right.
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector2(1 * size, 1 * size);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector2(-1 * size, 1 * size);

        //sets animation parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        //Jump
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        //Adjustable jump height
        if (Input.GetKeyUp(KeyCode.Space) && body.linearVelocity.y > 0)
            body.linearVelocity = new Vector2(body.linearVelocity.x, body.linearVelocity.y / 2);
        else
        {
            body.gravityScale = 2;
            body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);
            if (isGrounded())
            {
                coyoteCounter = coyoteTime; //Reset coyote counter when on the ground
                jumpCounter = extraJumps; //Reset jump counter to extra jump value
                anim.SetBool("doublejump", false);
            }
            else
                coyoteCounter -= Time.deltaTime; //Start decreasing coyote counter when not on the ground
        }

        WallSlide();
        WallJump();

        if (!isWallJumping)
        {
            body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);
        }
    }

    private void FixedUpdate()
    {
        if (!isWallJumping)
            body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);
    }

    private void Jump()
    {
        if (DialogueManager.GetInstance().isDialoguePlaying)
            return;

        if (coyoteCounter <= 0 && jumpCounter <= 0) return;
        //If coyote counter is 0 or less and not on the wall and don't have any extra jumps don't do anything

        SoundManager.instance.PlaySound(jumpSound);
        if (isGrounded())
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
        }
        else
        {
            //If not on the ground and coyote counter bigger than 0 do a normal jump
            if (coyoteCounter > 0)
                body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
            else
            {
                if (jumpCounter > 0) //If we have extra jumps then jump and decrease the jump counter
                {
                    anim.SetBool("doublejump", true);
                    body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
                    jumpCounter--;
                }

            }
        }
    }

    public void stopPlayer()
    {
        body.linearVelocity = new Vector2(0, 0);
        horizontalInput = 0;
        anim.SetBool("run", false);
    }


    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    private void WallSlide()
    {
        if (onWall() && !isGrounded() && horizontalInput != 0)
        {
            isWallSliding = true;
            body.gravityScale = 15;
        }
        else
        {
            isWallSliding = false;
            body.gravityScale = 2;
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && wallJumpingCounter > 0)
        {
            isWallJumping = true;
            body.linearVelocity = new Vector2(wallJumpingPower.x * wallJumpingDirection, wallJumpingPower.y);
            wallJumpingCounter = 0;
            if (transform.localScale.x != wallJumpingDirection)
            {
                Vector3 localScale = transform.localScale;
                localScale.x = -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }

    }
    private void StopWallJumping()
    {
        isWallJumping = false;
    }
}