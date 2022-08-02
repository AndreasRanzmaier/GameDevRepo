using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationAndMovementController : MonoBehaviour
{
    PlayerInput playerInput;
    CharacterController characterController;
    Animator animator;
    int IsWalkingHash;
    int IsRunningHash;
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;
    bool IsMovementPressed;
    bool IsRunPressed;
    float rotationFactorPerFrame = 15.0f;

    void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        IsWalkingHash = Animator.StringToHash("IsWalking");
        IsRunningHash = Animator.StringToHash("IsRunning");

        playerInput.CharacterControls.Move.started += onMovementInput;
        playerInput.CharacterControls.Move.canceled += onMovementInput;
        playerInput.CharacterControls.Move.performed += onMovementInput;
        playerInput.CharacterControls.Run.started += onRun;
        playerInput.CharacterControls.Run.canceled += onRun;

    }

    void onRun(InputAction.CallbackContext context)
    {
        IsRunPressed = context.ReadValueAsButton();
    }

    void onMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;

        currentRunMovement.x = currentMovementInput.x * 3;
        currentRunMovement.z = currentMovementInput.y * 3;

        IsMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    void handleRotation()
    {
        Vector3 positionToLookAt;

        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovement.z;

        Quaternion currentRotation = transform.rotation;

        if (IsMovementPressed)
        {
            Quaternion tartgetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, tartgetRotation, rotationFactorPerFrame);
        }

    }

    void handleGravity()
    {
        float groundedGravity = -0.05f;
        float gravity = -9.8f;

        if (characterController.isGrounded)
        {
            currentMovement.y = groundedGravity;
            currentRunMovement.y = groundedGravity;
        }
        else
        {
            currentMovement.y = gravity;
            currentRunMovement.y = gravity;
        }
    }

    void handleAnimation()
    {
        bool IsWalking = animator.GetBool(IsWalkingHash);
        bool IsRunning = animator.GetBool(IsRunningHash);

        if (IsMovementPressed && !IsWalking)
        {
            animator.SetBool(IsWalkingHash, true);
        }
        else if (!IsMovementPressed && IsWalking)
        {
            animator.SetBool(IsWalkingHash, false);
        }

        if ((IsMovementPressed && IsRunPressed) && !IsRunning)
        {
            animator.SetBool(IsRunningHash, true);
        }
        else if ((!IsMovementPressed && !IsRunPressed) && IsRunning)
        {
            animator.SetBool(IsRunningHash, false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        handleAnimation();
        handleRotation();

        if (IsRunPressed)
        {
            characterController.Move(currentRunMovement * Time.deltaTime);
        }
        else
        {
            characterController.Move(currentMovement * Time.deltaTime);
        }
    }

    void OnEnable()
    {
        // enable the CharacterControlls Action Map
        playerInput.CharacterControls.Enable();
    }

    void OnDisable()
    {
        // disable the CharacterControlls Action Map
        playerInput.CharacterControls.Disable();
    }
}
