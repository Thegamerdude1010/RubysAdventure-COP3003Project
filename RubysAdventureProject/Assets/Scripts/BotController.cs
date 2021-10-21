using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : EnemyController
{
    public float speed; //setting variables to public allows them to be changes within Unity

    Rigidbody2D rigidbody2d;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    new

        // Update is called once per frame
        void Update()
    {
        base.Update();
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;

        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
        }

        rigidbody2d.MovePosition(position);
    }
}
