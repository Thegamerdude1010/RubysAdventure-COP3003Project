using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// All C# scripts are subclasses of the MonoBehaviour class.
public class HealthCollectible : MonoBehaviour
{
    // Setting this public allows us to assign the audio clip in the editor.
    public AudioClip collectedClip;

    // Gets called on the first frame when a RigidBody enters a trigger (Unity Learn).
    void OnTriggerEnter2D(Collider2D other)
    {
        // This gets the RubyController component on the GameObject that enters the Trigger (Unity Learn).
        RubyController controller = other.GetComponent<RubyController>();

        // If statement gets entered if the controller has something in it.
        if (controller != null)
        {
            // Ruby will only pick up the collectable if her health is less than the max health.
            if(controller.health < controller.maxHealth)
            {
                // This calls the RubyController class' change health function
                controller.ChangeHealth(1);

                // This destroys the game pbject when it is picked up.
                Destroy(gameObject);

                // Passes the assigned audio clip to the PlaySound function in the Ruby controller.
                // Now, the sound assigned to collectedClip will play when the health pickup is collected.
                controller.PlaySound(collectedClip);
            }
        }
    }
}
