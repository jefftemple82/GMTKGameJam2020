using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    int damage = 1;
    int damageType = 1;

    public int GetDamage()
    {
        return damage;
    }

    public void Hit()
    {
        // later we can use this to destroy the projectile
        return;
    }
}
