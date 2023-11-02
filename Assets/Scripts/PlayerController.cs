using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [Header("Variables")]
    public float moveSpeed;
    public float currentSpeed;


    [Header("Sprint")]
    public float sprintSpeed;
    public bool isSprinting;
    public bool canSprint;

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


    [Header("Other")]
    public Animator anim;
    GameObject plModel;
    CharacterController controller;

    float inputX;
    float inputZ;


    
    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = moveSpeed;
        currentStamina = maxStamina;

        plModel = gameObject.transform.GetChild(0).gameObject;
        anim = plModel.gameObject.GetComponent<Animator>();
        controller = transform.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        UpdateAnimations();
        Crouch();
        Sprint();

    }





    //####################
    //      Movement
    //####################

    void Movement()
    {
        // get input value
        inputX = Input.GetAxisRaw("Horizontal");
        inputZ = Input.GetAxisRaw("Vertical");

        // set up new transform
        Vector3 move = transform.right * inputX + transform.forward * inputZ;
        // implement movement onto the CharacterController
        controller.Move(move * currentSpeed * Time.deltaTime);
    }

    void Sprint()
    {
        // If allowed to / unlocked sprint
        if (canSprint)
        {
            // if u get sprint input
            // and if moving forward (works sides too - can't sprint backwards)
            if (Input.GetKey(KeyCode.LeftShift) && (inputZ > 0f || inputX != 0f) && currentStamina > 0f)
            {
                isSprinting = true;
            }
            else
            {
                isSprinting = false;
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
            else
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

    void Crouch()
    {
        // If allowed to / unlocked crouch
        if (canCrouch)
        {
            // if u get crouch input
            if (Input.GetKey(KeyCode.LeftControl))
            {
                isCrouching = true;
                currentSpeed = crouchSpeed;
            }
            else
            {
                isCrouching = false;
                currentSpeed = moveSpeed;

            }

            // when crouching
            if (isCrouching)
            {
                controller.height = 1.5f;
                controller.center = new Vector3(0f, -0.75f, 0f);
            }
            else
            {
                controller.height = 3f;
                controller.center = new Vector3(0f, 0f, 0f);
            }
        }
    }

    void UpdateAnimations()
    {
        anim.SetFloat("InputZ", inputZ);
        anim.SetFloat("InputX", inputX);
        anim.SetBool("isRunning", isSprinting);
        anim.SetBool("isCrouching", isCrouching);

        // if one of the inputs is not a 0, player is in motion 
        if (inputX != 0f || inputZ != 0f)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
    }
}
