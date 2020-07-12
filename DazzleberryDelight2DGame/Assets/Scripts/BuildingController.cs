using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DBD.Buildings
{
    public class BuildingController : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 2f;
        [SerializeField] int health = 24;
        int halfHealth;
        [SerializeField] GameObject[] buildingPrefabs;
        bool destructionStage0, destructionStage1;



        // Start is called before the first frame update
        void Start()
        {
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
            if (!damageDealer || other.gameObject.tag == "Enemy") { return; }

            int damage = damageDealer.GetDamage();
            if (damage <= 1 || health <= 0) { return; }

            ProcessHit(damage);
        }

        private void ProcessHit(int damage)
        {
            health -= damage;

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
            }
        }

        public float GetMoveSpeed()
        {
            return moveSpeed;
        }
    }
}