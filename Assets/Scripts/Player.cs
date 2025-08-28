using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float jumpForce = 15f;
    private Rigidbody2D rb;
    private bool isGround;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    private Animator animator;
    [SerializeField] private BoxCollider2D normalCollider;
    [SerializeField] private CapsuleCollider2D duckCollider;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        normalCollider.enabled = true;
        duckCollider.enabled = false;
    }

    void Update()
    {
        isGround = CheckIfGrounded();
        HandleJump();
        HandleDuck();
        HandleSoundEffect();
    }
    private bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }    
    private void HandleDuck()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            normalCollider.enabled = false;
            duckCollider.enabled = true;
            animator.SetBool("isDuck", true);
        }    
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            normalCollider.enabled = true;
            duckCollider.enabled = false;
            animator.SetBool("isDuck", false);
        }    
    }    
    private void HandleSoundEffect()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            AudioManager.instance.PLayJumpClip();
        }
        if(isGround && !AudioManager.instance.HasPlayEffectSound())
        {
            AudioManager.instance.PLayTapClip();
            AudioManager.instance.SetHasPlayEffectSound(true);
        }
        else if(!isGround)
        {
            AudioManager.instance.SetHasPlayEffectSound(false);
        }    
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Obstacle"))
        {
            AudioManager.instance.PLayHurtClip();
        }
    }
}
