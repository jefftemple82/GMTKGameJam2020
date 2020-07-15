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

        [SerializeField] int aliensToKill = 100;
        int totalAliens;
        int aliensKilled = 0;

        // Start is called before the first frame update
        void Start()
        {
            // DontDestroyOnLoad(this);
            gameplayUIManager = FindObjectOfType<GameplayUIManager>();
            gameplayUIManager.UpdateLongestOutburstTime(longestTimeSinceOutburst);
            gameplayUIManager.UpdateAlienCount(aliensToKill);

            totalAliens = aliensToKill;

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

        public float GetTimerCoefficient()
        {
            return timeSinceLastOutburst / 100;
        }

        public float GetAlienCoefficient()
        {
            return aliensKilled / totalAliens;
        }

        public void SubtractAlien()
        {
            aliensToKill--;
            aliensKilled++;
            gameplayUIManager.UpdateAlienCount(aliensToKill);

            if (aliensToKill <= 0)
            {
                GameOver();
            }
        }

        public void GameOver()
        {
            Time.timeScale = 0;

            gameplayUIScreen.SetActive(false);
            resultsScreen.SetActive(true);

            ResetOutburstClock();
            gameplayUIManager.SetResultsScreen(longestTimeSinceOutburst);

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
