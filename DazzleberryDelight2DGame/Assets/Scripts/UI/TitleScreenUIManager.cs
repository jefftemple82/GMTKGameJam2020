using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenUIManager : MonoBehaviour
{
    [SerializeField] GameObject creditsScreen;

    // Start is called before the first frame update
    void Start()
    {
        creditsScreen.SetActive(false);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenCredits()
    {
        creditsScreen.SetActive(true);
    }

    public void CloseCredits()
    {
        creditsScreen.SetActive(false);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
