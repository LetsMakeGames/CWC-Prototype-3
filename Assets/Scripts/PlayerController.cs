using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB;
    private int jumpPow = 1250;
    private float gravityPow = 2.75f;
    public bool isOnGround = true;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        Physics.gravity *= gravityPow;
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
        isOnGround = true;
    }
}
