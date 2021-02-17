using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    private Text pointsText;
    [SerializeField]
    private GameObject mainUI;
    [SerializeField]
    private GameObject saveScoreUI;
    [SerializeField]
    private Button saveScoreButton;
    [SerializeField]
    private InputField nameInput;
    public void Awake()
    {
        pointsText.text = $"Your city has fallen. You scored {PlayerPoints.Value} points.";
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void SaveScore()
    {
        mainUI.SetActive(false);
        saveScoreUI.SetActive(true);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("StartUI");
    }

    public void SubmitScore()
    {
        if (!string.IsNullOrWhiteSpace(nameInput.text))
        {
            submitScore(nameInput.text);
            mainUI.SetActive(true);
            saveScoreUI.SetActive(false);
            saveScoreButton.interactable = false;
        }
    }

    private void submitScore(string name)
    {
        PlayerPoints.AddHighscoreEntry((int) PlayerPoints.Value, name);
    }
}
