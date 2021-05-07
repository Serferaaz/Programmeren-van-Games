using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int lives;
    public int score;
    public Text LivesText;
    public Text ScoreText;
    public Text highScoreText;
    public bool gameOver;
    public GameObject gameOverPanel;
    public int numberOfBricks;
    public Transform[] levels;
    public int currentLevel = 0;
    public BallScript ballScript;
   

    // Start is called before the first frame update
    void Start() {
        LivesText.text = "Lives: " + lives;
        ScoreText.text = "Score: " + score;
        numberOfBricks = GameObject.FindGameObjectsWithTag("Brick").Length;
    }


    public void UpdateLives(int changeInLives) {
        lives += changeInLives;

        // Kijk of je nog levens hebt en zo niet dan trigger je het einde van de game.
        if (lives <= 0) {
            lives = 0;
            GameOver();
        }

        LivesText.text = "Lives: " + lives;
    }

    // Hier komen de punten erbij bij je score.
    public void UpdateScore(int points) {
        score += points;

        ScoreText.text = "Score: " + score;
    }
    // Je kijkt hier of er 0 bricks zijn, als dat zo is kijk je of er nog meer levels zijn. Als er meer levels zijn dan zet je de volgende level na 2 seconden.
    // Als dat niet zo is dan is het Game Over.
    public void UpdateNumbersOfBricks(){
        numberOfBricks--;
        if(numberOfBricks <= 0){
            if (currentLevel >= levels.Length - 1)
            {
                GameOver();
            }
            else
            {
                ballScript.inPlay = false;
                Invoke("LoadingLevels", 2f);
            }
        }
    }

    // Hier laadt je het volgende level als.
    void LoadingLevels() {
        currentLevel++;
        Instantiate(levels[currentLevel], Vector2.zero, Quaternion.identity);
        numberOfBricks = GameObject.FindGameObjectsWithTag("Brick").Length;
    }
    // Hier stopt de game mee en hier activeer je de Game Over panel. Je ziet dan ook de nieuwe high score maar als de een nieuwe speler minder punten behaald
    // dan zie je op je scherm de oude highscore en de tekst: Can you beat it?
    void GameOver(){
        gameOver = true;
        gameOverPanel.SetActive (true);
        int highscore = PlayerPrefs.GetInt("HIGHSCORE");
        if( score > highscore){
            PlayerPrefs.SetInt("HIGHSCORE", score);

            highScoreText.text = "New High Score! " + score;
        }
        else {
            highScoreText.text = "High Score: " + highscore + "\n" + "Can you beat it?";
        }
    }

    // Door deze functie laad je scene opnieuw.
    public void Retry(){
        SceneManager.LoadScene("Main");
    }

    // Door deze functie quit je de game.
    public void Quit(){
        SceneManager.LoadScene("Startscherm");
    }
}