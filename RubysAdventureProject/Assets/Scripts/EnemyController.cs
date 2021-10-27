using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float changeTime = 3.0f;

    
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
}
