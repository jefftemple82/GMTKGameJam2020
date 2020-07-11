using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DBD.Player;

namespace DBD.Core
{
    public class UIManager : MonoBehaviour
    {
        /*
        [SerializeField] GameObject primaryMenuCanvas;
        [SerializeField] GameObject optionsMenuCanvas;
        [SerializeField] GameObject creditsMenuCanvas;

        OptionsController optionsController;
        */

        HeroController heroController;

        [SerializeField] Text punchEnergyText;
        [SerializeField] Text laserEnergyText;

        // Start is called before the first frame update
        void Start()
        {
            heroController = FindObjectOfType<HeroController>();
            //    optionsController = FindObjectOfType<OptionsController>();

            //    CloseMenu();

            UpdateEnergyLevels();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void UpdateEnergyLevels()
        {
            int punchEnergy = heroController.GetPunchEnergy();
            int laserEnergy = heroController.GetLaserEnergy();
            
            punchEnergyText.text = punchEnergy.ToString();
            laserEnergyText.text = laserEnergy.ToString();
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