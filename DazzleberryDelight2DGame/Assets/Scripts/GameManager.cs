using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DBD.Core
{
    public class GameManager : MonoBehaviour
    {
        GameplayUIManager gameplayUIManager;
        [SerializeField] GameObject resultsScreen;
        [SerializeField] GameObject gameplayUIScreen;

        float timeSinceLastOutburst = 0f;
        float longestTimeSinceOutburst = 0f;

        [SerializeField] int alientsToKill = 100;

        // Start is called before the first frame update
        void Start()
        {
            // DontDestroyOnLoad(this);
            gameplayUIManager = FindObjectOfType<GameplayUIManager>();
            gameplayUIManager.UpdateLongestOutburstTime(longestTimeSinceOutburst);
            gameplayUIManager.UpdateAlienCount(alientsToKill);

            gameplayUIScreen.SetActive(true);
            resultsScreen.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            timeSinceLastOutburst += Time.deltaTime;
        }

        public void ResetOutburstClock()
        {
            if (longestTimeSinceOutburst == 0 || timeSinceLastOutburst > longestTimeSinceOutburst)
            {
                longestTimeSinceOutburst = timeSinceLastOutburst;
                gameplayUIManager.UpdateLongestOutburstTime(timeSinceLastOutburst);
            }
            timeSinceLastOutburst = 0;
        }

        public float GetTimer()
        {
            return timeSinceLastOutburst;
        }

        public void SubtractAlien()
        {
            alientsToKill--;
            gameplayUIManager.UpdateAlienCount(alientsToKill);

            if (alientsToKill <= 0)
            {
                GameOver();
            }
        }

        public void GameOver()
        {
            Time.timeScale = 0;

            gameplayUIScreen.SetActive(false);
            resultsScreen.SetActive(true);

            Debug.Log("GAME OVER!");
        }

        public void PlayAgain()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void MainMenu()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }

        public void LoadNextScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
