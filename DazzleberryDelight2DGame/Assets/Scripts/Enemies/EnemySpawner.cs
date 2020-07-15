using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DBD.Core;
using DBD.Player;

namespace DBD.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        GameManager gameManager;
        HeroController heroController;

        [SerializeField] GameObject[] enemyPrefabs;
        float currentSpawnTimer = 0f;

        [Header("Intensity Parameters")]
        [SerializeField] float xDirectionMin;
        [SerializeField] float XDirectionMax;
        [SerializeField] float yDirectionMin;
        [SerializeField] float yDirectionMax;
        [SerializeField] float enemyXVelocity;
        [SerializeField] float enemyYVelocity;
        float timeInducedVelocity = 1f;
        float alienInducedVelocity = 1f;

        void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
            heroController = FindObjectOfType<HeroController>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SpawnEnemy()
        {
            timeInducedVelocity = 1 + (gameManager.GetTimerCoefficient());
            alienInducedVelocity = 1 + (gameManager.GetAlienCoefficient());

            int index;

            if (enemyPrefabs.Length > 0)
            {
                index = Random.Range(0, enemyPrefabs.Length);
            }
            else
            {
                index = 0;
            }

            GameObject enemy = Instantiate(
                enemyPrefabs[index],
                transform.position,
                Quaternion.identity
                ) as GameObject;

            float xDirection = Random.Range(xDirectionMin, XDirectionMax);
            float yDirection = Random.Range(yDirectionMin, yDirectionMax);
            float moveSpeedModifier = enemy.GetComponent<Enemy>().GetMoveSpeed();

            float xVelocity = xDirection * enemyXVelocity * timeInducedVelocity * alienInducedVelocity;
            float yVelocity = yDirection * enemyYVelocity * timeInducedVelocity * alienInducedVelocity;

            enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(xVelocity, yVelocity);
        }
    }
}