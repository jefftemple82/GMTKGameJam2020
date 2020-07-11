﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DBD.Player
{
    public class HeroController : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 10f;
        [SerializeField] int punchLevel = 1;
        [SerializeField] int laserLevel = 0;

        Coroutine firingCoroutine;

        [Header("Poewr parameters")]
        [SerializeField] int[] powers;
        // 1 = punching, 2 = lasers
        [SerializeField] Slider powerSlider;
        public float fillSpeed = 0.5f;
        int currentPower = 1;
        int currentPowerLevel = 1;
        int maxPowerLevel = 4;

        [Header("Punch Parameters")]
        [SerializeField] GameObject punchImpactPrefab;
        [SerializeField] Transform punchImpactLocation;
        // [SerializeField] AudioClip punchSound;
        [SerializeField] [Range(0, 1)] float punchSoundVolume;
        [SerializeField] float punchFiringPeriod = 1f;

        [Header("Laser Parameters")]
        [SerializeField] GameObject laserPrefab;
        [SerializeField] float laserSpeed = 25f;
        // [SerializeField] AudioClip laserSound;
        [SerializeField] [Range(0, 1)] float laserSoundVolume;
        [SerializeField] float laserFiringPeriod = 0.25f;



        float padding = 1f;
        float xMin;
        float xMax;
        float yMin;
        float yMax;



        // Start is called before the first frame update
        void Start()
        {
            SetUpMoveBoundaries();
            currentPower = 1; // change this later, for testing certain powers only

            UpdatePowerSlider();
        }

        // Update is called once per frame
        void Update()
        {
            Move();
            Fire();
        }

        void SetUpMoveBoundaries()
        {
            Camera gameCamera = Camera.main;
            xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
            xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
            yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
            yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
        }

        void UpdatePowerSlider()
        {
            float value = (float)currentPowerLevel / (float)maxPowerLevel;

            powerSlider.value = value;
            Debug.Log(powerSlider.value);
        }

        void Move()
        {
            var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
            var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

            var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
            var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
            transform.position = new Vector2(newXPos, newYPos);
        }

        void Fire()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                firingCoroutine = StartCoroutine(FireContinuously());
            }
            if (Input.GetButtonUp("Fire1"))
            {
                StopCoroutine(firingCoroutine);
            }
        }

        IEnumerator FireContinuously()
        {
            while (true)
            {
                if (currentPower == 1)
                {
                    GameObject punchImpact = Instantiate(
                        punchImpactPrefab,
                        punchImpactLocation.position,
                        Quaternion.identity) as GameObject;
                    // AudioSource.PlayClipAtPoint(punchSound, Camera.main.transform.position, punchSoundVolume);
                    punchImpact.transform.parent = gameObject.transform;
                    yield return new WaitForSeconds(punchFiringPeriod);
                }
                else if (currentPower == 2) // currently only lasers exist
                {
                    GameObject laser = Instantiate(
                        laserPrefab,
                        transform.position,
                        Quaternion.identity) as GameObject;
                    laser.GetComponent<Rigidbody2D>().velocity = new Vector2(laserSpeed, 0);
                    // AudioSource.PlayClipAtPoint(laserSound, Camera.main.transform.position, laserSoundVolume);
                    yield return new WaitForSeconds(laserFiringPeriod);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
            if (!damageDealer || other.gameObject.tag == "Player") { return; }
            ProcessHit(damageDealer);
            Destroy(other.gameObject);
        }

        private void ProcessHit(DamageDealer damageDealer)
        {
            currentPowerLevel++;
            UpdatePowerSlider();
        }

    }
}