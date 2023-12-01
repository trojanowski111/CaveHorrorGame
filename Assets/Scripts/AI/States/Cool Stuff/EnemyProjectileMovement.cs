using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileMovement : MonoBehaviour
{
    public float knockbackForce;
    // public AudioClip hitSound;
    void Start()
    {
        Destroy(gameObject, 20);
    }
    void OnCollisionEnter(Collision other)
    {
        //animation, effect
        // AudioSource.PlayClipAtPoint(hitSound, transform.position);
        Vector3 dir = other.transform.position - transform.position;
        
        if(other.gameObject.CompareTag("Player"))
        {
            // other.gameObject.GetComponent<StarterAssets.ThirdPersonController>().TakeDamage(true);
            // other.gameObject.GetComponent<StarterAssets.ThirdPersonController>().ApplyKnockBack(dir, knockbackForce, true);
            // other.gameObject.GetComponent<StarterAssets.ThirdPersonController>().SetGravityAndSpeed("Projectile", 1.5f, 1.25f);
        }
        Destroy(gameObject);
    }
}
