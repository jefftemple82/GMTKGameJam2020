using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DBD.Core;

namespace DBD.Player
{
    public class HeroController : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 10f;
        [SerializeField] int punchLevel = 1;
        [SerializeField] int laserLevel = 0;

        Coroutine firingCoroutine;

        [Header("Poewr parameters")]
        [SerializeField] int[] damageTypeLevels;
        // 1 = punching, 2 = lasers
        public float fillSpeed = 0.5f;
        int currentPowerType = 1;
        int currentPowerLevel = 1;
        int maxPowerLevel = 4;

        [Header("Punch Parameters")]
        [SerializeField] GameObject[] punchImpactPrefab;
        [SerializeField] Transform punchImpactLocation;
        // [SerializeField] AudioClip punchSound;
        [SerializeField] [Range(0, 1)] float punchSoundVolume;
        [SerializeField] float punchFiringPeriod = 1f;

        [Header("Laser Parameters")]
        [SerializeField] GameObject[] laserPrefab;
        [SerializeField] float laserSpeed = 25f;
        // [SerializeField] AudioClip laserSound;
        [SerializeField] [Range(0, 1)] float laserSoundVolume;
        [SerializeField] float laserFiringPeriod = 0.25f;

        UIManager uiManager;

        float padding = 1f;
        float xMin;
        float xMax;
        float yMin;
        float yMax;



        // Start is called before the first frame update
        void Start()
        {
            SetUpMoveBoundaries();
            currentPowerType = 2; // change this later, for testing certain powers only
            uiManager = FindObjectOfType<UIManager>();
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
                    else if (currentPowerLevel == 1)
                    {
                        GameObject punchImpact = Instantiate(
                            punchImpactPrefab[currentPowerLevel - 1],
                            punchImpactLocation.position,
                            Quaternion.identity) as GameObject;
                        // AudioSource.PlayClipAtPoint(punchSound, Camera.main.transform.position, punchSoundVolume);
                        punchImpact.transform.parent = gameObject.transform;
                        yield return new WaitForSeconds(punchFiringPeriod);
                    }
                    else if (currentPowerLevel == 2)
                    {
                        GameObject punchImpact = Instantiate(
                            punchImpactPrefab[currentPowerLevel - 1],
                            punchImpactLocation.position,
                            Quaternion.identity) as GameObject;
                        // AudioSource.PlayClipAtPoint(punchSound, Camera.main.transform.position, punchSoundVolume);
                        punchImpact.transform.parent = gameObject.transform;
                        yield return new WaitForSeconds(punchFiringPeriod);
                    }
                    else if (currentPowerLevel == 3)
                    {
                        GameObject punchImpact = Instantiate(
                            punchImpactPrefab[currentPowerLevel - 1],
                            punchImpactLocation.position,
                            Quaternion.identity) as GameObject;
                        // AudioSource.PlayClipAtPoint(punchSound, Camera.main.transform.position, punchSoundVolume);
                        punchImpact.transform.parent = gameObject.transform;
                        yield return new WaitForSeconds(punchFiringPeriod);
                    }

                }
                else if (currentPowerType == 2) // lasers
                {
                    if (currentPowerLevel >= maxPowerLevel) { yield return null; }
                    else if (currentPowerLevel == 1)
                    {
                        GameObject laser = Instantiate(
                            laserPrefab[currentPowerLevel - 1],
                            transform.position,
                            Quaternion.identity) as GameObject;
                        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(laserSpeed, 0);
                        // AudioSource.PlayClipAtPoint(laserSound, Camera.main.transform.position, laserSoundVolume);
                        yield return new WaitForSeconds(laserFiringPeriod);
                    }
                    else if (currentPowerLevel == 2)
                    {
                        GameObject laser = Instantiate(
                            laserPrefab[currentPowerLevel - 1],
                            transform.position,
                            Quaternion.identity) as GameObject;
                        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(laserSpeed, 0);
                        // AudioSource.PlayClipAtPoint(laserSound, Camera.main.transform.position, laserSoundVolume);
                        yield return new WaitForSeconds(laserFiringPeriod);
                    }
                    else if (currentPowerLevel == 3)
                    {
                        GameObject laser = Instantiate(
                            laserPrefab[currentPowerLevel - 1],
                            transform.position,
                            Quaternion.identity) as GameObject;
                        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(laserSpeed, 0);
                        // AudioSource.PlayClipAtPoint(laserSound, Camera.main.transform.position, laserSoundVolume);
                        yield return new WaitForSeconds(laserFiringPeriod);
                    }

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
            int damageType = damageDealer.GetDamageType();

            if (damageType != currentPowerType)
            {
                currentPowerType = damageType;
                damageTypeLevels[damageType - 1]++;

                currentPowerLevel = damageTypeLevels[damageType - 1];
                Debug.Log("Damage type changed to " + damageType + " with a power level of " + currentPowerLevel);
            }
            else
            {
                damageTypeLevels[damageType - 1]++;
                currentPowerLevel =  damageTypeLevels[damageType - 1];

                Debug.Log("Damage type remains " + damageType + " and power level is now " + currentPowerLevel);

                if (currentPowerLevel >= maxPowerLevel)
                {
                    OutOfControl();
                    Debug.Log("Triggering OUT OF CONTROL event");
                }
            }

            uiManager.UpdateEnergyLevels();
        }

        private void OutOfControl()
        {
            if (currentPowerType == 1) // punches
            {
                ResetPowerLevels();

                GameObject deathWave = Instantiate(
                    punchImpactPrefab[currentPowerLevel - 1],
                    transform.position,
                    Quaternion.identity) as GameObject;
            }
            else if (currentPowerType == 2) // lasers
            {
                ResetPowerLevels();

                GameObject deathWave = Instantiate(
                    laserPrefab[currentPowerLevel - 1],
                    transform.position,
                    Quaternion.identity) as GameObject;
            }
        }

        private void ResetPowerLevels()
        {
            damageTypeLevels[0] = 1;
            damageTypeLevels[1] = 1;
            currentPowerLevel = 1;

            uiManager.UpdateEnergyLevels();
        }

        public int GetPunchEnergy()
        {
            return damageTypeLevels[0];
        }
        
        public int GetLaserEnergy()
        {
            return damageTypeLevels[1];
        }

    }
}