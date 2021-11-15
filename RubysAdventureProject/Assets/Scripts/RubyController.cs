using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// All C# scripts are subclasses of the MonoBehaviour class.
public class RubyController : MonoBehaviour  //this is a class that stores data for Ruby, the main character.
                                             //It is a subclass of MonoBehavior, a "base class from which every Unity script derives"
{
    public float speed = 3.0f;

    public GameObject projectilePrefab;

    public int maxHealth = 5;
    public float timeInvincible = 2.0f;

    int currentHealth;
    public int health { get { return currentHealth; } } //this allows us to get Ruby's current health, it uses C# properties (Unity Learn Tutorial)

    bool isInvincible;
    float invincibleTimer;

    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;

    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);

    public ParticleSystem collectHealth;
    public ParticleSystem hitRuby;

    AudioSource audioSource;

    // these store the audio clips for throwing a cog and getting hit
    public AudioClip throwCog;
    public AudioClip getHit;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;

        animator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
            {
                isInvincible = false;
            }
        }

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (Input.GetKeyDown(KeyCode.C)) // This tests to see if the c key is pressed
        {
            Launch();
        }

        // This is for Raycasting. The tutorial says it is the action of casting a ray and seeing if it collodes with anything.
        // This lets Ruby talk to the NPC only when she is looking up from infron of him
        if (Input.GetKeyDown(KeyCode.X)) // This tests to see if the x key is pressed
        {
            // This stores the result of a raycast.
            // The arguments are:
            // 1. The starting position of the ray.
            // 2. The direction, which in this case is the direction Ruby is looking.
            // 3. The maximum distance of the ray.
            // 4. A layer mask to let us test for certain layers.
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));

            if (hit.collider != null) // Test to see if the raycast has hit something
            {
                // Checks if the object holds the NonPlayerCharacter script
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                {
                    // Runs the NPC script display function to display the textbox
                    character.DisplayDialog();
                }

            }
        }
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            animator.SetTrigger("Hit");
            if (isInvincible)
            {
                return;
            }
            isInvincible = true;
            invincibleTimer = timeInvincible;
            hitRuby.Play();
            PlaySound(getHit);
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        if (amount > 0)
        {
            collectHealth.Play(); // plays the collect health particle effects
        }

        // This line updates the healthbar dynamically when Ruby's health changes (Unity Learn Tutorial)
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }

    void Launch() //lets Ruby shoot projectiles
    {
        // Quaternion.identity means "no rotation"
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");

        PlaySound(throwCog);
    }

    // This function takes an audio clip as a parameter so the audio source can play the specific audio clip.
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
