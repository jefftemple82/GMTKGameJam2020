using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DBD.Enemies
{
    public class Enemy : MonoBehaviour
    {
        float moveSpeed = 10f;
        int health = 1;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
            if (!damageDealer) { return; }
            ProcessHit(damageDealer);
        }

        private void ProcessHit(DamageDealer damageDealer)
        {
            health -= damageDealer.GetDamage();
            damageDealer.Hit();
            if (health <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Destroy(gameObject);
        }

        public float GetMoveSpeed()
        {
            return moveSpeed;
        }
    }
}