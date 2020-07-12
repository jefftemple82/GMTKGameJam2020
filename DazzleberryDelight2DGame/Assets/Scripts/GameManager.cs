using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DBD.Core
{
    public class GameManager : MonoBehaviour
    {
        int cityHealth = 10;

        // Start is called before the first frame update
        void Start()
        {
            DontDestroyOnLoad(this);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public int GetCityHealth()
        {
            return cityHealth;
        }

        public void GameOver()
        {
            Time.timeScale = 0;

            Debug.Log("GAME OVER!");
        }

        public void EndLevel()
        {
            LoadNextScene();
        }

        public void LoadNextScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
