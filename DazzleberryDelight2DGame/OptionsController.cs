using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DBD.Core
{
    public class OptionsController : MonoBehaviour
    {
        [SerializeField] Slider volumeSlider;
        float defaultVolume = 0.6f;

        // Start is called before the first frame update
        void Start()
        {
            volumeSlider.value = defaultVolume;
        }

        // Update is called once per frame
        void Update()
        {
            var audioPlayer = FindObjectOfType<AudioPlayer>();
            if (audioPlayer)
            {
                audioPlayer.SetVolume(volumeSlider.value);
            }
            else
            {
                Debug.LogWarning("No audio player found.");
            }
        }

        public void SetDefault()
        {
            volumeSlider.value = defaultVolume;
            PlayerPrefsController.SetMasterVolume(volumeSlider.value);
        }

        public void SaveOptions()
        {
            PlayerPrefsController.SetMasterVolume(volumeSlider.value);
        }
    }
}