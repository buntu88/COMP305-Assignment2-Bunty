using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameController : MonoBehaviour
{
    // PRIVATE INSTANCE VARIABLES
    private int _scoreValue;
    private int _livesValue;
    private AudioSource _gameOver;

    public buntyController bunty;
   

    // PUBLIC ACCESS METHODS
    public int ScoreValue
    {
        get
        {
            return this._scoreValue;
        }

        set
        {
            this._scoreValue = value;
            this.ScoreLabel.text = "Score: " + this._scoreValue;
        }
    }

    public int LivesValue
    {
        get
        {
            return this._livesValue;
        }

        set
        {
            this._livesValue = value;
            if (this._livesValue <= 0)
            {
                this._endGame();
            }
            else
            {
                this.LivesLabel.text = "Lives: " + this._livesValue;
            }
        }
    }

    // PUBLIC INSTANCE VARIABLES
    public Text LivesLabel;
    public Text ScoreLabel;
    public Text GameOverLabel;
    public Text HighScoreLabel;
    public Button RestartButton;

    // Use this for initialization
    void Start()
    {
        this._initialize();

    }

    // Update is called once per frame
    void Update()
    {

    }

    //PRIVATE METHODS ++++++++++++++++++

    //Initial Method
    private void _initialize()
    {
        this.ScoreValue = 0;
        this.LivesValue = 5;
        this.GameOverLabel.gameObject.SetActive(false);
        this.HighScoreLabel.gameObject.SetActive(false);
        this.RestartButton.gameObject.SetActive(false);
    }

    public void _endGame()
    {
        if (this._livesValue < 1)
        {
            this.GameOverLabel.text = "Game Over";
        }
        else
        {
            this.GameOverLabel.text = "You Won";
        }
        this.HighScoreLabel.text = "High Score: " + this._scoreValue;
        this.GameOverLabel.gameObject.SetActive(true);
        this.HighScoreLabel.gameObject.SetActive(true);
        this.LivesLabel.gameObject.SetActive(false);
        this.ScoreLabel.gameObject.SetActive(false);
        this.RestartButton.gameObject.SetActive(true);
        this.bunty.gameObject.SetActive(false);
        this._gameOver.Play();
    }

    public void RestartButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
