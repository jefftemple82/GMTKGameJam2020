using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DBD.Core
{
    public class GameplayUIManager : MonoBehaviour
    {
        GameManager gameManager;

        [SerializeField] Slider powerSlider;
        float powerSliderTarget;
        float powerSliderFillSpeed = 0.5f;

        [SerializeField] Text timerText;
        [SerializeField] Text longestOutburstTimerText;
        [SerializeField] Text shortestOutburstTimerText;
        [SerializeField] Text alienCounterText;
        [SerializeField] Text damagesText;

        int damages = 0;

        float currentPowerLevel;

        // Start is called before the first frame update
        void Start()
        {
            //    optionsController = FindObjectOfType<OptionsController>();

            //    CloseMenu();

            gameManager = FindObjectOfType<GameManager>();
            powerSlider.value = 1;
            UpdatePowerLevel(0);
            UpdateDamages(damages);
        }

        // Update is called once per frame
        void Update()
        {
            timerText.text = gameManager.GetTimer().ToString("F1");

            if (powerSlider.value > powerSliderTarget)
            {
                powerSlider.value -= powerSliderFillSpeed * Time.deltaTime;
            }
            else if (powerSlider.value < powerSliderTarget)
            {
                powerSlider.value += powerSliderFillSpeed * Time.deltaTime;
            }
        }

        public void UpdatePowerLevel(int power)
        {
            int currentPowerLevel = 10 - power;

            powerSliderTarget = currentPowerLevel * 0.1f;
            Debug.Log("Power Slider Target is " + powerSliderTarget);
        }

        public void UpdateAlienCount(int aliens)
        {
            alienCounterText.text = aliens.ToString();
        }

        public void UpdateDamages(int money)
        {
            damages += money;
            int negativeMoney = -damages;
            damagesText.text = negativeMoney.ToString();
        }

        public void SubtractCityHealth()
        {

        }

        public void UpdateLongestOutburstTime(float time)
        {
            longestOutburstTimerText.text = time.ToString("F1");
        }

        public void UpdateShortestOutburstTime(float time)
        {
            shortestOutburstTimerText.text = time.ToString("F1");
        }

        public void CloseMenu()
        {
        //    optionsController.SaveOptions();

        //    primaryMenuCanvas.SetActive(false);
        //    optionsMenuCanvas.SetActive(false);        
        //    creditsMenuCanvas.SetActive(false);
        }

        public void OpenOptionsMenu()
        {
        //    primaryMenuCanvas.SetActive(true);
        //    optionsMenuCanvas.SetActive(true);
        }

        public void OpenCreditsMenu()
        {
        //    primaryMenuCanvas.SetActive(true);
        //    creditsMenuCanvas.SetActive(true);
        }
    }
}