using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DBD.Buildings
{
    public class BuildingSpawner : MonoBehaviour
    {
        [SerializeField] GameObject[] buildingPrefabs;
        [SerializeField] float minYOffset = -6f;
        [SerializeField] float maxYOffset = -2f;

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
            int index = Random.Range(0, buildingPrefabs.Length);
            float yOffset = Random.Range((int)minYOffset, (int)maxYOffset);
            Vector3 spawnLocation = new Vector3(transform.position.x, transform.position.y + yOffset, transform.position.z);

            GameObject building = Instantiate(
                buildingPrefabs[index],
                spawnLocation,
                Quaternion.identity
                ) as GameObject;

            building.GetComponent<Rigidbody2D>().velocity = new Vector2(building.GetComponent<BuildingController>().GetMoveSpeed() * -1, 0);
        }
    }
}