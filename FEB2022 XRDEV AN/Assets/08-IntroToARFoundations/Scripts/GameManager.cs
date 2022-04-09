using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// This game manager will be tasked with holding relevant information of the game
/// it will also be tasked with the control of the number of lives our robot has
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int lives = 10;
    public Canvas gameOverCanvas;
    public TMP_Text livesText;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        gameOverCanvas.enabled = false;
        livesText.text = lives.ToString();
    }

    public void LoseLives()
    {
        // decrease lives by one
        lives--;

        livesText.text = lives.ToString();

        // check to see if lives is 0, if so game over
        if (lives <= 0)
        {
            // game over!
            gameOverCanvas.enabled = true;
        }
    }
}
