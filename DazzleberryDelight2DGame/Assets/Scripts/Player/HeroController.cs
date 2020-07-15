using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DBD.Core;

namespace DBD.Player
{
    public class HeroController : MonoBehaviour
    {
        GameManager gameManager;
        GameplayUIManager gameplayUIManager;
        Rigidbody2D myRigidBody;

        [SerializeField] float moveSpeed = 10f;
        [SerializeField] int punchLevel = 1;
        [SerializeField] int laserLevel = 0;

        Coroutine firingCoroutine;

        [Header("Power parameters")]
        [SerializeField] int[] damageTypeLevels;
        // 1 = punching, 2 = lasers
        public float fillSpeed = 0.5f;
        int currentPowerType = 1;
        int currentPowerLevel = 0;
        int maxPowerLevel = 10;
        bool facingLeft = false;
        [SerializeField] AudioClip takeDamage;
        [SerializeField] [Range(0, 1)] float takeDamageVolume;


        [Header("Punch Parameters")]
        [SerializeField] GameObject[] punchImpactPrefab;
        [SerializeField] Transform punchImpactLocation;
        [SerializeField] AudioClip punchSound;
        [SerializeField] [Range(0, 1)] float punchSoundVolume;
        [SerializeField] float punchFiringPeriod = 0.5f;
        [SerializeField] AudioClip punchDeathWaveSound;
        [SerializeField] [Range(0, 1)] float punchDeathWaveSoundVolume = 0.6f;

        [Header("Laser Parameters")]
        [SerializeField] GameObject[] laserPrefab;
        [SerializeField] Transform laserLocation;
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
            gameManager = FindObjectOfType<GameManager>();
            gameplayUIManager = FindObjectOfType<GameplayUIManager>();
            myRigidBody = GetComponent<Rigidbody2D>();

            SetUpMoveBoundaries();
            currentPowerType = 1; // change this later, for testing certain powers only
        }

        // Update is called once per frame
        void Update()
        {
            Move();
            Fire();
            FlipSprite();
        }

        void SetUpMoveBoundaries()
        {
            Camera gameCamera = Camera.main;
            xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
            xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
            yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
            yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
        }

        /*
        void UpdatePowerSlider()
        {
            float value = (float)currentPowerLevel / (float)maxPowerLevel;

            powerSlider.value = value;
            Debug.Log(powerSlider.value);
        }
        */

        void Move()
        {
            var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
            var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

            var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
            var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
            transform.position = new Vector2(newXPos, newYPos);

            /* 
            if (deltaX < 0)
            {
                transform.localScale = new Vector2(-1f, 1f);
                punchImpactLocation.localScale = new Vector2(-1f, 1f);
            }
            else if (deltaX > 0)
            {
                transform.localScale = new Vector2(1f, 1f);
                punchImpactLocation.localScale = new Vector2(1f, 1f);
            }
            else
            {
                return;
            }
            */
        }

        private void FlipSprite()
        {
            
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
                if (currentPowerType == 1) // punching
                {
                    if (currentPowerLevel >= maxPowerLevel) { yield return null; }
                    else if (currentPowerLevel >= 0 && currentPowerLevel <= 3)
                    {
                        GameObject punchImpact = Instantiate(
                            punchImpactPrefab[0],
                            punchImpactLocation.position,
                            transform.rotation) as GameObject;
                        punchImpact.transform.localScale = punchImpactLocation.localScale;
                        AudioSource.PlayClipAtPoint(punchSound, Camera.main.transform.position, punchSoundVolume);
                        // punchImpact.transform.parent = gameObject.transform;
                        Destroy(punchImpact, punchFiringPeriod);

                        yield return new WaitForSeconds(punchFiringPeriod);
                    }
                    else if (currentPowerLevel >= 4 && currentPowerLevel <= 6)
                    {
                        GameObject punchImpact = Instantiate(
                            punchImpactPrefab[1],
                            punchImpactLocation.position,
                            Quaternion.identity) as GameObject;
                        AudioSource.PlayClipAtPoint(punchSound, Camera.main.transform.position, punchSoundVolume);
                        // punchImpact.transform.parent = gameObject.transform;
                        Destroy(punchImpact, punchFiringPeriod);

                        yield return new WaitForSeconds(punchFiringPeriod);
                    }
                    else if (currentPowerLevel >= 7 && currentPowerLevel <= 9)
                    {
                        GameObject punchImpact = Instantiate(
                            punchImpactPrefab[2],
                            punchImpactLocation.position,
                            Quaternion.identity) as GameObject;
                        AudioSource.PlayClipAtPoint(punchSound, Camera.main.transform.position, punchSoundVolume);
                        // punchImpact.transform.parent = gameObject.transform;
                        Destroy(punchImpact, punchFiringPeriod);

                        yield return new WaitForSeconds(punchFiringPeriod);
                    }

                }
                /*
                else if (currentPowerType == 2) // lasers
                {
                    if (currentPowerLevel >= maxPowerLevel) { yield return null; }
                    else if (currentPowerLevel == 1)
                    {
                        GameObject laser = Instantiate(
                            laserPrefab[currentPowerLevel - 1],
                            laserLocation.position,
                            Quaternion.identity) as GameObject;
                        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(laserSpeed, 0);
                        // AudioSource.PlayClipAtPoint(laserSound, Camera.main.transform.position, laserSoundVolume);
                        Destroy(laser, 3f);

                        yield return new WaitForSeconds(laserFiringPeriod);
                    }
                    else if (currentPowerLevel == 2)
                    {
                        GameObject laser = Instantiate(
                            laserPrefab[currentPowerLevel - 1],
                            laserLocation.position,
                            Quaternion.identity) as GameObject;
                        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(laserSpeed, 0);
                        // AudioSource.PlayClipAtPoint(laserSound, Camera.main.transform.position, laserSoundVolume);
                        yield return new WaitForSeconds(laserFiringPeriod);
                    }
                    else if (currentPowerLevel == 3)
                    {
                        GameObject laser = Instantiate(
                            laserPrefab[currentPowerLevel - 1],
                            laserLocation.position,
                            Quaternion.identity) as GameObject;
                        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(laserSpeed, 0);
                        // AudioSource.PlayClipAtPoint(laserSound, Camera.main.transform.position, laserSoundVolume);
                        yield return new WaitForSeconds(laserFiringPeriod);
                    }

                }
                */
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
            if (!damageDealer || other.gameObject.tag == "Player Attack") { return; }
            ProcessHit();
            Destroy(other.gameObject);
        }

        private void ProcessHit()
        {
            currentPowerLevel++;
            AudioSource.PlayClipAtPoint(takeDamage, Camera.main.transform.position, takeDamageVolume);
            gameplayUIManager.UpdatePowerLevel(currentPowerLevel);

            if (currentPowerLevel >= maxPowerLevel)
                {
                    OutOfControl();
                    Debug.Log("Triggering OUT OF CONTROL event");
                }
        }

        private void OutOfControl()
        {
            if (currentPowerType == 1) // punches
            {
                GameObject deathWave = Instantiate(
                    punchImpactPrefab[3],
                    transform.position,
                    Quaternion.identity) as GameObject;
                AudioSource.PlayClipAtPoint(punchDeathWaveSound, Camera.main.transform.position, punchDeathWaveSoundVolume);

                ResetPowerLevels();
                Destroy(deathWave, 1f);
            }
            else if (currentPowerType == 2) // lasers
            {
                GameObject deathWave = Instantiate(
                    laserPrefab[currentPowerLevel - 1],
                    transform.position,
                    Quaternion.identity) as GameObject;

                ResetPowerLevels();
            }
        }

        private void ResetPowerLevels()
        {
            currentPowerLevel = 1;

            gameManager.ResetOutburstClock();
            gameplayUIManager.UpdatePowerLevel(currentPowerLevel - 1);
        }

        public void AddPowerLevel()
        {
            currentPowerLevel++;
            gameplayUIManager.UpdatePowerLevel(currentPowerLevel);

            if (currentPowerLevel >= maxPowerLevel)
            {
                OutOfControl();
            }
        }

        public int GetPowerVelocityModifier()
        {
            return currentPowerLevel;
        }

    }
}