using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Script for main menu
public class MainMenu : MonoBehaviour
{
    [SerializeField] 
    private int next_scene_index = 0; // Level 1 build index goes here
    
    // Loads the next game scene
    public void Play()
    {
        SceneManager.LoadScene(next_scene_index);
    }
    
    // Closes the application
    public void Quit()
    {
        Application.Quit();
    }
}
