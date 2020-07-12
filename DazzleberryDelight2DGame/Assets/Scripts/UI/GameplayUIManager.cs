using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DBD.Core
{
    public class GameplayUIManager : MonoBehaviour
    {
        GameManager gameManager;

        [SerializeField] Text gameOverText;
        
        [SerializeField] Slider cityHealthSlider;
        float cityHealthSliderTarget;
        float cityHealthSliderDepleteSpeed = 0.5f;
        [SerializeField] int cityHealth;

        [SerializeField] Slider powerSlider;
        float powerSliderTarget;
        float powerSliderFillSpeed = 0.5f;

        float currentPowerLevel;

        // Start is called before the first frame update
        void Start()
        {
            //    optionsController = FindObjectOfType<OptionsController>();

            //    CloseMenu();

            gameManager = FindObjectOfType<GameManager>();
            cityHealth = gameManager.GetCityHealth();
            UpdatePowerLevel(0);
            UpdateCityHealth();
        }

        // Update is called once per frame
        void Update()
        {
            if (powerSlider.value < powerSliderTarget)
            {
                powerSlider.value += powerSliderFillSpeed * Time.deltaTime;
            }
            else if (powerSlider.value > powerSliderTarget)
            {
                powerSlider.value -= powerSliderFillSpeed * Time.deltaTime;
            }

            if (cityHealthSlider.value > cityHealthSliderTarget)
            {
                cityHealthSlider.value -= cityHealthSliderDepleteSpeed * Time.deltaTime;
            }
            else if (cityHealthSlider.value < cityHealthSliderTarget)
            {
                cityHealthSlider.value += cityHealthSliderDepleteSpeed * Time.deltaTime;
            }
        }

        public void UpdatePowerLevel(int power)
        {
            int currentPowerLevel = power;

            powerSliderTarget = currentPowerLevel * 0.1f;
            Debug.Log("Power Slider Target is " + powerSliderTarget);
        }

        private void UpdateCityHealth()
        {
            cityHealthSliderTarget = cityHealth * 0.1f;
            Debug.Log("City Health Target is " + cityHealthSliderTarget);
        }

        public void SubtractCityHealth()
        {
            cityHealth--;
            UpdateCityHealth();

            if (cityHealth <= 0)
            {
                gameManager.GameOver();
                gameOverText.gameObject.SetActive(true);
            }
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