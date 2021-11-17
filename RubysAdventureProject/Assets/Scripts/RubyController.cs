using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// All C# scripts are subclasses of the MonoBehaviour class.
public class RubyController : MonoBehaviour  //this is a class that stores data for Ruby, the main character.
                                             //It is a subclass of MonoBehavior, a "base class from which every Unity script derives"
{
    // Variable for Ruby's speed. Public to allow it to be adjusted in the Unity editor.
    public float speed = 3.0f;

    // Stores the GameObject for the cog projectile.
    public GameObject projectilePrefab;

    // Variable for Ruby's max health. Public to allow changes in the Unity Editor.
    public int maxHealth = 5;

    // Variable for Ruby's invincibility. Public to allow changes in the Unity Editor.
    // This prevents Ruby from continuously taking damage for the specified time.
    public float timeInvincible = 2.0f;

    // Private variable to store Ruby's health.
    int currentHealth;

    // This allows us to get Ruby's current health, it uses C# properties (Unity Learn).
    public int health { get { return currentHealth; } } 

    // Boolean to determine if the invincibility timer is active.
    bool isInvincible;

    // The invincibility timer.
    float invincibleTimer;

    // Allows us to store the rigid body and access it in the script (Unity Learn).
    Rigidbody2D rigidbody2d;

    // Variables for storing the results of Input.GetAxis() (Unity Learn).
    float horizontal;
    float vertical;

    // Animator variable (Unity Learn).
    Animator animator;

    // Store the look direction.
    // The State Machine doesn't know direction when ruby stands still so we tell it (Unity Learn).
    Vector2 lookDirection = new Vector2(1, 0);
    
    // These hold the particle effects that were assigned in the Unity editor.
    public ParticleSystem collectHealth;
    public ParticleSystem hitRuby;

    // Stores the audio source (Unity Learn).
    AudioSource audioSource;

    // These store the audio clips for throwing a cog and getting hit.
    public AudioClip throwCog;
    public AudioClip getHit;

    // Start is called before the first frame update (Auto Generated).
    void Start()
    {
        // This tells Unity to give you the Rigidbody2D that is attached to the same GameObject that your script is attached to (Unity Learn)
        rigidbody2d = GetComponent<Rigidbody2D>();

        // Sets the current health to the max health at the start of the game.
        currentHealth = maxHealth;

        // These do the same thing as the GetComponent above but for the specific components.
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame (Auto Generated).
    void Update()
    {
        // The tutorial doesn't explain what GetAxis does.
        // I assume it allows the user to retrieve the axis position.
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        
        // This will decrease the invincibility timer/
        if (isInvincible)
        {
            // Decrements the timer.
            invincibleTimer -= Time.deltaTime;

            // Once the timer is 0, isInvincible should be false.
            if (invincibleTimer < 0)
            {
                isInvincible = false;
            }
        }

        // Use a vector instead of changing X and Y positions independently (Unity Learn).
        Vector2 move = new Vector2(horizontal, vertical);

        // Checks if the x or y position is 0.
        // Mathf.Approximately takes into account imprecision in the storage of variables (Unity Learn).
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            // Sets the look direction based on movement (Unity Learn).
            lookDirection.Set(move.x, move.y);

            // Makes the length of lookDirection equal to 1 (Unity Learn).

            lookDirection.Normalize();
        }

        // Sends data to the animator (Unity Learn).
        // These will play the correct animations based on the variables.
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        // This tests to see if the c key is pressed.
        if (Input.GetKeyDown(KeyCode.C)) 
        {
            Launch();
        }

        // This is for Raycasting. The tutorial says it is the action of casting a ray and seeing if it collodes with anything.
        // This lets Ruby talk to the NPC only when she is looking up from infront of him.
        // (Unity Learn)
        if (Input.GetKeyDown(KeyCode.X)) // This tests to see if the x key is pressed.
        {
            // This stores the result of a raycast.
            // The arguments are:
            // 1. The starting position of the ray.
            // 2. The direction, which in this case is the direction Ruby is looking.
            // 3. The maximum distance of the ray.
            // 4. A layer mask to let us test for certain layers.
            // (Unity Learn)
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));

            if (hit.collider != null) // Test to see if the raycast has hit something (Unity Learn).
            {
                // Checks if the object holds the NonPlayerCharacter script (Unity Learn).
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                {
                    // Runs the NPC script display function to display the textbox.
                    character.DisplayDialog();
                }

            }
        }
    }

    // FixedUpdate is used when you want to directly influence physics components or objects (Unity Learn)
    void FixedUpdate()
    {
        // Sets the position to the position of the rigid body (Unity Learn).
        Vector2 position = rigidbody2d.position;
        
        // CHanges the position based on the different variables.
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        // Uses the rigidbody component to move Ruby (Unity Learn).
        rigidbody2d.MovePosition(position);
    }

    // The ChangeHealth function allows us to change Ruby's health.
    public void ChangeHealth(int amount)
    {
        // If Ruby takes damage, amount will be less than 0.
        if (amount < 0)
        {
            animator.SetTrigger("Hit");

            // If Ruby is invincible, the function will not run.
            if (isInvincible)
            {
                return;
            }

            // Makes Ruby invincible after she takes damage.
            isInvincible = true;

            // Sets the invincibility timer to the amount of time Ruby should be invincible.
            invincibleTimer = timeInvincible;

            // Plays the particle effect assigned to hitRuby.
            hitRuby.Play();

            // Plays the sound that is assigned to getHit.
            PlaySound(getHit);
        }

        // Calculates the damage that Ruby takes.
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        
        // If Ruby picks up a collectable, amount will be greater than 0.
        // We want to play the correct particle effect when Ruby gains health.
        if (amount > 0)
        {
            collectHealth.Play(); // Plays the collect health particle effects.
        }

        // This line updates the healthbar dynamically when Ruby's health changes (Unity Learn)
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }

    // This function lets ruby shoot projectiles.
    void Launch() 
    {
        // Quaternion.identity means "no rotation" (Unity Learn).
        // Instantiate takes an object as the first parameter
        // and creates a copy at the position in the second parameter,
        // with the rotation in the third parameter (Unity Learn).
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        // Gets the projectile component assigned to the character.
        Projectile projectile = projectileObject.GetComponent<Projectile>();

        // Launches the projectile.
        projectile.Launch(lookDirection, 300);

        // Plays the animation for Ruby throwing a cog.
        animator.SetTrigger("Launch");

        // This plays the sound attatched to the throwCog variable.
        PlaySound(throwCog);
    }

    // This function takes an audio clip as a parameter so the audio source can play the specific audio clip (Unity Learn).
    public void PlaySound(AudioClip clip)
    {
        // PlayOneShot allows the audio source to play the audio clip once (Unity Learn).
        audioSource.PlayOneShot(clip);
    }
}
