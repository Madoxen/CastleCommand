using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject menu;
    [SerializeField]
    private GameObject guide;
    [SerializeField]
    private GameObject leaderboard;
    [SerializeField]
    private List<Sprite> guidePages;
    [SerializeField]
    private Image guideDisplay;
    [SerializeField]
    private Button prevBtn;
    [SerializeField]
    private Button nextBtn;

    private int _currengGuidePage;
    private int currentGuidePage
    {
        get => _currengGuidePage;
        set
        {
            _currengGuidePage = value;
            guideDisplay.sprite = guidePages[_currengGuidePage];
            if (_currengGuidePage == 0)
            {
                prevBtn.gameObject.SetActive(false);
                nextBtn.gameObject.SetActive(true);
            }
            else if (_currengGuidePage == guidePages.Count - 1)
            {
                prevBtn.gameObject.SetActive(true);
                nextBtn.gameObject.SetActive(false);
            }
            else
            {
                prevBtn.gameObject.SetActive(true);
                nextBtn.gameObject.SetActive(true);
            }
        }
    }

    public void Awake()
    {
        currentGuidePage = 0;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }
    public void ShowGuide()
    {
        Debug.Log("Showing the guide");
        guide.SetActive(true);
        menu.SetActive(false);
    }

    public void ShowLeaderboard()
    {
        Debug.Log("Showing the leaderboard");
        leaderboard.SetActive(true);
        menu.SetActive(false);
    }

    public void GuideNextPage()
    {
        currentGuidePage += 1;
    }

    public void GuidePrevPage()
    {
        currentGuidePage -= 1;
    }

    public void BackToMenu()
    {
        currentGuidePage = 0;
        Debug.Log("Showing the menu");
        menu.SetActive(true);
        guide.SetActive(false);
    }
    public void Quit()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
