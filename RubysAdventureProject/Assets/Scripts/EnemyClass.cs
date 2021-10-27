using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClass
{
    private int damage = -1;
    private bool vertical;
    private float speed;

    public EnemyClass() { 
        damage = -1;
        speed = 1.0f;
        vertical = false;
    }

    public EnemyClass(int d = -1, bool v = false, float s = 1.0f)
    {
        if (d < 0)
            damage = d;
        else
            damage = -d;
        vertical = v;
        speed = s;
    }

    public int GetDamage() { return damage; }
    public bool GetVertical() { return vertical; }
    public float GetSpeed() { return speed; }
}
