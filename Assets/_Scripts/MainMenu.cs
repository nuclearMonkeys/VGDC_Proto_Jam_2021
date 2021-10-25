using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Script for main menu
public class MainMenu : MonoBehaviour
{
    [SerializeField] 
    private int next_scene_index = 0; // Level 1 build index goes here
    
    [SerializeField]
    private GameObject settings_menu = null;
    
    [SerializeField]
    private bool show = false;
    
    [SerializeField]
    private Slider slider = null;
    
    [SerializeField]
    private AudioManager audio = null;
    
    // Loads the next game scene
    public void Play()
    {
        SceneManager.LoadScene(next_scene_index);
    }
    
    // Show the settings menu
    public void DisplaySettings(bool show)
    {
        settings_menu.SetActive(show);
    }
    
    // Closes the application
    public void Quit()
    {
        Application.Quit();
    }
    
    public void ChangeVolume()
    {
       // audio.SetVolume(slider.value);
        AudioManager.instance.SettingsButton(slider.value);

    }
}
