using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField]
    private AudioMixer mixer;
    [SerializeField]
    private MainSceneController controller;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void Resume()
    {
        controller.OnPause();
    }

    public void UpdateMasterVolume(float value)
    {
        mixer.SetFloat("Master", value);
    }

    public void UpdateMusicVolume(float value)
    {
        mixer.SetFloat("Music", value);
    }
    public void UpdateEffectsVolume(float value)
    {
        mixer.SetFloat("Effects", value);
    }

    public void Quit()
    {
        SceneManager.LoadScene("GameOver");
    }
}
