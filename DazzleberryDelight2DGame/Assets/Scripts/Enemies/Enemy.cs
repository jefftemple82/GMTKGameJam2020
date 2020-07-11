using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DBD.Core;

namespace DBD.Enemies
{
    public class Enemy : MonoBehaviour
    {
        EnemyCounter enemyCounter;
        float moveSpeed = 10f;
        int health = 1;

        [Header("Power Parameters")]

        [Header("Laser Parameters")]
        [SerializeField] GameObject laserPrefab;
        [SerializeField] float laserSpeed;
        float laserTimer;
        [SerializeField] float minTimeBetweenShots = 0.5f;
        [SerializeField] float maxTimeBetweenShots = 2f;
        // [SerializeField] AudioClip laserSound;
        [SerializeField] float laserSoundVolume;


        // Start is called before the first frame update
        void Start()
        {
            laserTimer = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
            enemyCounter = FindObjectOfType<EnemyCounter>();
        }

        // Update is called once per frame
        void Update()
        {
            CountdownAndShoot();
        }

        void CountdownAndShoot()
        {
            laserTimer -= Time.deltaTime;
            if (laserTimer <= 0)
            {
                Fire();
                laserTimer = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
            }
        }

        void Fire()
        {
            GameObject laser = Instantiate(
                laserPrefab,
                transform.position,
                Quaternion.identity
                ) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(-laserSpeed, 0);
            // AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, laserSoundVolume);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
            if (!damageDealer || other.gameObject.tag == "Enemy") { return; }
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
            enemyCounter.ReduceEnemyCounter();
            Destroy(gameObject);
        }

        public float GetMoveSpeed()
        {
            return moveSpeed;
        }
    }
}