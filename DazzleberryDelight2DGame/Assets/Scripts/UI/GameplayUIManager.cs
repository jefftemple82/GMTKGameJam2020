using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DBD.Core
{
    public class GameplayUIManager : MonoBehaviour
    {
        [SerializeField] Text punchEnergyText;
        
        [SerializeField] Slider citySlider;
        
        [SerializeField] Slider powerSlider;
        float powerSliderTarget;
        float powerSliderFillSpeed = 0.5f;

        float currentPowerLevel;

        // Start is called before the first frame update
        void Start()
        {
            //    optionsController = FindObjectOfType<OptionsController>();

            //    CloseMenu();

            UpdatePowerLevel(0);
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
        }

        public void UpdatePowerLevel(int power)
        {
            int currentPowerLevel = power;

            powerSliderTarget = currentPowerLevel * 0.1f;
            Debug.Log("Power Slider Target is " + powerSliderTarget);
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