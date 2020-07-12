using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DBD.Core
{
    public class ObjectShredder : MonoBehaviour
    {
        GameplayUIManager gameplayUIManager;

        // Start is called before the first frame update
        void Start()
        {
            gameplayUIManager = FindObjectOfType<GameplayUIManager>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.tag == "Enemy")
            {
                gameplayUIManager.SubtractCityHealth();
            }

            Destroy(other.gameObject);
        }
    }
}