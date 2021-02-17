using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneController : MonoBehaviour
{
    private MasterInput input;
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private Canvas ingameMenu;

    // Start is called before the first frame update
    void Start()
    {
        input = MasterInputProvider.input;
        input.UI.Enable();
        input.UI.Pause.performed += OnPause;
    }

    public void OnPause()
    {
        if (pauseMenu.activeSelf)
        {
            Time.timeScale = 1f;
            input.Camera.Enable();
            ingameMenu.enabled = true;
            pauseMenu.SetActive(false);
        }
        else
        {
            Time.timeScale = 0f;
            input.Camera.Disable();
            ingameMenu.enabled = false;
            pauseMenu.SetActive(true);
        }
    }

    private void OnPause(UnityEngine.InputSystem.InputAction.CallbackContext obj) => OnPause();

    // Update is called once per frame
    void Update()
    {
    }
}
