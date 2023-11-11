using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpSpeed;
    private Vector2 playerInput = Vector2.zero;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    private Collider2D coll;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }

    /* Called whenever player enters move input */
    private void OnMove(InputValue value) {
        // Get player input vector
        playerInput = value.Get<Vector2>();
    }

    /* Called whenever player presses the jump button */
    void OnJump(InputValue value) {
        if (coll.IsTouchingLayers(LayerMask.GetMask("Ground"))) {
            if (value.isPressed) {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            }
        }
    }

    /* Called every frame */
    private void Update() {
        // Run
        rb.velocity = new Vector2(playerInput.x * runSpeed, rb.velocity.y);
        FlipSprite();
        // Player is running if moving on x-axis
        anim.SetBool("isRunning", playerInput.x != 0);
        // Player is jumping if moving on y-axis and not touching ground
        anim.SetBool("isJumping", rb.velocity.y != 0 && !coll.IsTouchingLayers(LayerMask.GetMask("Ground")));
    }

    /* Flips the player sprite based on the player input velocity */
    private void FlipSprite() {
        // Running to the left
        if (playerInput.x < 0) {
            sprite.flipX = false;
        }
        // Running to the right
        else if (playerInput.x > 0) {
            sprite.flipX = true;
        }
        // Leave sprite as-is if velocity is 0
    }
}