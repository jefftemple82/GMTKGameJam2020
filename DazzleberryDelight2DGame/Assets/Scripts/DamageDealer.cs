using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 1;
    [SerializeField] int damageType = 1;

    public int GetDamage()
    {
        return damage;
    }

    public void Hit()
    {
        // later we can use this to destroy the projectile
        return;
    }

    public int GetDamageType()
    {
        return damageType;
    }

    public void DestroyDamageDealer()
    {
        Destroy(gameObject);
    }
}
