using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DBD.Core
{
    public class AudioPlayer : MonoBehaviour
    {
        AudioSource audioSource;
        [SerializeField] AudioSource bgMusic01;
        OptionsController optionsController;

        // Start is called before the first frame update
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.volume = 0.6f;
        }

        public void SetVolume(float volume)
        {
            audioSource.volume = volume;
        }

    }
}