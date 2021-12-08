using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// All C# scripts are subclasses of the MonoBehaviour class.
public class EnemyController : MonoBehaviour
{

    // By making this variable public, its value can be adjusted within the Unity editor. (Unity Learn Tutorial)
    public float changeTime = 3.0f;

    // This demonstrates creating an object using the default or parameterless constructor.
    EnemyClass enemy = new EnemyClass();

    // Rigidbody object to store a rigid body.
    protected Rigidbody2D rigidbody2d;

    // Animator object to hold an animator.
    public Animator animator;

    // Particle system objects to hold a particle system.
    public ParticleSystem smokeEffect;
    public ParticleSystem fixBotEffect;

    // Variables for changin the bots direction.
    // timer is the amount of time before the bot changes direction.
    float timer;
    protected int direction = 1;

    // Boolean for determining if the bot is broken.
    // When false, the bot is "fixed" and change its behavior.
    protected bool broken = true;

    // Audio source object to hold an audio source.
    protected AudioSource audioSource;

    // Holds an audio clip.
    public AudioClip getHit;


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

    // This allows the bot to damage Ruby when she enters.
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
        smokeEffect.Stop();
        fixBotEffect.Play();
        PlaySound(getHit);
    }

    // Plays the audio clip passed to the function.
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

}
