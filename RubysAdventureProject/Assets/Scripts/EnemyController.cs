using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float changeTime = 3.0f;

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
