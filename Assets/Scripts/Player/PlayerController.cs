using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;

    [Header("Variables")]
    public float moveSpeed;
    public float currentSpeed;
    public float gravity;
    public bool isGrounded;
    public bool ceilingAbove;
    private bool canMove = true;

    [SerializeField] private float groundCheckRadius;
    [SerializeField] private float ceilingCheckRadius;

    [Header("Sprint")]
    public float sprintSpeed;
    public bool isSprinting;
    public bool canSprint;
    public bool allowSprint;

    [Space(10)]
    public float currentStamina;
    public float maxStamina;
    public float staminaDrain;
    public float staminaRecovery;
    public float staminaTimer;
    public float staminaCurrentTimer;
    public Slider staminaSlider;

    [Header("Crouch")]
    public float crouchSpeed;
    public bool isCrouching;
    public bool canCrouch;
    public bool allowCrouch;

    [Header("Other")]
    public Animator anim;
    public Transform groundCkeck;
    public Transform ceilingCkeck;
    public LayerMask groundMask;
    public LayerMask ceilingMask;
    GameObject plModel;
    CharacterController controller;
    float gravityVelocity;
    private Vector2 movementInput;
    private bool sprintInput;
    private bool crouchInput;

    private void OnEnable()
    {
        inputReader.moveEvent += SetMovementInput;
        inputReader.sprintEvent += SetSprintInput;
        inputReader.crouchEvent += SetCrouchInput;
    }
    private void OnDisable()
    {
        inputReader.moveEvent -= SetMovementInput;
        inputReader.sprintEvent -= SetSprintInput;
        inputReader.crouchEvent -= SetCrouchInput;
    }
    void Start()
    {
        currentSpeed = moveSpeed;
        currentStamina = maxStamina;

        plModel = gameObject.transform.GetChild(0).gameObject;
        anim = plModel.gameObject.GetComponent<Animator>();
        controller = transform.GetComponent<CharacterController>();
    }
    void Update()
    {
        if(!canMove)
        return;
        Movement(movementInput);
        Sprint(sprintInput);
        Crouch(crouchInput);
        Gravity();
        UpdateAnimations();
    }
    private void FixedUpdate()
    {
        // ground and ceiling check
        isGrounded = Physics.CheckSphere(groundCkeck.position, groundCheckRadius, groundMask);
        ceilingAbove = Physics.CheckSphere(ceilingCkeck.position, ceilingCheckRadius, ceilingMask);
    }

    //####################
    //      Movement
    //####################
    private void Gravity()
    {
        if (isGrounded)
        {
            // small velocity
            gravityVelocity = -5f;
        }
        else
        {
            // if not grounded - dude be falling
            gravityVelocity += gravity * Time.deltaTime;
        }
    }
    private void Movement(Vector2 moveInput) // get input value
    {
        if(moveInput == Vector2.zero)
        return;
        // set up new transform
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;

        // implement movement onto the CharacterController
        Vector3 combinedForces = move * currentSpeed + new Vector3(0, gravityVelocity, 0);

        controller.Move(combinedForces * Time.deltaTime);
    }
    private void Sprint(bool sprintInput)
    {
        // If allowed to / unlocked sprint
        if (allowSprint)
        {
            // if u get sprint input
            // and if moving forward (works sides too - can't sprint backwards)
            if (sprintInput && canSprint && (movementInput.y > 0f || movementInput.x != 0f) && currentStamina > 0f)
            {
                isSprinting = true;
                canCrouch = false;
            }
            else
            {
                isSprinting = false;
                canCrouch = true;
            }

            // when sprinting
            if (isSprinting)
            {
                // update the speed to sprint speed
                currentSpeed = sprintSpeed;
                // reduce value from stamina * by multiplier (drain)
                currentStamina -= Time.deltaTime * staminaDrain;
                // set stamina current cooldown timer back to max
                staminaCurrentTimer = staminaTimer;
            }
            else if (!isCrouching)
            {
                // else go back to default
                currentSpeed = moveSpeed;

                // start reducing value from the timer
                if (staminaCurrentTimer > 0f)
                {
                    staminaCurrentTimer -= Time.deltaTime;
                }
                else
                {
                    staminaCurrentTimer = 0f;
                }
            }
            // once it hits 0, recover the stamina * by the recovery multiplier
            if (staminaCurrentTimer == 0f)
            {
                if (currentStamina < maxStamina)
                {
                    currentStamina += Time.deltaTime * staminaRecovery;
                }
                else
                {
                    currentStamina = maxStamina;
                }
            }


            // Slider visiblitiy
            if (currentStamina < maxStamina)
            {
                staminaSlider.gameObject.SetActive(true);
            }
            else if (currentStamina >= maxStamina)
            {
                staminaSlider.gameObject.SetActive(false);
                currentStamina = maxStamina;
            }

            staminaSlider.maxValue = maxStamina;
            staminaSlider.value = currentStamina;
        }
    }
    private void Crouch(bool crouchInput)
    {
        Debug.Log(crouchInput);
        // If allowed to / unlocked crouch
        if (allowCrouch)
        {
            // if u get crouch input
            if (crouchInput && canCrouch)
            {
                canSprint = false;
                isCrouching = true;
                currentSpeed = crouchSpeed;
            }
            else if (!isSprinting && !ceilingAbove)
            {
                canSprint = true;
                isCrouching = false;
                currentSpeed = moveSpeed;
            }

            // when crouching
            if (isCrouching)
            {
                controller.height = 2f;
                controller.center = new Vector3(0f, -0.5f, 0f);
            }
            else 
            {
                controller.height = 3f;
                controller.center = new Vector3(0f, 0f, 0f);
            }
        }
    }
    private void UpdateAnimations()
    {
        anim.SetFloat("InputZ", movementInput.y);
        anim.SetFloat("InputX", movementInput.x);
        anim.SetBool("isRunning", isSprinting);
        anim.SetBool("isCrouching", isCrouching);

        // if one of the inputs is not a 0, player is in motion 
        if (movementInput.x != 0f || movementInput.y != 0f)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
    }
    public void SetCanMove(bool newMove) // for dialogue, prob just turn the whole player movement script off through an event than this
    {
        canMove = newMove;
    }
    private void SetMovementInput(Vector2 vector2Input)
    {
        movementInput = vector2Input;
    }
    private void SetSprintInput(bool newInput)
    {
        sprintInput = newInput;
    }
    private void SetCrouchInput(bool newInput)
    {
        crouchInput = newInput;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCkeck.position, groundCheckRadius);
        Gizmos.DrawSphere(ceilingCkeck.position, ceilingCheckRadius);
    }
}
