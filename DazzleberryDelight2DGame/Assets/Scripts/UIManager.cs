using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DBD.Core
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] GameObject primaryMenuCanvas;
        [SerializeField] GameObject optionsMenuCanvas;
        [SerializeField] GameObject creditsMenuCanvas;

        OptionsController optionsController;

        // Start is called before the first frame update
        void Start()
        {
            optionsController = FindObjectOfType<OptionsController>();

            CloseMenu();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void CloseMenu()
        {
            optionsController.SaveOptions();

            primaryMenuCanvas.SetActive(false);
            optionsMenuCanvas.SetActive(false);        
            creditsMenuCanvas.SetActive(false);
        }

        public void OpenOptionsMenu()
        {
            primaryMenuCanvas.SetActive(true);
            optionsMenuCanvas.SetActive(true);
        }

        public void OpenCreditsMenu()
        {
            primaryMenuCanvas.SetActive(true);
            creditsMenuCanvas.SetActive(true);
        }
    }
}