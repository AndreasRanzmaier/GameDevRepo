using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private Rigidbody RigidbodyComponent;
    private bool jumpKeyPressed;
    [SerializeField] private int upForcePlayer;
    private Animator mAnimator;

    // Start is called before the first frame update
    void Start()
    {
        RigidbodyComponent = GetComponent<Rigidbody>();
        mAnimator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyPressed = true;
        }
    }

    // FixedUpdate is called once every physics update
    private void FixedUpdate()
    {
        if (jumpKeyPressed)
        {
            RigidbodyComponent.AddForce(Vector3.up * upForcePlayer, ForceMode.VelocityChange);
            mAnimator.SetTrigger("TFlapp");
            jumpKeyPressed = false;
        }
    }
}

