using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// All C# scripts are subclasses of the MonoBehaviour class.
public class EnemyController : MonoBehaviour 
{

    // By making this variable public, its value can be adjusted within the Unity editor.
    public float changeTime = 3.0f; 

    // This demonstrates creating an object using the default or parameterless constructor.
    EnemyClass enemy = new EnemyClass();

    protected Rigidbody2D rigidbody2d;

    public Animator animator;

    float timer;
    protected int direction = 1;

    protected bool broken = true;

    // Start is called before the first frame update
    void Start()
    {
        timer = changeTime;

    }

    protected void Update()
    {
        if (!broken)
        {
            return;
        }

        // This changes the enemies direction after a specified period of time.
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(enemy.GetDamage());
        }
    }

    // Public because we want to call it from elsewhere like the projectile script
    // This function "fixes" the enemy and stops their movement
    // It prevents enemies from damaging Ruby by removing the rigidbody component
    public void Fix()
    {
        animator = GetComponent<Animator>();
        broken = false;
        rigidbody2d.simulated = false;
        animator.SetTrigger("Fixed");
    }

}
