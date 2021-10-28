using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// All C# scripts are subclasses of the MonoBehaviour class.
public class HealthCollectible : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();

        if (controller != null)
        {
            if(controller.health < controller.maxHealth)
            {
                // This calls the RubyController class' change health function
                // using the an object of RubyController called controller.
                // Since this is a health pickup, the function is passed a value of 1
                // in order to increase her health.
                controller.ChangeHealth(1);

                // This destroys the game pbject when it is picked up
                Destroy(gameObject); 
            }
        }
    }
}
