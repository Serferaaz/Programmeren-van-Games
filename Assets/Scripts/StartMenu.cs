using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    // Als de Quit Button word ingedrukt dan stop je de applicatie.
  public void QuitGame(){
        Application.Quit();
        Debug.Log("Quit button pushed");
    }
    // Als de Start Game Button word ingedrukt begint het spel met spelen.
    public void StartGame(){
        SceneManager.LoadScene("Main");
    }
}
