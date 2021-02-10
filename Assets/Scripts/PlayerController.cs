using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB;
    private int jumpPow = 1250;
    private float gravityPow = 2.75f;
    private bool isOnGround = true;
    private Animator playerAnim;

    public bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        // Get the RB and Animator component of the player
        playerRB = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();

        // Set the game gravity power.
        Physics.gravity *= gravityPow;

        gameOver = false;
        
    }

    // Update is called once per frame
    void Update()
    {

        // If jump button, player jump
        if (Input.GetButtonDown("Jump") && isOnGround && !gameOver)
        {
            PlayerJump();
        }
    }

    void PlayerJump()
    {
        // Set animator trigger for jump animation transition
        playerAnim.SetTrigger("Jump_trig");

        // User AddForce to jump using physics vs translation.
        playerRB.AddForce(Vector3.up * jumpPow, ForceMode.Impulse);

        isOnGround = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If check collision type
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        } else if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Set player animator variables for transitioning to death animation
            playerAnim.SetFloat("Speed_f", 0.0f);
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);

            gameOver = true;
            Debug.Log("Game Over!");
            
        }        
        
    }
}
