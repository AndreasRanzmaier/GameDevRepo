using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    private bool jumpKeyPressed;
    static public int upForcePlayer = 5;
    Vector3 V3upForce = new Vector3(0, upForcePlayer, 0);
    private float horizontalInput;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        // -- Jump -- 
        // get button would also work just fine // KeyDown only checking it once
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            jumpKeyPressed = true;
        }

        horizontalInput = Input.GetAxis("horizontal");
    }

    // FixedUpdate is called once every physics update
    void FixedUpdate()
    {
        if (jumpKeyPressed)
        {
            GetComponent<Rigidbody>().AddForce(V3upForce, ForceMode.VelocityChange);
            jumpKeyPressed = false;
        }
    }
}
