using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DBD.Enemies
{
    public class EnemySpawnController : MonoBehaviour
    {
        [SerializeField] GameObject[] spawnPoints;
        [SerializeField] float spawnTimerMin = 1f;
        [SerializeField] float spawnTimerMax = 2f;
        float currentSpawnTimer;
        float spawnTimer;


        // Start is called before the first frame update
        void Start()
        {
            spawnTimer = GenerateSpawnTimer();
        }

        // Update is called once per frame
        void Update()
        {
            currentSpawnTimer += Time.deltaTime;

            if (currentSpawnTimer >= spawnTimer)
            {
                SpawnEnemy();
                currentSpawnTimer = 0f;

                spawnTimer = GenerateSpawnTimer();
            }
        }

        public float GenerateSpawnTimer()
        {
            return Random.Range(spawnTimerMin, spawnTimerMax);
        }

        private void SpawnEnemy()
        {
            int index = Random.Range(0, spawnPoints.Length);
            EnemySpawner currentSpawnPoint = spawnPoints[index].GetComponent<EnemySpawner>();

            currentSpawnPoint.SpawnEnemy();
        }
    }
}