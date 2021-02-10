using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB;
    private int jumpPow = 1250;
    private float gravityPow = 2.75f;
    private bool isOnGround = true;
    public bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        Physics.gravity *= gravityPow;
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            PlayerJump();
        }
    }

    void PlayerJump()
    {
        playerRB.AddForce(Vector3.up * jumpPow, ForceMode.Impulse);
        isOnGround = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        } else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over!");
            gameOver = true;
        }        
        
    }
}
