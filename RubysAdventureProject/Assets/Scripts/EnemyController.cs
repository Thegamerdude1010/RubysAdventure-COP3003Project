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
    
    float timer;
    protected int direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        timer = changeTime;
    }

    protected void Update()
    {
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
}
