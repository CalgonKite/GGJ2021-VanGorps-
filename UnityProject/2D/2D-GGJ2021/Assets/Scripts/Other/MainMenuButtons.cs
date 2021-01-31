using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{

    [Header("UIOverlays")]
    public GameObject ControlsUI;
    public GameObject OptionsUI;
    public GameObject LevelSelectUI;
    bool MenuOpen = false;

    private void Update()
    {
        if (!MenuOpen)
        {
            ControlsUI.SetActive(false);
            OptionsUI.SetActive(false);
            LevelSelectUI.SetActive(false);
        }
    }

    public void StartGame()
    {
       SceneManager.LoadScene(1);
    }

    public void Controls()
    {
        ControlsUI.SetActive(true);
        MenuOpen = true;
    }

    public void Options()
    {
        OptionsUI.SetActive(true);
        MenuOpen = true;
    }

    public void LevelSelect()
    {
        LevelSelectUI.SetActive(true);
        MenuOpen = true;
    }

    public void BackButton()
    {
        MenuOpen = false;
    }

    public void StartBeachLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void StartJungleLevel()
    {
        SceneManager.LoadScene(2);
    }

    public void StartMountainLevel()
    {
        //SceneManager.LoadScene(3);
    }



}
