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
    private float counter;

    public bool gameOver = false;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioSource footStep;
    public AudioSource impact;
    public AudioSource deathSound;
    public AudioSource jumpSound;

    private AudioSource bgmAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        // Get the RB and Animator component of the player
        playerRB = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();

        // Set the game gravity power.
        Physics.gravity *= gravityPow;

        gameOver = false;

        bgmAudioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();


    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;

        // If jump button, player jump
        if (Input.GetButtonDown("Jump") && isOnGround && !gameOver)
        {
            PlayerJump();
        } 
        
        if (counter >= 0.25 && isOnGround && !gameOver)
        {
            footStep.Play();
            counter = 0;
        }
    }

    void PlayerJump()
    {
        // Set animator trigger for jump animation transition
        playerAnim.SetTrigger("Jump_trig");
        dirtParticle.Stop();
        jumpSound.Play();

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
            if (!gameOver)
            {
                dirtParticle.Play();
            }

        } else if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Set player animator variables for transitioning to death animation
            playerAnim.SetFloat("Speed_f", 0.0f);
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            bgmAudioSource.Stop();
            impact.Play();
            deathSound.Play();            

            gameOver = true;
            Debug.Log("Game Over!");
            
        }        
        
    }

}
