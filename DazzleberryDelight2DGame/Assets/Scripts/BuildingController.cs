using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DBD.Core;

namespace DBD.Buildings
{
    public class BuildingController : MonoBehaviour
    {
        GameplayUIManager gameplayUIManager;

        [SerializeField] int cashValue;
        [SerializeField] float moveSpeed = 2f;
        [SerializeField] float fallSpeed = 1f;
        [SerializeField] int health = 24;
        int halfHealth;
        [SerializeField] GameObject[] buildingPrefabs;
        bool destructionStage0, destructionStage1;
        bool notDestroyed = true;
        [SerializeField] AudioClip smashSound;
        [SerializeField] float smashSoundVolume;


        // Start is called before the first frame update
        void Start()
        {
            gameplayUIManager = FindObjectOfType<GameplayUIManager>();

            buildingPrefabs[0].SetActive(true);
            buildingPrefabs[1].SetActive(false);
            buildingPrefabs[2].SetActive(false);

            halfHealth = health / 2;
            destructionStage0 = true;
            destructionStage1 = false;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
            if (!damageDealer) { return; }

            if (other.gameObject.tag == "Player Attack")
            {
                int damage = damageDealer.GetDamage();
                // if (damage <= 1 || health <= 0) { return; }

                if (notDestroyed)
                {
                    ProcessHit(damage);
                }
                else { return; }
            }
        }

        private void ProcessHit(int damage)
        {
            health -= damage;
            float damages = cashValue / damage;
            gameplayUIManager.UpdateDamages(damages);
            AudioSource.PlayClipAtPoint(smashSound, Camera.main.transform.position, smashSoundVolume);


            if (health <= halfHealth && destructionStage0 == true)
            {
                buildingPrefabs[0].SetActive(false);
                buildingPrefabs[1].SetActive(true);

                destructionStage0 = false;
                destructionStage1 = true;
            }
            else if (health <= 0)
            {
                buildingPrefabs[1].SetActive(false);
                buildingPrefabs[2].SetActive(true);

                notDestroyed = false;
                GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, -fallSpeed);
                Destroy(gameObject, 10f);
            }
        }

        public float GetMoveSpeed()
        {
            return moveSpeed;
        }
    }
}