using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Since this class is a subclass of EnemyController,
// which inherits MonoBehaviour,
// it does not need to inherit MonoBehaviour.

//I do not believe you can specify visibility for inheritance in C#
//Trying to change visibility (Ex: public class SubClass : public BaseClass) causes an error
public class RedBotController : EnemyController
{
    // This is demonstrating creating an object of EnemyClass
    // and using a constructor to assign values to private members.
    EnemyClass redEnemy = new EnemyClass(-3, 2.0f);

    public bool vertical;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // This was placed here because it was not working in the base class
        audioSource = GetComponent<AudioSource>();
    }

    new

        // Update is called once per frame
        void Update() // this Update function overides the base class' Update function
    {
        // Using base.Update calls the base class' update function.
        // This essentially makes the subclass' update function useless
        base.Update();
    }

    // This function changes the bots position.
    void FixedUpdate()
    {
        if (!broken)
        {
            return;
        }
        Vector2 position = rigidbody2d.position;

        // If the bot is supposed to travel vertically, it changes y position,
        // otherwise it changes the x position.
        if (vertical)
        {
            position.y = position.y + Time.deltaTime * redEnemy.GetSpeed() * direction;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            position.x = position.x + Time.deltaTime * redEnemy.GetSpeed() * direction;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }

        rigidbody2d.MovePosition(position);
    }

    // Since OnCollisionEnter2D is defined in this class,
    // it will overide the base class' version of the function.
    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(redEnemy.GetDamage());
        }
    }


}
