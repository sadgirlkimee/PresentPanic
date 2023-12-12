using UnityEngine;
using UnityEngine.UI;
using System.Net.Mime;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public Pacman pacman;
    public Transform pellets;
    public Text scoreText;
    public Text livesText;
    public Text GameOverText;

    public int ghostMultiplier { get; private set; } = 1;

    public int score { get; private set; }
    public int lives { get; private set; }


    private void Start()
    {
        LoadScore();
        NewGame();
    }


    private void Update()
    {
        if (lives <= 0 && Input.anyKeyDown)
        {
            SaveScore();
            NewGame();
        }
        else if (!HasRemainingPellets())
        {
            pacman.gameObject.SetActive(false);
            SaveScore();
            LoadNextLevel();
            // You can choose to uncomment the following line if you want a delay before loading the next level
            // Invoke(nameof(NewRound), 3.0f);
        }
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            // If there is no next scene, you can reload the current scene
            SceneManager.LoadScene(currentSceneIndex);
        }
    }

    private void NewGame()
    {
        SetLives(3);
        NewRound();
    }

    private void NewRound()
    {
        GameOverText.enabled = false;

        foreach (Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
        }
        ResetState();
    }

    private void SaveScore()
    {
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.Save();
    }

    private void LoadScore()
    {
        score = PlayerPrefs.GetInt("Score", 0);
        SetScore(score);
    }
    private void ResetState()
    {
        //
        ResetGhostMultiplier();

        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].ResetState();
        }

        this.pacman.ResetState();
    }

    private void GameOver()
    {
        GameOverText.enabled = true;

        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].gameObject.SetActive(false);
        }
        this.pacman.gameObject.SetActive(false);
    }

    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString().PadLeft(2, '0');
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
        livesText.text = "x" + lives.ToString();
    }

    public void GhostEaten(Ghost ghost)
    {
        int points = ghost.points * this.ghostMultiplier;
        SetScore(this.score + points);
        this.ghostMultiplier++;
    }

    public void PacmanEaten()
    {
        this.pacman.gameObject.SetActive(false);

        SetLives(this.lives - 1);

        if (this.lives > 0)
        {
            Invoke(nameof(ResetState), 3.0f);
        }
        else
        {
            GameOver();
        }
    }

    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);

        SetScore(this.score + pellet.points);
        if (!HasRemainingPellets())
        {
            this.pacman.gameObject.SetActive(false);
            LoadNextLevel();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //Invoke(nameof(NewRound), 3.0f);
        }
    }

    public void PowerPelletEaten(PowerPellet pellet)
    {

        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].frightened.Enable(pellet.duration);
        }

        PelletEaten(pellet);
        CancelInvoke();
        Invoke(nameof(ResetGhostMultiplier), pellet.duration);
    }

    private bool HasRemainingPellets()
    {
        foreach (Transform pellet in this.pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }

    private void ResetGhostMultiplier()
    {
        this.ghostMultiplier = 1;
    }

}