using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace DBD.Core
{
    public class EnemyCounter : MonoBehaviour
    {
        [SerializeField] int enemyCounter;
        [SerializeField] TextMeshProUGUI enemyCounterText;
        GameManager gameManager;

        // Start is called before the first frame update
        void Start()
        {
            enemyCounterText.text = enemyCounter.ToString();
            gameManager = FindObjectOfType<GameManager>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ReduceEnemyCounter()
        {
            enemyCounter--;
            enemyCounterText.text = enemyCounter.ToString();

            if (enemyCounter <= 0)
            {
                gameManager.EndLevel();
            }
        }
    }

}