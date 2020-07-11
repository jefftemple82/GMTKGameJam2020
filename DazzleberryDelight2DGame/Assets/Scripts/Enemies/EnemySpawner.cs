using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DBD.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] GameObject enemyPrefab;
        float currentSpawnTimer = 0f;


        // Update is called once per frame
        void Update()
        {
            float spawnTimer = 1f;
            currentSpawnTimer += Time.deltaTime;

            if (currentSpawnTimer >= spawnTimer)
            {
                SpawnEnemy();
                currentSpawnTimer = 0f;
            }
        }

        void SpawnEnemy()
        {
            GameObject enemy = Instantiate(
                enemyPrefab,
                transform.position,
                Quaternion.identity
                ) as GameObject;

            enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(enemy.GetComponent<Enemy>().GetMoveSpeed() * -1, 0);
        }
    }
}