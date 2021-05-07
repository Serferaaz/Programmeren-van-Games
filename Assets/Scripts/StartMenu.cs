using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    // Als de Quit Button word ingedrukt dan stop je de applicatie
  public void QuitGame(){
        Application.Quit();
        Debug.Log("Quit button pushed");
    }

    public void StartGame(){
        SceneManager.LoadScene("Main");
    }
}
