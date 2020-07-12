using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DBD.Buildings
{
    public class BuildingSpawner : MonoBehaviour
    {
        [SerializeField] GameObject[] buildingPrefabs;
        float currentSpawnTimer = 0f;
        float spawnTimer;

        void Start()
        {
            SpawnBuilding();
            RandomizeSpawnTimer();
        }

        // Update is called once per frame
        void Update()
        {
            currentSpawnTimer += Time.deltaTime;

            if (currentSpawnTimer >= spawnTimer)
            {
                SpawnBuilding();
                currentSpawnTimer = 0f;
                RandomizeSpawnTimer();
            }
        }

        private float RandomizeSpawnTimer()
        {
            float minSpawnTimer = 2.5f;
            float maxSpawnTimer = 3.25f;

            spawnTimer = Random.Range(minSpawnTimer, maxSpawnTimer);
            return spawnTimer;
        }

        private void SpawnBuilding()
        {
            int spawnNumber = 0; // adjust this later to spawn random buildings
            float minYOffset = 0f;
            float maxYOffset = 1.5f;
            float yOffset = Random.Range(minYOffset, maxYOffset);
            Vector3 spawnLocation = new Vector3(transform.position.x, transform.position.y - yOffset, transform.position.z);

            GameObject building = Instantiate(
                buildingPrefabs[spawnNumber],
                spawnLocation,
                Quaternion.identity
                ) as GameObject;

            building.GetComponent<Rigidbody2D>().velocity = new Vector2(building.GetComponent<BuildingController>().GetMoveSpeed() * -1, 0);
        }
    }
}