using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DBD.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] GameObject enemyPrefab;

        // Start is called before the first frame update
        void Start()
        {
            SpawnEnemy();
        }

        // Update is called once per frame
        void Update()
        {

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