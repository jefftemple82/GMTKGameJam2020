using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DBD.Player;

namespace DBD.Core
{
    public class ObjectShredder : MonoBehaviour
    {
        GameplayUIManager gameplayUIManager;
        HeroController heroController;

        // Start is called before the first frame update
        void Start()
        {
            gameplayUIManager = FindObjectOfType<GameplayUIManager>();
            heroController = FindObjectOfType<HeroController>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.tag == "Enemy")
            {
                heroController.AddPowerLevel();
            }

            Destroy(other.gameObject);
        }
    }
}