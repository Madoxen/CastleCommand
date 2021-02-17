using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject _menuCanvas;
    public GameObject _guideCanvas;

    public void Awake()
    {
        //_menuCanvas = GameObject.Find("Canvas");
        //_guideCanvas = GameObject.Find("GuideCanvas");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }
    public void ShowGuide()
    {
        Debug.Log("Showing the guide");
        _guideCanvas.SetActive(true);
        _menuCanvas.SetActive(false);
    }
    public void BackToMenu()
    {
        Debug.Log("Showing the menu");
        _menuCanvas.SetActive(true);
        _guideCanvas.SetActive(false);
    }
    public void Quit()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
