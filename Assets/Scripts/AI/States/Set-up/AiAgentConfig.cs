using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]

public class AiAgentConfig : ScriptableObject
{
    [Header("General Enemy")]
    public int maxHealth;
    // public int damage;
    public float knockBackForce;

    [Header("Idle State")]

    [Range(0,360)]
    public float idleAngle; [Tooltip("The vision cone for when the enemy is idle")]
    public float movementRadius; [Tooltip("The radius of which random positions can appear in")]
    public float idleMoveSpeed; [Tooltip("The speed when the enemy is idle")]
    public float idleDistance;[Tooltip("The spot sphere radius when idle")]
    public float detectedCoolOff; [Tooltip("Do you want the player to have a chance to escape if seen? ")]

    [Header("Chase State")]

    [Range(0,360)]
    public float chaseAngle; [Tooltip("The vision cone for when the enemy is chasing")]
    public float chaseMoveSpeed; [Tooltip("The speed when the enemy is chasing you")]
    public float lostSightDuration; [Tooltip("How long it takes to lose the player")]
    public float chaseDistance; [Tooltip("The spot sphere radius when chased")]

    [Header("Search State")]

    [Range(0,360)]
    public float searchAngle; [Tooltip("The vision cone for when the enemy is searching")]
    public float searchingMoveSpeed; [Tooltip("The speed of the enemy when searching")]
    public float searchingDuration; [Tooltip("The duration of the enemy searching for the player")]
    public float searchDistance; [Tooltip("The spot sphere radius when searching")]
    public float searchingTimer; [Tooltip("How fast the enemy finds new points to move towards")]

    [Header("Attack State")]
    public float attackMoveSpeed; [Tooltip("The speed when the enemy attacked the player")]
    public float attackCooldown; [Tooltip("The cooldown timer which stops the enemy from attacking constantly")]

    [Header("Shoot State")]

    [Range(0,360)]
    public float shootAngle; [Tooltip("The vision cone for when the enemy is shooting")]
    public float shooterMoveSpeed; [Tooltip("The speed of the enemy when shooting the player")]
    public float shootDistance; [Tooltip("The spot sphere radius when shooting")]
    public GameObject projectile; [Tooltip("Type of projectile")]
    public GameObject rareProjectile; [Tooltip("Boss projectile")] // change this later maybe
    public float shootForce; [Tooltip("How fast the projectile moves")]
    public float shootTimer; [Tooltip("How often the enemy will shoot")]
    public float shootChargeTimer; [Tooltip("The charge time before shooting")]


    [Header("Animations")]
    public Animation idleAnimation;
    public Animation spottedAnimation;
    public Animation chasingAnimation;
    public Animation searchingAnimation;
    public Animation retreatAnimation;
    public Animation deathAnimation;

    [Header("AudioClips")]
    public AudioClip idleAudio;
    public AudioClip spottedAudio;
    public AudioClip chasingAudio;
    public AudioClip searchingAudio;
    public AudioClip retreatAudio;
    public AudioClip deathAudio;
}
