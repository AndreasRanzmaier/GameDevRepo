using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] private int upForcePlayer;
    [SerializeField] private Transform GroundCheckTransform;
    [SerializeField] private LayerMask playerMask;
    private bool jumpKeyPressed;
    private float horizontalInput;
    private Rigidbody RigidbodyComponent;
    private int superJumps;
    private Animator mAnimator;


    // Start is called before the first frame update
    void Start()
    {
        RigidbodyComponent = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        // -- Jump -- 
        // get button would also work just fine // KeyDown only checking it once
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyPressed = true;
            mAnimator = GetComponent<Animator>();

        }

        // -- Left Right --
        horizontalInput = Input.GetAxis("Horizontal");
    }

    // FixedUpdate is called once every physics update
    private void FixedUpdate()
    {
        RigidbodyComponent.velocity = new Vector3(horizontalInput, RigidbodyComponent.velocity.y, 0);

        if (Physics.OverlapSphere(GroundCheckTransform.position, 0.01f, playerMask).Length == 0)
        {
            return;
            // exiting fixed because of no air jump
            // code unrelated has to go above
        }

        if (jumpKeyPressed)
        {
            upForcePlayer = 5;
            if (superJumps > 0)
            {
                upForcePlayer += 3;
                superJumps --;
            }
            RigidbodyComponent.AddForce(Vector3.up * upForcePlayer, ForceMode.VelocityChange);
            jumpKeyPressed = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            Destroy(other.gameObject);
            superJumps ++;
        }
    }
}
