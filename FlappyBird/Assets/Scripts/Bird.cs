using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private Rigidbody RigidbodyComponent;
    private bool jumpKeyPressed;
    [SerializeField] private int upForcePlayer;
    private Animator mAnimator;
    private string currentState;
    private bool isJumping;

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
            jumpKeyPressed = false;

            // Movement
            RigidbodyComponent.AddForce(Vector3.up * upForcePlayer, ForceMode.VelocityChange);

            // Animation
            if (!isJumping)
            {
                isJumping = true;
                ChangeAnimationState("BirdFlapp");
                Invoke("JumpComplete", 0.13f); // mAnimator.GetCurrentAnimatorStateInfo(0).length - 0.3f
            }
        }
    }

    void JumpComplete()
    {
        isJumping = false;
        ChangeAnimationState("Idle");
    }

    void ChangeAnimationState(string newState)
    {
        //Guard
        if(currentState == newState) return;
        
        mAnimator.Play(newState);
        
        currentState = newState;
    }
}
