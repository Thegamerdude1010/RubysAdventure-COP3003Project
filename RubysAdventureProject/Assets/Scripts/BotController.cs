using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Since this class is a subclass of EnemyController,
// which inherits MonoBehaviour,
// it does not need to inherit MonoBehaviour.

//I do not believe you can specify visibility for inheritance in C#
//Trying to change visibility (Ex: public class SubClass : public BaseClass) causes an error
public class BotController : EnemyController
{
    // This is demonstrating creating an object of enemy class
    // and using the default (parameterless) constructor to assign default values to private members.
    EnemyClass enemy = new EnemyClass();

    Rigidbody2D rigidbody2d;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Since there is no Update function, the base classes Update function is called

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;

        if (enemy.GetVertical())
        {
            position.y = position.y + Time.deltaTime * enemy.GetSpeed() * direction;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            position.x = position.x + Time.deltaTime * enemy.GetSpeed() * direction;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }

        rigidbody2d.MovePosition(position);
    }

    // If the function below was apart of this class, it would overridee the base class' version
    // Since it is not apart of the code, this class defaults to the base class' version
    
    //void OnCollisionEnter2D(Collision2D other)
    //{
    //    RubyController player = other.gameObject.GetComponent<RubyController>();

    //    if (player != null)
    //    {
    //        player.ChangeHealth(enemy.GetDamage());
    //    }
    //}
}
