using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadForestScene(){
        SceneManager.LoadScene("GreenPlanet");
    }

    public void LoadSceneSelecter(){
        SceneManager.LoadScene("SceneSelect");
    }

    public void LoadIceScene(){
        SceneManager.LoadScene("BluePlanet");
    }

    public void LoadFireScene(){
        SceneManager.LoadScene("RedPlanet");
    }

    public void LoadMenuScreen(){
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadNextScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame(){
        Application.Quit();
    }
}