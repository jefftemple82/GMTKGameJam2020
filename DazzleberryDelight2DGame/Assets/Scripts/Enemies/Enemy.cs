using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DBD.Core;

namespace DBD.Enemies
{
    public class Enemy : MonoBehaviour
    {
        GameManager gameManager;

        [SerializeField] float moveSpeedModifier = 1f;
        [SerializeField] int health = 1;

        [Header("Power Parameters")]

        [Header("Laser Parameters")]
        [SerializeField] GameObject laserPrefab;
        [SerializeField] float laserSpeed;
        float laserTimer;
        [SerializeField] float minTimeBetweenShots = 0.5f;
        [SerializeField] float maxTimeBetweenShots = 2f;
        [SerializeField] AudioClip laserSound;
        [SerializeField] float laserSoundVolume;
        float timeInducedVelocity = 1f;
        float alienInducedVelocity = 1f;
        [SerializeField] GameObject deathCryPrefab;


        // Start is called before the first frame update
        void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
            laserTimer = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }

        // Update is called once per frame
        void Update()
        {
            if (!laserPrefab) { return; }
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
            timeInducedVelocity = 1 + (gameManager.GetTimerCoefficient());
            alienInducedVelocity = 1 + (gameManager.GetAlienCoefficient());

            GameObject laser = Instantiate(
                laserPrefab,
                transform.position,
                Quaternion.identity
                ) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(-laserSpeed * timeInducedVelocity * alienInducedVelocity, 0);
            AudioSource.PlayClipAtPoint(laserSound, Camera.main.transform.position, laserSoundVolume);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
            if (!damageDealer || other.gameObject.tag == "Enemy" || other.gameObject.tag == "Enemy Projectile") { return; }

            Debug.Log(gameObject.transform.name + " collided with " + other.gameObject.transform.name);

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
            gameManager.SubtractAlien();
            GameObject deathCry = Instantiate(
                deathCryPrefab,
                transform.position,
                Quaternion.identity
                ) as GameObject;
            Destroy(deathCry, 1f);
            Destroy(gameObject);
        }

        public float GetMoveSpeed()
        {
            return moveSpeedModifier;
        }
    }
}