using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClass
{
    public int damage = -1;
    public bool vertical;
    public float speed;

    public EnemyClass() { 
        damage = -1;
        speed = 1;
        vertical = false;
    }

    public EnemyClass(int d = -1, bool v = false, int s = 1)
    {
        if (d < 0)
            damage = d;
        else
            damage = -d;
        vertical = v;
        speed = s;
    }
}
