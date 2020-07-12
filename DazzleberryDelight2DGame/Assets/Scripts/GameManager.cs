using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DBD.Core
{
    public class GameManager : MonoBehaviour
    {
        GameplayUIManager gameplayUIManager;

        float timeSinceLastOutburst = 0f;
        float longestTimeSinceOutburst = 0f;
        float shortestTimeSinceOutburst = 0f;

        // Start is called before the first frame update
        void Start()
        {
            DontDestroyOnLoad(this);
            gameplayUIManager = FindObjectOfType<GameplayUIManager>();
            gameplayUIManager.UpdateLongestOutburstTime(longestTimeSinceOutburst);
            gameplayUIManager.UpdateShortestOutburstTime(shortestTimeSinceOutburst);
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

            if (shortestTimeSinceOutburst == 0 || timeSinceLastOutburst < shortestTimeSinceOutburst)
            {
                shortestTimeSinceOutburst = timeSinceLastOutburst;
                gameplayUIManager.UpdateShortestOutburstTime(timeSinceLastOutburst);
            }

            timeSinceLastOutburst = 0;
        }

        public float GetTimer()
        {
            return timeSinceLastOutburst;
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
